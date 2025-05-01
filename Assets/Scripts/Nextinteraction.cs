using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextinteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //ToInteraction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToInteraction()
    {
        SceneManager.LoadScene("cat_interaction");
    }
}
