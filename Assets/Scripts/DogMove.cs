using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMove : MonoBehaviour
{

    public Transform userTransform;
    private Vector3 currentTarget;

    public Animator animator;

    private float stopDistance = 0.5f;
    private float rotationSpeed = 1.5f;
    private float wanderRadius = 5f;
    private float wanderTimer = 3f;
    private float moveSpeed = 0.1f;

    public bool isMovingToUser = false; // �̵� �� ���� �÷���

    // Start is called before the first frame update
    void Start()
    {
        if (userTransform == null)
        {
            userTransform = Camera.main?.transform;

            if (userTransform == null)
            {
                Debug.LogError("Main camera not found!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        userTransform = Camera.main?.transform;

        if (isMovingToUser)
        {
            //Debug.Log("Called");
            Vector3 targetPos = GetFloatingPositionInFrontOfUser(userTransform, 1.5f);
            MoveTowards(targetPos);
            return;
        }
    }

    public void OnDogNameCalled()
    {
        //Debug.Log("Called");
        //Vector3 targetPos = GetFloatingPositionInFrontOfUser(userTransform, 1.5f);
        isMovingToUser = true;
        animator.SetFloat("Speed", 1f);
        //MoveTowards(currentTarget);
        //return;
    }

    private void MoveTowards(Vector3 target)
    {
        //Debug.Log("Called");
        Vector3 direction = target - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            //Debug.Log("Called");
            // �������� ȸ��
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // ���� �������� �̵�
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            animator.SetFloat("Speed", 0.4f); // �ȴ� �ִϸ��̼�
        }
        else
        {
            RotateTowardsUser();
            animator.SetFloat("Speed", 0f);
            isMovingToUser = false;
            //animator.SetTrigger("Sit");
        }
    }

    private Vector3 GetFloatingPositionInFrontOfUser(Transform user, float distance = 1.5f)
    {
        Vector3 forward = new Vector3(user.forward.x, 0f, user.forward.z).normalized;
        Vector3 targetPos = user.position + forward * distance;
        targetPos.y = transform.position.y; // ���� ����
        return targetPos;
    }

    private void RotateTowardsUser()
    {
        Vector3 directionToUser = userTransform.position - transform.position;
        directionToUser.y = 0f; // ���� ���⸸ ���

        if (directionToUser.sqrMagnitude < 0.01f) return; // �ʹ� ������ ����

        Quaternion targetRotation = Quaternion.LookRotation(directionToUser);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
