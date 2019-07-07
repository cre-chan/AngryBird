using System;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.SceneManagement;
namespace Assets.AngryBirds.UI.levelloaders
{
    class LevelLoader
    {
        private int levelNum;//关卡数量
        private List<string> levelList = new List<string>();//储存所有关卡的信息
        private int levelRange;//所有的场景里是关卡的数量。默认从0到levelNum-1是关卡，levelNum是主界面

        public LevelLoader()
        {
            levelNum = SceneManager.sceneCountInBuildSettings;

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
                    Debug.Log("已添加关卡:" + sceneName);
                }
                else
                {
                    Debug.Log("获取关卡错误，head=" + head + "end=" + end);
                }
                ;

            }



        }

        public bool Load(int index)//按照index来加载
        {
            if (index >= levelRange || index < 0)
            {
                Debug.Log("加载失败，关卡数越界");
                return false;
            }
            else
            {
              
                SceneManager.LoadScene(index);
                Debug.Log("按index加载成功:");
                return true;
            }
        }

        public bool Load(string name)//还用不了，没法把路径中的文件名提取
        {

            if (levelList.Contains(name))
            {
                SceneManager.LoadScene(name);
                Debug.Log("按名字加载成功:");
                return true;
            }
            else
            {
                Debug.Log("加载失败，不存在该名字");
                return false;
            }
        }


        public void ReLoadScene()
        {
            int sceneIndex;
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            Load(sceneIndex);
            Time.timeScale = 1.0f;
        }

        public void Quit()
        {
            Application.Quit();
            Debug.Log("退出游戏");
        }

        public void LoadNextLevel()
        {
            int currLevelIndex = SceneManager.GetActiveScene().buildIndex;
            int nextLevelIndex = currLevelIndex + 1;
            Load(nextLevelIndex);


        }
    }
}
