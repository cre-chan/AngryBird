using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyControl : MonoBehaviour {
    [SerializeField]
    private Dummy scene;
    [SerializeField]
    private UISystem ui;

    private IControllable activeControl;
    private bool sceneAtControl;

	// Use this for initialization
	void Start () {
        activeControl = scene as IControllable;
        sceneAtControl = false;
	}
	
	// Update is called once per frame
	void Update () {
        bool escPressed = Input.GetKeyDown(KeyCode.Escape);

        if (escPressed)
            if (sceneAtControl) {
                sceneAtControl = !sceneAtControl;
                activeControl = (IControllable)ui;
                ui.ShowPause();
            } else {
                sceneAtControl = !sceneAtControl;
                activeControl = (IControllable)scene;
            };

        activeControl.GetInput();
	}
}
