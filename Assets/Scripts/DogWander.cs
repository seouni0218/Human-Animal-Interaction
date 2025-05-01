using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogWander : MonoBehaviour
{
    public Animator animator;

    public float wanderRadius = 5f;
    public float wanderTimer = 3f;
    public float moveSpeed = 0.1f;

    public float restChance = 0.1f;
    public float restDuration = 30f;

    public float stopDistance = 0.5f;
    public float rotationSpeed = 1.5f;

    private float timer;
    private bool isResting = false;
    public bool isNameCalled = false;
    //private bool isInteracting = false;

    public Transform userTransform;
    private Vector3 currentTarget;

    void Start()
    {
        timer = wanderTimer;

        if (userTransform == null)
        {
            userTransform = Camera.main?.transform;

            if (userTransform == null)
            {
                Debug.LogError("Main camera not found!");
            }
        }

        PickNewWanderTarget();
    }

    void Update()
    {
        if (isResting)
        {
            animator.SetFloat("Speed", 0f);
            return;
        }

        if (isNameCalled)
        {
            //Vector3 targetPos = GetFloatingPositionInFrontOfUser(userTransform, 1.5f);
            //MoveTowards(targetPos);
            //animator.SetFloat("Speed", 0f);
            //Debug.Log("")
            return;
        }
        //return;

        timer += Time.deltaTime;

        MoveTowards(currentTarget);

        if (Vector3.Distance(transform.position, currentTarget) <= stopDistance)
        {
            if (Random.value < restChance)
            {
                StartCoroutine(RestRoutine());
            }
            else
            {
                PickNewWanderTarget();
                timer = 0;
            }
        }

        if (timer >= wanderTimer)
        {
            PickNewWanderTarget();
            timer = 0;
        }
    }

    public void OnNameCalled()
    {
        isNameCalled = true;
        //animator.SetFloat("Speed", 1f);

        // 매번 새로운 타겟 위치 계산해서 이동
        //userTransform = Camera.main?.transform;
        //currentTarget = GetFloatingPositionInFrontOfUser(userTransform, 1.5f);
    }

    private void MoveTowards(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            // 방향으로 회전
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 전방 방향으로 이동
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            animator.SetFloat("Speed", 0.4f); // 걷는 애니메이션
        }
        else
        {
            RotateTowardsUser();
            animator.SetFloat("Speed", 0f);
            //animator.SetTrigger("Sit");
        }
    }

    private IEnumerator RestRoutine()
    {
        isResting = true;
        animator.SetTrigger("Sit");
        yield return new WaitForSeconds(restDuration);
        isResting = false;
        PickNewWanderTarget();
    }

    private Vector3 GetFloatingPositionInFrontOfUser(Transform user, float distance = 1.5f)
    {
        Vector3 forward = new Vector3(user.forward.x, 0f, user.forward.z).normalized;
        Vector3 targetPos = user.position + forward * distance;
        targetPos.y = transform.position.y; // 높이 유지
        return targetPos;
    }

    private void PickNewWanderTarget()
    {
        Vector2 randCircle = Random.insideUnitCircle * wanderRadius;
        Vector3 randomOffset = new Vector3(randCircle.x, 0f, randCircle.y);
        currentTarget = transform.position + randomOffset;
    }

    private void RotateTowardsUser()
    {
        Vector3 directionToUser = userTransform.position - transform.position;
        directionToUser.y = 0f; // 수평 방향만 고려

        if (directionToUser.sqrMagnitude < 0.01f) return; // 너무 가까우면 무시

        Quaternion targetRotation = Quaternion.LookRotation(directionToUser);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

}