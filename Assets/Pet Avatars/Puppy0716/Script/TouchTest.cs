using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : MonoBehaviour
{
    public float minLaunchForce = 5f; // 최소 발사 힘
    public float maxLaunchForce = 10f; // 최대 발사 힘
    //public float launchForce = 10f; // 발사 힘
    public float angle = 45f; // 발사 각도 (도 단위)
    private Rigidbody rb;
    public bool isFlying = false;

    // Start는 첫 프레임 업데이트 전에 호출됩니다.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody 컴포넌트를 이 게임 오브젝트에서 찾을 수 없습니다.");
        }
        else
        {
            rb.isKinematic = true; // 초기에는 움직이지 않도록 설정
            rb.useGravity = false; // 초기에는 중력이 작용하지 않도록 설정
        }
    }

    // Update는 매 프레임 호출됩니다.
    void Update()
    {
        if (isFlying && transform.position.y <= -0.2)
        {
            StopBall();
        }
    }

    public void touchtest1()
    {
        Debug.Log("공 터치 테스트");
        LaunchBall();
    }

    void LaunchBall()
    {
        // 일정 거리만큼만 날아가게 (pilot test 용)
        float launchForce = 4f;
        float angleInRadians = angle * Mathf.Deg2Rad;

        // X-Z 평면 방향 랜덤 회전 (0~360도)
        float randomYaw = Random.Range(0f, 360f);
        Quaternion rotation = Quaternion.Euler(0, randomYaw, 0);

        // 발사 벡터 (기준: Z방향으로 발사 후, 회전 적용)
        Vector3 launchDirection = new Vector3(0, Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians));
        Vector3 rotatedDirection = rotation * launchDirection;

        Vector3 launchVelocity = rotatedDirection * launchForce;

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.velocity = launchVelocity;

        Debug.Log($"발사 방향 각도(Y축 회전): {randomYaw}도");
        isFlying = true;    // 비행 중으로 회전

        /* // 발사 힘을 최소값과 최대값 사이에서 랜덤하게 설정
        float launchForce = Random.Range(minLaunchForce, maxLaunchForce);
        Debug.Log($"발사 힘 (랜덤): {launchForce}");

        Debug.Log("공 발사 테스트");
        rb.isKinematic = false; // 포물선 운동을 시작할 때 kinematic을 해제
        rb.useGravity = true; // 중력을 활성화

        float angleInRadians = angle * Mathf.Deg2Rad; // 각도를 라디안으로 변환
        float launchVelocityZ = launchForce * Mathf.Cos(angleInRadians); // z축 속도
        float launchVelocityY = launchForce * Mathf.Sin(angleInRadians); // y축 속도

        float randomAngle = Random.Range(-15f, 15f);
        float angleOffsetInRadians = randomAngle * Mathf.Deg2Rad; 
        float launchVelocityX = launchForce * Mathf.Sin(angleOffsetInRadians);

        
        // 발사 방향 랜덤 결정: -1 (왼쪽), 0 (직진), 1 (오른쪽)
        //int randomDirection = Random.Range(-1, 2); // -1, 0, 1 중 하나를 선택
        //float launchVelocityX = randomDirection * launchForce * 0.5f; // x축 속도

        //Vector3 launchVelocity = new Vector3(0, launchVelocityY, launchVelocityZ); // 초기 속도 벡터 설정
        Vector3 launchVelocity = new Vector3(launchVelocityX, launchVelocityY, launchVelocityZ); // 초기 속도 벡터 설정
        rb.velocity = launchVelocity; // 리지드바디에 초기 속도 적용

        //Debug.Log($"발사 방향: {randomDirection}, 속도: {launchVelocity}");
        Debug.Log($"발사 방향 (랜덤 각도): {randomAngle}도, 속도: {launchVelocity}");
        isFlying = true; // 비행 중으로 설정
        */
    }

    void StopBall()
    {
        Debug.Log("공 멈춤 테스트");
        rb.isKinematic = true; // y축 값이 0가 되면 멈추게 설정
        rb.useGravity = false; // 중력을 비활성화
        rb.velocity = Vector3.zero; // 속도를 0으로 설정
        isFlying = false; // 비행 상태 해제
    }
}
