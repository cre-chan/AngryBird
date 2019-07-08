using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyControl : MonoBehaviour {
    [SerializeField]
    private Dummy scene;
    [SerializeField]
    private Pause pause;
    [SerializeField]
    private Win win;
    [SerializeField]
    private Fail fail;

    private Controller activeControl;
    private bool sceneAtControl;

	// Use this for initialization
	void Start () {
        activeControl = Controller.From<Dummy>(
            new Existence<Dummy>( scene));
        scene.BindController(activeControl);
        pause.BindController(activeControl);
        win.BindController(activeControl);
        fail.BindController(activeControl);
	}
	
	// Update is called once per frame
	void Update () {
        //调用活跃的控制器，控制器内部会自动处理状态转换
        activeControl.GetInput();

	}
}
