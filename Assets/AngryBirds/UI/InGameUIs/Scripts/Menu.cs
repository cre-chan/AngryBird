using System;
using UnityEngine;
using UnityEngine.UI;



namespace UI.InGameUIs
{
    //本文件内的函数均为UI控件触发的函数，功能重叠请在公共脚本LevelLoader里合并！
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField]
        private Image confirm;
        // Use this for initialization
        void Awake()
        {
            Debug.Log(Time.timeScale + "timescale"); /////////////////////////////////////////////////////////////////////////////////////////
        }


        //打开窗口
        public void OpenMenu()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                Debug.Log(gameObject.name + "已激活");
                Pause();
            }
        }


        //暂停游戏
        public void Pause()
        {
            Time.timeScale = 0;
            Debug.Log("游戏暂停");
        }


        //退出游戏
        public void Quit()
        {
            confirm.gameObject.SetActive(true);
        }

    }
}
