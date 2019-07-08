using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWinOrFialController : MonoBehaviour {


    public GameObject WinMenu;
    public GameObject FailMenu;
	// Use this for initialization
	void Start () {
        WinMenu.SetActive(false);
        FailMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("a"))
        {
            WinMenu.GetComponent<Menu>().Activate();
        }
        else if(Input.GetKey("d"))
        {
            FailMenu.GetComponent<Menu>().Activate();
        }
    }
}
