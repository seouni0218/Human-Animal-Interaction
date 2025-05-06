using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBall : MonoBehaviour
{
    public Transform userTransform;
    private Vector3 currentTarget;
    public bool isMovingToUser = false;
    //public Transform dogTransform;
    
    public GameObject dog; // 강아지 오브젝트
    public GameObject ballPrefab; // 공 프리팹
    public GameObject targetObject; // 활성화할 게임 오브젝트
    public float moveSpeed = 3.0f; // 강아지 이동 속도
    public float rotationSpeed = 5.0f; // 강아지 회전 속도
    public float stopDistance = 0.5f; // 공에 도달한 것으로 간주되는 거리
    //public float waitTime = 3.0f; // 대기 시간

    private GameObject ballInstance; // 생성된 공 오브젝트 참조
    private TouchTest ballScript;
    
    private bool isMoving = false;
    private bool flag = false;

    private Vector3 initialPosition; // 강아지의 초기 위치
    private Animator dogAnimator;

    Coroutine rtb;

    void Start()
    {
        if (dog == null)
        {
            Debug.LogError("Dog object not assigned.");
            return;
        }
        if (ballPrefab == null)
        {
            Debug.LogError("Ball prefab not assigned.");
            return;
        }
        if (targetObject == null)
        {
            Debug.LogError("Target object not assigned.");
            return;
        }

        dogAnimator = dog.GetComponent<Animator>();
        if (dogAnimator == null)
        {
            Debug.LogError("Animator component not found on the dog object.");
            return;
        }

        // 강아지의 초기 위치 저장
        initialPosition = dog.transform.position; 
        // 활성화할 오브젝트의 Mesh Renderer 비활성화
        targetObject.GetComponent<MeshRenderer>().enabled = false;

        // user 현재 위치 저장
        userTransform = Camera.main?.transform;
    }

    void Update()
    {
        if (ballInstance == null)
        {
            ballInstance = GameObject.FindWithTag("Ball");
            if (ballInstance != null)
            {
                ballScript = ballInstance.GetComponent<TouchTest>();
                if (ballScript == null)
                {
                    Debug.LogError("TouchTest script not found on the ball instance.");
                }
            }
        }
        /////////
        if (ballInstance != null && !ballScript.isFlying && ballInstance.transform.position.y <= -0.2)
        {
            isMoving = true;
        }
        // 계속 isMoving 상태이면 안됨
        if (isMoving && !flag)
        {
            MoveDogToBall();
        }
        else if (isMoving)  // ?
        {
            dogAnimator.ResetTrigger("isTouching");
        }

        if (isMovingToUser)
        {
            Vector3 targetPos = GetFloatingPositionInFrontOfUser(userTransform, 1.5f);
            MoveTowards(targetPos);
            return;
            //isMovingToUser = true;
            //dogAnimator.SetFloat("Speed", 1f);  // run으로 바꿔도 될듯
            //dogAnimator.SetBool("isRunning", true);
        }
    }

    void MoveDogToBall()
    {
        flag = true;

        //ResetAllTriggers(dogAnimator);
        //dogAnimator.enabled = false;
        //dogAnimator.enabled = true;
        //dogAnimator.Rebind();
        //dogAnimator.Update(0f);
        //dogAnimator.ResetTrigger("isIdle");

        //dogAnimator.ResetTrigger("isTouching");

        //if (!dogAnimator.GetBool("isRunning"))
        //{
        // 공을 향해 회전하기 시작
        //    dogAnimator.SetBool("isRunning", false);
        //rtb = StartCoroutine(RotateTowardsBall());
        //}
        StartCoroutine(RotateTowardsBall());

    }

    IEnumerator RotateTowardsBall()
    {
        dogAnimator.SetBool("isTrotting", true);
        dogAnimator.CrossFade("isTrotting", 0.1f);

        // 강아지가 공을 향해 회전
        Vector3 direction = (ballInstance.transform.position - dog.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        
        while (Quaternion.Angle(dog.transform.rotation, lookRotation) > 0.1f)
        {
            //dog.transform.rotation = Quaternion.Slerp(dog.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed * 100f);
            dog.transform.rotation = Quaternion.Lerp(dog.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed * 100f);
            yield return null; // 다음 프레임까지 대기
        }

        dogAnimator.SetBool("isTrotting", false);

        dogAnimator.SetBool("isRunning", true); // 회전이 끝나면 뛰기 애니메이션 시작
        dogAnimator.CrossFade("isRunning", 0.1f);

        // 강아지가 공을 향해 이동
        while (Vector3.Distance(dog.transform.position, ballInstance.transform.position) > stopDistance)
        {
            dog.transform.position += dog.transform.forward * moveSpeed * Time.deltaTime * 100f;
            yield return null; // 다음 프레임까지 대기
        }

        dogAnimator.SetBool("isRunning", false);

        Debug.Log("Dog reached the ball.");
        Destroy(ballInstance); // 공 오브젝트 삭제
        ActivateTargetObject(); // 다른 게임 오브젝트의 Mesh Renderer 활성화

        //StartCoroutine(ReturnToStartPosition());
        isMovingToUser = true;
    }

    void ActivateTargetObject()
    {
        targetObject.GetComponent<MeshRenderer>().enabled = true;
    }
    
    // 이걸 user가 현재 있는 위치로 돌아오게 하기
    private void MoveTowards(Vector3 target)
    {
        dogAnimator.SetBool("isRunning", true);

        Vector3 direction = target - userTransform.position;
        direction.y = 0f;

        float distance = direction.magnitude;

        if(distance>stopDistance)
        {
            // user 방향으로 회전
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            dog.transform.rotation = Quaternion.Slerp(dog.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 전방 방향으로 이동
            dog.transform.position += dog.transform.forward * moveSpeed * Time.deltaTime;

            dogAnimator.SetBool("isRunning", true);
        }
        else
        {
            RotateTowardsUser();
            dogAnimator.SetBool("isRunning", false);
        }
    }

    private Vector3 GetFloatingPositionInFrontOfUser(Transform user, float distance=1.5f)
    {
        Vector3 forward = new Vector3(user.forward.x, 0f, user.forward.z).normalized;
        Vector3 targetPos = user.position + forward * distance;
        targetPos.y = dog.transform.position.y;
        return targetPos;
    }

    private void RotateTowardsUser()
    {
        Vector3 directionToUser = userTransform.position - dog.transform.position;
        directionToUser.y = 0f;

        // 너무 가까우면 무시하기
        if (directionToUser.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(directionToUser);
        dog.transform.rotation = Quaternion.Slerp(userTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

    IEnumerator ReturnToStartPosition()
    {
        //yield return new WaitForSeconds(waitTime); // 3초 대기
        /*
        if (dogAnimator.GetBool("isTouching"))
        {
            dogAnimator.SetBool("isTouching", false);
        }
        */

        dogAnimator.SetBool("isTrotting", true);
        dogAnimator.CrossFade("isTrotting", 0.1f);

        Vector3 direction = (initialPosition - dog.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        while (Quaternion.Angle(dog.transform.rotation, lookRotation) > 0.1f)
        {
            //dog.transform.rotation = Quaternion.Slerp(dog.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed * 100f);
            dog.transform.rotation = Quaternion.Lerp(dog.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed * 100f);
            yield return null; // 다음 프레임까지 대기
        }

        dogAnimator.SetBool("isTrotting", false);



        dogAnimator.SetBool("isRunning", true); // 회전 후 이동
        dogAnimator.CrossFade("isRunning", 0.1f);

        while (Vector3.Distance(dog.transform.position, initialPosition) > stopDistance)
        {
            dog.transform.position += dog.transform.forward * moveSpeed * Time.deltaTime * 100f;
            yield return null; // 다음 프레임까지 대기
        }

        dogAnimator.SetBool("isRunning", false); // 뛰기 애니메이션 중지

        Debug.Log("Dog returned to the starting position.");
        


        // 특정 애니메이션 재생
        dogAnimator.SetBool("isPlaying", true);

        yield return new WaitForSeconds(3.0f); // 애니메이션 실행 시간 대기 (예: 3초)

        dogAnimator.SetBool("isPlaying", false); // 특정 애니메이션 중지

        //Destroy(ballInstance); // 공 오브젝트 삭제

        targetObject.GetComponent<MeshRenderer>().enabled = false; // targetObject 비활성화

        flag = false;
        isMoving = false;
    }



    void ResetAllTriggers(Animator animator)
    {
        foreach (var param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(param.name);
            }
        }
    }

}
