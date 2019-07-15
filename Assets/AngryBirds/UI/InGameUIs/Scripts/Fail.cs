using UnityEngine;
using IControllable = Controllers.IControllable;
using Controller = Controllers.Controller;


namespace UI.InGameUIs
{
    public class Fail : Menu, IControllable
    {


        // Use this for initialization
        public void GetInput() { /*do nothing and do not transit */}

        public void BindController(Controller controller)
        {

        }

        //重置本关
        public void ResetScene()
        {
            Time.timeScale = 1.0f;
            LevelLoader.ReLoadScene();
        }
    }
}

