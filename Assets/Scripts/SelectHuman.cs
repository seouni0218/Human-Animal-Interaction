using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHuman : MonoBehaviour
{
    //public GameObject Julia;
    public GameObject Terence;
    public GameObject Natasha;
    public GameObject Robyn;
    //public GameObject Jenny;

    //public MovieTexture

    // Start is called before the first frame update
    void Start()
    {
        //Jenny.GetComponent<
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectTerence()
    {
        //Destroy(Julia);
        //Destroy(Natasha);
        Natasha.SetActive(false);
        Robyn.SetActive(false);
        //Destroy(Robyn);
        //Destroy(Jenny);

        //중앙으로 위치 이동
        Terence.transform.position = new Vector3(-0.1f,-0.75f,2.926f);
    }

    public void SelectNatasha()
    {
        //Destroy(Julia);
        Terence.SetActive(false);
        Robyn.SetActive(false);
        //Destroy(Jenny);

        //중앙으로 위치 이동 (0,-400,850)
        Natasha.transform.position = new Vector3(-0.1f, -0.75f, 2.926f);
    }

    public void SelectRobyn()
    {
        //Destroy(Julia);
        Terence.SetActive(false);
        Natasha.SetActive(false);
        //Destroy(Jenny);

        //중앙으로 위치 이동
        Robyn.transform.position = new Vector3(-0.1f, -0.75f, 2.926f);
    }
}
