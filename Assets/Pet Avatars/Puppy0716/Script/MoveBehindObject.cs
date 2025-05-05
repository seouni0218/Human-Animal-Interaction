using UnityEngine;
using System.Collections;

public class MoveBehindObject : MonoBehaviour
{
    public GameObject targetObject; // 기준이 되는 게임 오브젝트
    public GameObject objectToMove; // 이동할 게임 오브젝트
    public float distanceBehind = 2.0f; // 뒤로 이동할 거리
    public float distanceAbove = 1.0f; // 위로 이동할 거리
    public float moveSpeed = 1.0f; // 이동 속도
    public float rotationSpeed = 360.0f; // 회전 속도 추가
    public Animator animator; // 애니메이터

    private bool isWalking = false; // 이동 상태를 나타내는 변수

    // 외부에서 호출할 메서드
    public void MoveObjectBehindAndAbove()
    {
        if (targetObject != null && objectToMove != null)
        {
            // 타겟 오브젝트의 위치를 기준으로 z 축 뒤로 이동 및 y 축 위로 이동할 목표 위치 설정
            Vector3 behindPosition = targetObject.transform.position;
            behindPosition.z -= distanceBehind;
            behindPosition.y += distanceAbove;

            // 이동할 오브젝트를 목표 위치로 이동
            StartCoroutine(MoveToPosition(behindPosition));
        }
        else
        {
            if (targetObject == null)
                Debug.LogError("Target object not assigned.");
            if (objectToMove == null)
                Debug.LogError("Object to move not assigned.");
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        Debug.Log("Object moving to the target position.");
        isWalking = true;
        animator.SetBool("isWalking", true);
        animator.CrossFade("isWalking", 0.1f);

        // 1초 대기
        //yield return new WaitForSeconds(1.0f);

        while (Vector3.Distance(objectToMove.transform.position, targetPosition) > 0.01f)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            Debug.Log("Current position: " + objectToMove.transform.position);
            yield return null;
        }

        objectToMove.transform.position = targetPosition;
        Debug.Log("Object moved behind and above the target object.");
        
        // 목표 위치 도착 후 타겟 오브젝트를 바라보도록 회전
        Vector3 direction = (targetObject.transform.position - objectToMove.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        while (Quaternion.Angle(objectToMove.transform.rotation, targetRotation) > 0.01f)
        {
            objectToMove.transform.rotation = Quaternion.RotateTowards(objectToMove.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        objectToMove.transform.rotation = targetRotation;
        isWalking = false;
        animator.SetBool("isWalking", false);
        Debug.Log("Object rotated to face the target object.");
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
