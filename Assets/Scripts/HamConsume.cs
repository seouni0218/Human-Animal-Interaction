using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamConsume : MonoBehaviour
{
    GameObject[] portions;
    int currentIdx;
    bool play=true;
    //float interval = 1f;
    GameObject cat;

    //public GameObject hamClick; // hamClick : 클릭용 햄
    public GameObject ham;
    public GameObject heart;
    //public GameObject gamescript;

    // Start is called before the first frame update
    void Start()
    {
        // 고양이 객체 찾기
        cat = GameObject.FindWithTag("CAT");

        // ham portions 초기화
        bool skipFirst = transform.childCount > 4;
        portions = new GameObject[skipFirst ? transform.childCount - 1 : transform.childCount];
        for(int i=0; i<portions.Length; i++)
        {
            portions[i] = transform.GetChild(skipFirst ? i + 1 : i).gameObject;
            if (portions[i].activeInHierarchy)
                currentIdx = i;
        }
        currentIdx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            HamConsumer();
            play = false;
        }
    
    }

    private void OnEnable()
    {
        play = true;
        portions[0].SetActive(true);
        portions[1].SetActive(true);
        portions[2].SetActive(true);
        portions[3].SetActive(true);
    }

    void HamConsumer()
    {
        if (gameObject.activeSelf == true)
        {
            cat.GetComponent<Animator>().SetTrigger("eat");

            if (currentIdx < 4)
            {
                StartCoroutine("test1");
            }
        }

    }

    IEnumerator test1()
    {
        yield return new WaitForSeconds(0.5f);
        portions[0].SetActive(false);

        yield return new WaitForSeconds(1.0f);
        portions[1].SetActive(false);

        yield return new WaitForSeconds(1.5f);
        portions[2].SetActive(false);

        yield return new WaitForSeconds(2.0f);
        portions[3].SetActive(false);

        yield return new WaitForSeconds(2.1f);
        
        heart.SetActive(true);
        cat.GetComponent<Animator>().SetTrigger("eat_end");           
        ham.SetActive(false);

        //gamescript.GetComponent<MotionController>().isEat = true;
        //ham.GetComponent<HamConsume>().enabled = false;
    }
}
