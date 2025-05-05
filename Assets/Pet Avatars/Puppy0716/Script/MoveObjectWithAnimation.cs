using UnityEngine;
using System.Collections;

public class MoveObjectWithAnimation : MonoBehaviour
{
    // 이동시킬 GameObject와 속도를 위한 public 변수
    public GameObject targetObject;
    public float speed = 1.0f;

    // Animator 컴포넌트를 위한 변수
    private Animator animator;

    // 수정된 목적지 위치 (y = -0.247, z = 0.5)
    private Vector3 targetPosition = new Vector3(0f, -0.247f, 0.5f);

    void Start()
    {
        // targetObject에 연결된 Animator 컴포넌트 가져오기
        if (targetObject != null)
        {
            animator = targetObject.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator 컴포넌트가 targetObject에 없습니다.");
            }
        }
        else
        {
            Debug.LogError("targetObject가 설정되지 않았습니다.");
        }
    }

    // public 메서드: GameObject를 지정된 위치로 이동시킴
    public void MoveToPosition()
    {
        if (targetObject != null)
        {
            // 코루틴 시작
            StartCoroutine(MoveCoroutine());
        }
        else
        {
            Debug.LogError("targetObject가 설정되지 않았습니다.");
        }
    }

    private IEnumerator MoveCoroutine()
    {
        if (animator != null)
        {
            // 애니메이션의 isWalking을 true로 설정
            animator.SetBool("isWalking", true);
            animator.CrossFade("isWalking", 0.1f);
        }



        // 1초 대기
        //yield return new WaitForSeconds(1.0f);



        // 이동하는 동안 루프
        while (Vector3.Distance(targetObject.transform.position, targetPosition) > 0.01f)
        {
            // 현재 위치에서 목표 위치로 속도에 맞춰 이동
            targetObject.transform.position = Vector3.MoveTowards(targetObject.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        if (animator != null)
        {
            // 목표 위치에 도달한 후, 애니메이션의 isWalking을 false로 설정
            animator.SetBool("isWalking", false);
        }

        // 정확하게 목표 위치로 설정
        targetObject.transform.position = targetPosition;
    }
}
