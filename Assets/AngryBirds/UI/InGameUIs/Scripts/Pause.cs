using Utility;
using UnityEngine;
using Controllers;


namespace UI.InGameUIs
{
    public class Pause : Menu, IControllable
    {
        [SerializeField]
        private CentralControl scene;


        private Controller controller;
        public void GetInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                this.Resume();

        }

        public void Resume()
        {
            //use this to call the Resume in base class

            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                Time.timeScale = 1.0f;
                Debug.Log(gameObject.name + "已关闭");
            }
            controller.BindsTo(new Existing<CentralControl>(scene));
        }

        //重置本关
        public void ResetScene()
        {
            Time.timeScale = 1.0f;
            LevelLoader.ReLoadScene();
        }

        public void BindController(Controller controller)
        {
            this.controller = controller;
        }
    }
}

