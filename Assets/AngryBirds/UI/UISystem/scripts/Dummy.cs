using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour,IControllable {
    [SerializeField]
    private Pause pause;
    [SerializeField]
    private Win win;
    [SerializeField]
    private Fail fail;

    private Controller controller;

    public IControllable GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            this.pause.Activate();
            controller.BindsTo(new Existence<Pause>(pause));
            return this.pause;
        }

        Debug.Log("Dummy");

        return null;
    }

    public void BindController(Controller controller)
    {
        this.controller = controller;
    }

}
