using UnityEngine;
using System.Collections;

public class ObjectMover : MonoBehaviour
{
    public GameObject objectA;
    public GameObject objectB;
    public Vector3 offset = Vector3.zero;
    public float moveSpeed = 1.0f;
    public float rotationSpeed = 360.0f; // 회전 속도 추가
    public Animator animator; // 애니메이터
    public GameObject mainCamera; // 메인 카메라를 퍼블릭으로 받아옴

    private bool isWalking = false; // 이동 상태를 나타내는 변수
    private bool isSitting = false; // 앉는 상태를 나타내는 변수

    private Vector3 initialPosition; // 초기 위치를 저장할 변수

    void Start()
    {
        if (objectA != null)
        {
            initialPosition = objectA.transform.position;
        }
    }

    // 외부에서 호출할 메서드
    public void MoveAToB()
    {
        if (objectA == null || objectB == null)
        {
            Debug.LogError("objectA 또는 objectB가 설정되지 않았습니다.");
            return;
        }

        // objectB의 Mesh Renderer가 활성화되어 있는지 확인
        MeshRenderer meshRenderer = objectB.GetComponent<MeshRenderer>();
        if (meshRenderer == null || !meshRenderer.enabled)
        {
            Debug.Log("objectB의 Mesh Renderer가 비활성화되어 이동을 시작할 수 없습니다.");
            return;
        }

        // objectA와 objectB의 현재 위치를 가져옵니다.
        Vector3 positionA = objectA.transform.position;
        Vector3 positionB = objectB.transform.position;

        // objectA가 이미 objectB의 위치에 있는지 판단
        if (IsAAtB(positionA, positionB))
        {
            Debug.Log("objectA는 이미 objectB의 위치에 있습니다.");
            StartSitting();
        }
        else
        {
            // 이동 시작 전에 애니메이션을 설정
            StartWalking();

            // 1.5초 지연 후 이동을 시작하도록 코루틴을 실행합니다.
            StartCoroutine(DelayedMoveToB(positionB + offset));
        }
    }

    // 외부에서 호출할 메서드: 지정된 위치로 되돌아가기
    public void MoveAToInitialPosition()
    {
        if (objectA == null)
        {
            Debug.LogError("objectA가 설정되지 않았습니다.");
            return;
        }

        // 앉는 애니메이션을 멈추기
        isSitting = false;
        if (animator != null)
        {
            animator.SetBool("isSitting", false);
        }

        // 3초 지연 후 걷기 애니메이션을 시작하고 초기 위치로 이동
        StartCoroutine(DelayedMoveToInitialPosition());
    }

    private IEnumerator DelayedMoveToB(Vector3 targetPosition)
    {
        // 1.5초 대기
        yield return new WaitForSeconds(1.5f);

        // objectA를 objectB의 위치로 회전 후 이동시킵니다.
        StartCoroutine(RotateAndMoveToPosition(targetPosition, true));
    }

    private IEnumerator DelayedMoveToInitialPosition()
    {
        yield return new WaitForSeconds(3);

        // 걷기 애니메이션을 시작
        StartWalking();

        // objectA를 초기 위치로 회전 후 이동시킵니다.
        StartCoroutine(RotateAndMoveToPosition(initialPosition, false, true));
    }

    // objectA가 objectB의 위치에 있는지 판단하는 메서드
    private bool IsAAtB(Vector3 positionA, Vector3 positionB)
    {
        return Vector3.Distance(positionA, positionB) < 0.01f;
    }

    // objectA를 지정된 위치로 회전 후 이동시키는 코루틴
    private IEnumerator RotateAndMoveToPosition(Vector3 targetPosition, bool toB, bool lookAtCamera = false)
    {
        Debug.Log("objectA를 목표 위치로 회전합니다.");

        // 1초 대기
        yield return new WaitForSeconds(1.0f);

        // 목표 위치로의 방향을 계산
        Vector3 direction = (targetPosition - objectA.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 회전
        while (Quaternion.Angle(objectA.transform.rotation, targetRotation) > 0.01f)
        {
            objectA.transform.rotation = Quaternion.RotateTowards(objectA.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("objectA를 이동 시작합니다.");

        // 회전이 완료된 후 목표 위치로 이동
        while (Vector3.Distance(objectA.transform.position, targetPosition) > 0.01f)
        {
            objectA.transform.position = Vector3.MoveTowards(objectA.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            Debug.Log("현재 위치: " + objectA.transform.position);
            yield return null;
        }

        objectA.transform.position = targetPosition;
        isWalking = false;
        if (animator != null)
        {
            animator.SetBool("isWalking", false);
        }

        if (toB)
        {
            StartSitting();
        }
        else if (lookAtCamera)
        {
            // MAIN CAMERA를 향해 회전
            if (mainCamera != null)
            {
                Vector3 cameraDirection = (mainCamera.transform.position - objectA.transform.position).normalized;
                Quaternion cameraRotation = Quaternion.LookRotation(cameraDirection);

                while (Quaternion.Angle(objectA.transform.rotation, cameraRotation) > 0.01f)
                {
                    objectA.transform.rotation = Quaternion.RotateTowards(objectA.transform.rotation, cameraRotation, rotationSpeed * Time.deltaTime);
                    yield return null;
                }

                objectA.transform.rotation = cameraRotation;
                Debug.Log("objectA가 MAIN CAMERA를 바라보도록 회전했습니다.");
            }
            else
            {
                Debug.LogError("mainCamera가 설정되지 않았습니다.");
            }
        }

        Debug.Log("objectA가 목표 위치로 이동했습니다.");

        // objectB의 Mesh Renderer를 비활성화
        if (!toB && objectB != null)
        {
            MeshRenderer meshRenderer = objectB.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
                Debug.Log("objectB의 Mesh Renderer가 비활성화되었습니다.");
            }
        }
    }

    private void StartWalking()
    {
        isWalking = true;
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }
    }

    private void StartSitting()
    {
        isSitting = true;
        if (animator != null)
        {
            animator.SetBool("isSitting", true);
        }
        Debug.Log("objectA가 앉는 애니메이션을 시작했습니다.");
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsSitting()
    {
        return isSitting;
    }
}
