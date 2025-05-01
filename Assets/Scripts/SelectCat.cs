using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCat : MonoBehaviour
{
    GameObject blackcat;
    GameObject graycat;
    GameObject tigercat;
    GameObject cat;

    Vector3 init = new Vector3(0.0f, -2.0f, 5.0f);

    //int isFirst=0;

    public GameObject heart;
    //public GameObject cube;
    public bool selected;

    // Start is called before the first frame update
    void Start()
    {
        blackcat = GameObject.Find("cat_black");
        graycat = GameObject.Find("cat_gray");
        tigercat = GameObject.Find("cat_tiger");

        selected = false;
    }

    // Update is called once per frame
    void Update()
    {   

    }

    public void SelectBlack()
    {
        if (selected)
        {
            blackcat.GetComponent<Animator>().SetTrigger("happy");
            heart.SetActive(true);
            blackcat.GetComponent<AudioSource>().Play();
        }
        else
        {
            // gray, tiger 고양이 지우기
            Destroy(graycat);
            Destroy(tigercat);

            // Tag 설정하기
            blackcat.tag = "CAT";

            selected = true;
        }

    }

    public void SelectGray()
    {
        if (selected)
        {
            graycat.GetComponent<Animator>().SetTrigger("happy");
            heart.SetActive(true);
            graycat.GetComponent<AudioSource>().Play();
        }
        else
        {
            // black, tiger 고양이 지우기
            Destroy(blackcat);
            Destroy(tigercat);

            // Tag 설정하기
            graycat.tag = "CAT";

            selected = true;

        }

    }

    public void SelectTiger()
    {
        if (selected)
        {
            tigercat.GetComponent<Animator>().SetTrigger("happy");
            heart.SetActive(true);
            tigercat.GetComponent<AudioSource>().Play();
        }
        else
        {
            // black, gray 고양이 지우기
            Destroy(blackcat);
            Destroy(graycat);

            // Tag 설정하기
            tigercat.tag = "CAT";

            selected = true;
        }

    }
}
