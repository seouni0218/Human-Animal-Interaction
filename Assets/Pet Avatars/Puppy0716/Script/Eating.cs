using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    public Animator anim;
    public GameObject otherGameObject; // 비활성화할 게임 오브젝트
    public GameObject food1; // 비활성화할 첫 번째 음식
    public GameObject food2; // 비활성화할 두 번째 음식
    public GameObject food3; // 비활성화할 세 번째 음식
    private float animationDuration = 11.3f; // 15 seconds
    private float animationTimer = 0f;
    private bool isAnimating = false;
    private bool isEatingStarted = false;
    
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
        if (isAnimating)
        {
            animationTimer += Time.deltaTime;

            if (animationTimer >= animationDuration)
            {
                EndAnimation();
            }
            else if (isEatingStarted)
            {
                CheckAndDisableMeshRenderer(8f, food1MeshRenderer);
                CheckAndDisableMeshRenderer(9f, food2MeshRenderer);
                CheckAndDisableMeshRenderer(10f, food3MeshRenderer);
                CheckAndDisableMeshRenderer(11f, otherMeshRenderer); // 마지막 오브젝트
            }
        }
    }

    public void BtnToEat()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            animationTimer = 0f;
            anim.SetBool("isEating", true);
            Debug.Log("BtnToEat method called: Animation started, isEating set to true");

            // 모든 MeshRenderer를 다시 활성화
            EnableMeshRenderer(otherMeshRenderer);
            EnableMeshRenderer(food1MeshRenderer);
            EnableMeshRenderer(food2MeshRenderer);
            EnableMeshRenderer(food3MeshRenderer);

            // Eating 시작 시점으로 타이머 초기화
            //StartCoroutine(StartEatingAfterDelay(0f)); // 0.5초 후에 isEating 시작

            isEatingStarted = true;
            Debug.Log("Eating started.");
        }
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

    private void CheckAndDisableMeshRenderer(float time, MeshRenderer renderer)
    {
        if (animationTimer >= time && renderer != null && renderer.enabled)
        {
            renderer.enabled = false;
            Debug.Log($"MeshRenderer of {renderer.gameObject.name} has been disabled at {time} seconds.");
        }
    }

    private void EndAnimation()
    {
        anim.SetBool("isEating", false);
        animationTimer = 0f;
        isAnimating = false;
        isEatingStarted = false;
        Debug.Log("Animation completed: isEating reset to false");
    }

    private void EnableMeshRenderer(MeshRenderer renderer)
    {
        if (renderer != null)
        {
            renderer.enabled = true;
            Debug.Log($"MeshRenderer of {renderer.gameObject.name} has been enabled.");
        }
    }



    /*
    public Animator anim;
    public GameObject otherGameObject; // 비활성화할 게임 오브젝트
    public GameObject food1; // 비활성화할 첫 번째 음식
    public GameObject food2; // 비활성화할 두 번째 음식
    public GameObject food3; // 비활성화할 세 번째 음식
    private float animationDuration = 11.3f; // 15 seconds
    private float animationTimer = 0f;
    private bool isAnimating = false;
    private bool isEatingStarted = false;

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
        if (isAnimating)
        {
            animationTimer += Time.deltaTime;

            if (animationTimer >= animationDuration)
            {
                EndAnimation();
            }
            else if (isEatingStarted)
            {
                CheckAndDisableMeshRenderer(8f, food1MeshRenderer);
                CheckAndDisableMeshRenderer(9f, food2MeshRenderer);
                CheckAndDisableMeshRenderer(10f, food3MeshRenderer);
                CheckAndDisableMeshRenderer(11f, otherMeshRenderer); // 마지막 오브젝트
            }
        }
    }

    public void BtnToEat()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            animationTimer = 0f;
            anim.SetBool("isEating", true);
            Debug.Log("BtnToEat method called: Animation started, isEating set to true");

            // 모든 MeshRenderer를 다시 활성화
            EnableMeshRenderer(otherMeshRenderer);
            EnableMeshRenderer(food1MeshRenderer);
            EnableMeshRenderer(food2MeshRenderer);
            EnableMeshRenderer(food3MeshRenderer);

            // Eating 시작 시점으로 타이머 초기화
            //StartCoroutine(StartEatingAfterDelay(0f)); // 0.5초 후에 isEating 시작

            isEatingStarted = true;
            Debug.Log("Eating started.");
        }
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

    private void CheckAndDisableMeshRenderer(float time, MeshRenderer renderer)
    {
        if (animationTimer >= time && renderer != null && renderer.enabled)
        {
            renderer.enabled = false;
            Debug.Log($"MeshRenderer of {renderer.gameObject.name} has been disabled at {time} seconds.");
        }
    }

    private void EndAnimation()
    {
        anim.SetBool("isEating", false);
        animationTimer = 0f;
        isAnimating = false;
        isEatingStarted = false;
        Debug.Log("Animation completed: isEating reset to false");
    }

    private void EnableMeshRenderer(MeshRenderer renderer)
    {
        if (renderer != null)
        {
            renderer.enabled = true;
            Debug.Log($"MeshRenderer of {renderer.gameObject.name} has been enabled.");
        }
    }
    */

}