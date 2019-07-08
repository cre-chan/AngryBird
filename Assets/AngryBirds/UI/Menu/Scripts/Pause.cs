using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause: Menu,IControllable{
    [SerializeField]
    private Dummy scene;

    private Controller controller;
    public IControllable GetInput() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            this.Resume();
            return scene;
        }

        Debug.Log("Pause Menu");

        return null;
    }

    public new void  Resume() {
        //use this to call the Resume in base class
        ((Menu)this).Resume();
        controller.BindsTo(new Existence<Dummy>(scene));
    }

    public void BindController(Controller controller)
    {
        this.controller = controller;
    }
}

