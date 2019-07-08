using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fail : Menu,IControllable {

    private Controller controller;

    // Use this for initialization
    public IControllable GetInput()
    {
        Debug.Log("Fail");
        return null;//do nothing and do not transit
    }

    public void BindController(Controller controller) {
        this.controller = controller;
    }
}

