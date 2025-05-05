using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEating : MonoBehaviour
{
    public Animator anim;
    public GameObject otherGameObject; // 비활성화할 게임 오브젝트
    public GameObject food1; // 비활성화할 첫 번째 음식
    public GameObject food2; // 비활성화할 두 번째 음식
    public GameObject food3; // 비활성화할 세 번째 음식

    private float timer = 0f;

    private bool isEatingStarted = false;
    private bool isEatingEnded = false;

    private MeshRenderer otherMeshRenderer;
    private MeshRenderer food1MeshRenderer;
    private MeshRenderer food2MeshRenderer;
    private MeshRenderer food3MeshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isEating", false);
        Debug.Log("Start method called: isEating set to false");

        InitializeMeshRenderer(otherGameObject, ref otherMeshRenderer, "OtherGameObject");
        InitializeMeshRenderer(food1, ref food1MeshRenderer, "Food1");
        InitializeMeshRenderer(food2, ref food2MeshRenderer, "Food2");
        InitializeMeshRenderer(food3, ref food3MeshRenderer, "Food3");
    }

    // Update is called once per frame
    void Update()
    {
        if (isEatingStarted)
        {
            timer = Time.deltaTime;

            CheckAndDisableMeshRenderer(timer + 2f, food1MeshRenderer);
            CheckAndDisableMeshRenderer(timer + 3f, food2MeshRenderer);
            CheckAndDisableMeshRenderer(timer + 5f, food3MeshRenderer);
            CheckAndDisableMeshRenderer(timer + 7f, otherMeshRenderer); // 마지막 오브젝트
            isEatingStarted = false;
            isEatingEnded = true;
        }
        else if (isEatingEnded)
        {
            EndAnimation();
        }
    }

    public void BtnToEat()
    {
        anim.SetBool("isEating", true);
        anim.CrossFade("isStartEating", 0.1f);
        Debug.Log("BtnToEat method called: Animation started, isEating set to true");

        // 모든 MeshRenderer를 다시 활성화
        EnableMeshRenderer(otherMeshRenderer);
        EnableMeshRenderer(food1MeshRenderer);
        EnableMeshRenderer(food2MeshRenderer);
        EnableMeshRenderer(food3MeshRenderer);

        anim.CrossFade("isEating", 0.1f);
        isEatingStarted = true;
        Debug.Log("Eating started.");
    }



    private void InitializeMeshRenderer(GameObject obj, ref MeshRenderer renderer, string name)
    {
        if (obj != null)
        {
            renderer = obj.GetComponent<MeshRenderer>();
            if (renderer == null)
            {
                Debug.LogError($"{name} does not have a MeshRenderer component.");
            }
        }
        else
        {
            Debug.LogError($"{name} is not assigned.");
        }
    }

    private void EnableMeshRenderer(MeshRenderer renderer)
    {
        if (renderer != null)
        {
            renderer.enabled = true;
            Debug.Log($"MeshRenderer of {renderer.gameObject.name} has been enabled.");
        }
    }

    private void CheckAndDisableMeshRenderer(float time, MeshRenderer renderer)
    {
        if (isEatingStarted && renderer != null && renderer.enabled)
        {
            renderer.enabled = false;
        }
    }



    private void EndAnimation()
    {
        anim.CrossFade("isEndEating", 0.1f);
        anim.SetBool("isEating", false);
        isEatingEnded = false;
        Debug.Log("Animation completed: isEating reset to false");
    }

}
