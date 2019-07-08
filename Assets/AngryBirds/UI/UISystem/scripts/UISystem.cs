using System.Collections;
using System.Collections.Generic;
using UnityEngine;


////主要用于控制场景中的UI，控制是否显示
//public class UISystem : MonoBehaviour,IControllable {

//    [SerializeField]
//    private Menu pauseMenu;
//    [SerializeField]
//    private Menu winMenu;
//    [SerializeField]
//    private Menu failMenu;

//    private Menu activeMenu;

//    void Start() {
//        activeMenu = null;

//    }

//    void Activate(Existence<Menu> m) {
//        if (activeMenu != null)
//            activeMenu.Inactivate() ;

//        m.Unwrap().Activate();
//        this.activeMenu = m.Unwrap();
//    }


//    public void ShowPause() {
//        this.Activate(new Existence<Menu>(pauseMenu));
//    }


//    public void ShowWin() {
//        this.Activate(new Existence<Menu>(winMenu));
//    }


//    public void ShowFail() {
//        this.Activate(new Existence<Menu>(failMenu));
//    }


//    //在主循环里被调用，获取输入并进行反应
//    public void GetInput() {
//        Debug.Log("UI system in control");
//    }
//}
