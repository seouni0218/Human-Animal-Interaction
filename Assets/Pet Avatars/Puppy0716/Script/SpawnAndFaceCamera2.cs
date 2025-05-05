using UnityEngine;
using System.Collections;

public class SpawnAndFaceCamera2 : MonoBehaviour
{
    public GameObject objectPrefab;
    public float distanceFromCamera = 5f;
    public float heightOffset = -1f;
    private Camera mainCamera;
    private GameObject spawnedObject;

    // MeshRenderer를 활성화할 다른 게임 오브젝트를 참조합니다.
    public GameObject otherGameObject;
    // 애니메이션을 활성화할 또 다른 게임 오브젝트를 참조합니다.
    public GameObject animatedGameObject;

    void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found.");
            return;
        }

        if (objectPrefab == null)
        {
            Debug.LogError("Object prefab not assigned.");
            return;
        }

        Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;
        spawnPosition.y += heightOffset;

        spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

        if (spawnedObject == null)
        {
            Debug.LogError("Failed to instantiate the object.");
            return;
        }

        FaceCamera2 faceCamera = spawnedObject.AddComponent<FaceCamera2>();
        faceCamera.mainCamera = mainCamera;

        StartCoroutine(DestroyPrefabAfterDelay(5f)); // 5초 지연 후 프리팹 파괴
        StartCoroutine(EnableMeshRendererAfterDelay(5f)); // 5초 지연 후 다른 게임 오브젝트의 MeshRenderer 활성화

        Debug.Log("Object spawned and scripts added.");
    }

    void Update()
    {
        if (spawnedObject != null && mainCamera != null)
        {
            Vector3 newPosition = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;
            newPosition.y += heightOffset;
            spawnedObject.transform.position = newPosition;
        }
    }

    IEnumerator DestroyPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            Debug.Log("Prefab destroyed after delay");
        }
    }

    IEnumerator EnableMeshRendererAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (otherGameObject != null)
        {
            MeshRenderer meshRenderer = otherGameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
                Debug.Log("Other GameObject's MeshRenderer enabled after delay.");

                if (animatedGameObject != null)
                {
                    Animator animator = animatedGameObject.GetComponent<Animator>();
                    if (animator != null)
                    {
                        animator.SetBool("isPlaying", true); // 애니메이션 시작
                        Debug.Log("Animated GameObject's animation started.");
                    }
                    else
                    {
                        Debug.LogError("Animator not found on the animated GameObject.");
                    }
                }
                else
                {
                    Debug.LogError("Animated GameObject not assigned.");
                }

                StartCoroutine(DisableMeshRendererAfterDelay(15f)); // 지연 후 MeshRenderer 비활성화
            }
            else
            {
                Debug.LogError("MeshRenderer not found on the other GameObject.");
            }
        }
        else
        {
            Debug.LogError("Other GameObject not assigned.");
        }
    }

    IEnumerator DisableMeshRendererAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (otherGameObject != null)
        {
            MeshRenderer meshRenderer = otherGameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
                Debug.Log("Other GameObject's MeshRenderer disabled after delay.");

                if (animatedGameObject != null)
                {
                    Animator animator = animatedGameObject.GetComponent<Animator>();
                    if (animator != null)
                    {
                        animator.SetBool("isPlaying", false); // 애니메이션 중지
                        Debug.Log("Animated GameObject's animation stopped.");
                    }
                }
            }
        }
    }

    IEnumerator DestroyOtherGameObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (otherGameObject != null)
        {
            Destroy(otherGameObject);
            Debug.Log("Other GameObject destroyed after delay");
        }
    }
}

public class FaceCamera2 : MonoBehaviour
{
    public Camera mainCamera;

    void Update()
    {
        if (mainCamera != null)
        {
            transform.LookAt(mainCamera.transform);
        }
    }
}
