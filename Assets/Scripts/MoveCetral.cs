using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCetral : MonoBehaviour
{
    public GameObject heart;

    GameObject cat;
    Vector3 init = new Vector3(0,-0.24f,1);
    public bool isFirst=true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //isFirst = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirst)
        {
            
            if (gameObject.name == "cat_tiger")
            {
                GetComponent<Animator>().SetTrigger("walk");
                transform.eulerAngles = new Vector3(0, 270, 0);
                transform.position = Vector3.MoveTowards(transform.position, init, 0.004f);
            }
            else if (gameObject.name == "cat_gray")
            {
                GetComponent<Animator>().SetTrigger("walk");
                transform.eulerAngles = new Vector3(0, 90, 0);
                transform.position = Vector3.MoveTowards(transform.position, init, 0.004f);
            }
            else
            {
                // black cat은 이미 중앙에 위치해있음
            }

            if (transform.position == init)
            {
                if (gameObject.name != "cat_black")
                {
                    GetComponent<Animator>().SetTrigger("walk_end");
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                GetComponent<Animator>().SetTrigger("happy");
                GetComponent<AudioSource>().Play();
                heart.SetActive(true);

                isFirst = false;
                GetComponent<MoveCetral>().enabled = false;
            }
        }
        else
        {
            GetComponent<Animator>().SetTrigger("walk_end");
            //GetComponent<MoveCetral>().enabled = false;
        }

    }
}
