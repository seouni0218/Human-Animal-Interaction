using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardCamera : MonoBehaviour
{
    public GameObject objectPrefab; // 생성할 오브젝트의 프리팹
    public Camera mainCamera; // 메인 카메라 참조
    public float distanceFromCamera = 5f; // 카메라로부터의 거리
    public float heightOffset = -1f; // 높이 오프셋
    private GameObject spawnedObject; // 생성된 오브젝트 참조

    void Start()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not assigned.");
            return;
        }

        if (objectPrefab == null)
        {
            Debug.LogError("Object prefab not assigned.");
            return;
        }

        SpawnAndFaceCamera();
    }

    // 오브젝트를 생성하고 카메라를 향하게 하는 함수
    public void SpawnAndFaceCamera()
    {
        // 생성 위치 계산
        Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;
        spawnPosition.y += heightOffset;

        // 오브젝트 생성
        spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

        if (spawnedObject == null)
        {
            Debug.LogError("Failed to instantiate the object.");
            return;
        }

        // 오브젝트가 카메라를 바라보게 함
        spawnedObject.transform.LookAt(mainCamera.transform);

        Debug.Log("Object spawned and positioned in front of the camera.");
    }
}
