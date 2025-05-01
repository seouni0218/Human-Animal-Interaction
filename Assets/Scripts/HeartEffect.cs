using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartEffect : MonoBehaviour
{
    public GameObject heart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("test1");
    }

    IEnumerator test1()
    {
        yield return new WaitForSeconds(2.0f);
        if (heart.activeSelf == true)
            heart.SetActive(false);
    }
}
