using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;
using Assets.AngryBirds.SaveFile.Scripts.SaveFile;


namespace Assets.AngryBirds.UI.levelloaders
{
    static class LevelLoader
    {
        private static int levelNum;//scene总数量
        private static int levelRange;//关卡scene数量。默认从0到levelNum-1是关卡，levelNum是主界面
        private static  List<string> levelList = new List<string>();//储存所有关卡的名字


        public static int LEVELRANGE
        {
            get { return levelRange; }
        }


        //构造函数
        static LevelLoader()
        {
            //检测当前buildindex的数量是否>1
            levelNum = SceneManager.sceneCountInBuildSettings;
            if (levelNum <= 1)
            {
                Debug.Log("buildindex的数量有问题");
                return;
            }

            levelRange = levelNum - 1;
            for (int i = 0; i < levelNum; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);//找到路径，找到对应的名字
                int head = scenePath.LastIndexOf("/") + 1;
                int end = scenePath.LastIndexOf(".");
                if (head < end)
                {
                    string sceneName = scenePath.Substring(head, end - head);
                    levelList.Add(sceneName);//添加对应的文件名字（不包括后缀)
                }
                else
                {
                    Debug.Log("获取关卡错误，head=" + head + "end=" + end);
                }

            }

        }

        //按照index来加载scene
        public static void Load(uint index)
        {
            if (index >= levelRange)
            {
                Debug.LogError("加载失败，关卡数越界");
                throw new IndexOutOfRangeException("加载失败，关卡数越界");
            }
            else
            {
              
                SceneManager.LoadScene((int)index);
                Debug.Log("按index加载成功:");
            }
        }

        public static void Load(string name)
        {
            if(!levelList.Contains(name))
            {
                Debug.LogError("加载失败，无此关卡名字");
                throw new IndexOutOfRangeException("加载失败，关卡数越界");
            }
            else
            {
                SceneManager.LoadScene(name);
                Debug.Log("按index加载成功:");
            }
        }

        //重新加载scene
        public static void ReLoadScene()
        {
            Load(GetCurrIndex());
        }

        //退出游戏
        public static void Quit()
        {
            Debug.Log("退出游戏");
            Application.Quit();
            
        }

        //加载下一关
        public static void LoadNextLevel()
        {
            Load(GetCurrIndex() + 1);
        }

        //获得当前屏幕的index
        public static uint GetCurrIndex()
        {
            return (uint)SceneManager.GetActiveScene().buildIndex;

        }

        public static string GetName(uint index)
        {
            if (index >= levelRange)
            {
                Debug.LogError("获得名字失败，关卡数越界");
                throw new IndexOutOfRangeException("获得名字失败，关卡数越界");
            }
            else
                return levelList[(int)index];
        }

    }
}
