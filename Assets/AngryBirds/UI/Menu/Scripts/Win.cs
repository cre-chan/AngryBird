using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : Menu,IControllable {

    public IControllable GetInput() {
        return null;
    }

    public void BindController(Controller controller)
    {
        //nothing,the state is irrecoverable
    }

}
