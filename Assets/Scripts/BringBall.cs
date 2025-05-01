using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringBall : MonoBehaviour
{
    GameObject cat;
    GameObject ball;
    Transform tr;
    Animator anim;
    Vector3 init = new Vector3(0,-0.24f, 1);
    Vector3 ball_init;
    Vector3 ball_mid;
    Vector3 target;

    public GameObject ballClick;
    public GameObject heart;
    //public bool iswalk=false;
    public int meet;
    //public int ball_meet=0;

    int top=0;

    // Start is called before the first frame update
    void Start()
    {
        cat = GameObject.FindWithTag("CAT");
        ball = cat.transform.GetChild(2).gameObject;

        //tr = cat.GetComponent<Transform>();
        anim = cat.GetComponent<Animator>();
        target = new Vector3(0, -0.24f, 2);

        meet = 0;
    }

    void OnEnable()
    {
        meet = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // throwing a ball
        //ballClick.transform.position = target;
        if (meet == 0)
        {
            ballClick.SetActive(false);
            anim.SetTrigger("walk");
            cat.transform.eulerAngles = new Vector3(0, 0, 0);
            cat.transform.position = Vector3.MoveTowards(cat.transform.position, target, 0.004f);
        }

        if (cat.transform.position == target)
        {
            meet = 1;
            cat.transform.eulerAngles = new Vector3(0, 180, 0);
            ball.SetActive(true);
        }

        if (meet == 1)
        {
            cat.transform.position = Vector3.MoveTowards(cat.transform.position, init, 0.004f);
        }

        if(cat.transform.position==init && meet == 1)
        {
            anim.SetTrigger("walk_end");
            ball.SetActive(false);
            heart.SetActive(true);
            meet = 2;

            GetComponent<BringBall>().enabled = false;
        }

        /*if (meet == 1)
        {
            cat.transform.position = Vector3.MoveTowards(cat.transform.position, init, 0.004f);
        }

        if (cat.transform.position == init && meet==2)
        {
            anim.SetTrigger("walk_end");
            ball.SetActive(false);
            heart.SetActive(true);
            meet = 3;
        }*/

        // throwing a ball
        /*ballClick.transform.position = Vector3.MoveTowards(ballClick.transform.position, mid, 0.01f);
        if (ballClick.transform.position== mid)
        {
            top = 1;
        }

        if (top == 1)
        {
            ballClick.transform.position = Vector3.MoveTowards(ballClick.transform.position, target, 0.006f);
        }

        if (ballClick.transform.position == target)
        {
            meet = 0;
            anim.SetTrigger("walk");
        }*/
        //ballClick.transform.Rotate(1, 1, 0);

        /*if (meet==0)
            cat.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        

        if (meet == 0)
            cat.transform.position = Vector3.MoveTowards(cat.transform.position, target, 0.006f);

        if(cat.transform.position== ballClick.transform.position && meet == 0)
        {
            // 고양이와 공 충돌
            meet = 1;
            cat.transform.Rotate(0, 180, 0);
            meet = 2;
        }

        if (meet == 2)
        {
            //ball_init = new Vector3(0, 0, 0);
            //ball_mid = new Vector3(0, 0, 0);

            if (ball_meet == 0)
            {
                ballClick.SetActive(false);
                ball.SetActive(true);
                ball_meet = 1;
            }
            cat.transform.position = Vector3.MoveTowards(cat.transform.position, init, 0.004f);

            if (cat.transform.position == init)
            {
                anim.SetTrigger("walk_end");
                ball_meet = 2;
                ball.SetActive(false);
                ball_meet = 3;
            }
        }

        if (meet == 3)
            anim.SetTrigger("walk_end");*/
    }
}
