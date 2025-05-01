using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCommand : MonoBehaviour
{
    public GameObject hamClick;
    public GameObject fishClick;
    public GameObject hamConsume;
    public GameObject fishConsume;

    GameObject cat;


    // Start is called before the first frame update
    void Start()
    {
        cat = GameObject.FindWithTag("CAT");
    }

    // Update is called once per frame
    void Update()
    {
        if (hamConsume.activeSelf == false && fishConsume.activeSelf==false)
        {
            cat.GetComponent<Animator>().SetTrigger("eat_end");
        }

    }

    public void SelectFood()
    {
        hamClick.SetActive(true);
        fishClick.SetActive(true);
    }

    public void SelectHam()
    {
        // fish 비활성화
        fishClick.SetActive(false);
        hamConsume.SetActive(true);

        // 고양이 먹는 애니메이션 출력
        //cat.GetComponent<Animator>().SetTrigger("eat");
        //ham.GetComponent<>
    }

    public void SelectFish()
    {
        // ham 비활성화
        hamClick.SetActive(false);
        fishConsume.SetActive(true);

        // 고양이 먹는 애니메이션 출력
        //cat.GetComponent<Animator>().SetTrigger("eat");
    }
}
