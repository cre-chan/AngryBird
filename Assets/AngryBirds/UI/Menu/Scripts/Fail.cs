using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fail : Menu,IControllable {


    // Use this for initialization
    public IControllable GetInput()
    {
        return null;//do nothing and do not transit
    }

    public void BindController(Controller controller) {

    }
}

