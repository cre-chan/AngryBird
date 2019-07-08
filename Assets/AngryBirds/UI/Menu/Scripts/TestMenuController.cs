using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMenuController : MonoBehaviour {

    public GameObject PauseMenu;
    // Use this for initialization
    void Start () {
        PauseMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey("a"))
        {
            PauseMenu.GetComponent<Menu>().Active();
        }
		
	}
}
