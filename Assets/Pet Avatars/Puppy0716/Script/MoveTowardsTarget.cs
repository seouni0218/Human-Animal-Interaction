using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    public GameObject dog; // 이동할 게임 오브젝트 (강아지)
    public GameObject cushion; // 기준이 되는 게임 오브젝트 (방석)
    public float offset = 0.1f; // 약간 더 밑으로 이동할 오프셋
    public float speed = 0.5f; // 이동 속도

    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private bool shouldMoveToTarget = false;
    private bool shouldMoveToInitial = false;
    private Animator dogAnimator;

    void Start()
    {
        if (dog == null)
        {
            Debug.LogError("Dog object not assigned.");
            return;
        }
        if (cushion == null)
        {
            Debug.LogError("Cushion object not assigned.");
            return;
        }

        dogAnimator = dog.GetComponent<Animator>();
        if (dogAnimator == null)
        {
            Debug.LogError("Dog object does not have an Animator component.");
            return;
        }

        initialPosition = dog.transform.position; // 초기 위치 저장
        CalculateTargetPosition();
        StartMovingToTargetPosition();
    }

    void Update()
    {
        if (shouldMoveToTarget)
        {
            MoveToTarget(targetPosition, ref shouldMoveToTarget, "isSitting");
        }
        else if (shouldMoveToInitial)
        {
            MoveToTarget(initialPosition, ref shouldMoveToInitial, null);
        }
    }

    void StartMovingToTargetPosition()
    {
        shouldMoveToTarget = true;
        dogAnimator.SetBool("isWalking", true); // 이동 시작 애니메이션 트리거
    }

    void StartMovingToInitialPosition()
    {
        shouldMoveToInitial = true;
        dogAnimator.SetBool("isSitting", false); // 앉는 애니메이션 종료
        dogAnimator.SetBool("isWalking", true); // 이동 시작 애니메이션 트리거
    }

    void MoveToTarget(Vector3 targetPos, ref bool shouldMove, string sitAnimationTrigger)
    {
        dog.transform.position = Vector3.MoveTowards(dog.transform.position, targetPos, speed * Time.deltaTime);

        // Check if the dog has reached the target position
        if (Vector3.Distance(dog.transform.position, targetPos) < 0.01f)
        {
            shouldMove = false;
            dogAnimator.SetBool("isWalking", false); // 이동 종료 애니메이션 트리거

            dog.transform.position = targetPos;

            if (sitAnimationTrigger != null)
            {
                dogAnimator.SetBool(sitAnimationTrigger, true); // 앉는 애니메이션 트리거
                Debug.Log("Dog moved to the target position.");
                Debug.Log("Sitting animation triggered.");

                // 몇 초 후에 다시 처음 위치로 돌아가기 시작
                Invoke("StartMovingToInitialPosition", 15.0f); // 15초 대기
            }
            else
            {
                Debug.Log("Dog moved back to the initial position.");
            }
        }
    }

    void CalculateTargetPosition()
    {
        // 방석 오브젝트의 위치와 크기를 가져옴
        Vector3 cushionPosition = cushion.transform.position;
        Renderer cushionRenderer = cushion.GetComponent<Renderer>();
        Renderer dogRenderer = dog.GetComponent<Renderer>();

        if (cushionRenderer != null && dogRenderer != null)
        {
            Vector3 cushionSize = cushionRenderer.bounds.size;
            Vector3 dogSize = dogRenderer.bounds.size;

            // 강아지 오브젝트의 y축 위치를 방석 오브젝트의 y축 위치 바로 아래로 설정
            cushionPosition.y += (cushionSize.y / 2 + dogSize.y / 2) - offset;

            // 타겟 위치를 설정
            targetPosition = cushionPosition;
        }
        else
        {
            if (cushionRenderer == null)
                Debug.LogError("Cushion does not have a Renderer component.");
            if (dogRenderer == null)
                Debug.LogError("Dog does not have a Renderer component.");
        }
    }
}
