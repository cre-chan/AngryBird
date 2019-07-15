using System;
using UnityEngine;
using IControllable = Controllers.IControllable;
using Controller = Controllers.Controller;


namespace UI.InGameUIs
{
    public class Win : Menu, IControllable
    {

        public void GetInput() { }

        public void BindController(Controller controller){ /*nothing,the state is irrecoverable*/ }

        public void NextLevel(){
            try
            {

                LevelLoader.LoadNextLevel();
                Time.timeScale = 1.0f;
            }
            catch (IndexOutOfRangeException)
            {
                Debug.Log("You succeeded!!!!");

            }
        }

        //重置本关
        public void ResetScene()
        {
            Time.timeScale = 1.0f;
            LevelLoader.ReLoadScene();
        }

    }
}