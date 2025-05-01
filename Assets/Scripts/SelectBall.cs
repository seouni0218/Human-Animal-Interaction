using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBall : MonoBehaviour
{
    //Vector3 target;

    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        //target = new Vector3(0, 0, 0);
        ball.transform.position = new Vector3(0,0,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init_ball()
    {
        ball.transform.position = new Vector3(0, 0, 0.5f);
    }

    /*public void throwball()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 0.02f);
        transform.Rotate(1, 1, 0);
    }*/
}
