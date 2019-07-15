using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;




namespace UI.StartupMenus
{

    public class StartMenuController : MonoBehaviour
    {

        public GameObject SelectLevelMenu;

        // Use this for initialization
        void Start()
        {
            //强制载入，以保证游戏存档被载入
            LevelRecordLoader.ForceLoad();
        }


        //从levelloader和levelrecordloader中获得关卡的信息
        //进入选择关卡界面
        public void SelectLevel()
        {
            SelectLevelMenu.SetActive(true);
            this.gameObject.SetActive(false);
        }

        //退出游戏
        public void Quit()
        {
            Application.Quit();
            Debug.Log("退出游戏");
        }

        //继续上一次的关卡，调用了另一个界面的函数。因为我把和关卡有关的都放在另一个menu上了。
        public void ContinueLevel()
        {
            var last_level = LevelRecordLoader.GetInstance().curLevel;
            LevelLoader.Load(last_level);
        }
    }
}
