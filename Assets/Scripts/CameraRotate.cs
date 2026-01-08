using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform target;
    public float distance = 1f;
    public float speed = 30f;

    void LateUpdate()
    {
        if (!target)
            return;

        // target 중심으로 회전
        transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);

        // camera 방향 유지
        transform.LookAt(target.position);

        // target과 거리 유지
        transform.position = target.position + (transform.position - target.position).normalized * distance;

    }

    // Start is called before the first frame update
    void Start()
    {
        LateUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
