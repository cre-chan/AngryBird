using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




static class LevelLoader
{
    private static int levelNum;//scene总数量
    private static int levelRange;//关卡scene数量。默认从0到levelNum-1是关卡，levelNum是主界面
    private static  List<string> levelList = new List<string>();//储存所有关卡的名字


    public static int LevelRange
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
            LevelRecordLoader.GetInstance().curLevel = index;
            Debug.Log("按index加载成功:");
        }
    }



    //重新加载scene
    public static void ReLoadScene()
    {
        Load(GetCurIndex());
    }

    //退出游戏
    public static void Quit()
    {
        Debug.Log("退出游戏");
        LevelRecordLoader.GetInstance().SaveLevelRecord();
        LevelRecordLoader.GetInstance().ShowAllLevelRecord();
        Application.Quit();
            
    }

    //加载下一关
    public static void LoadNextLevel()
    {
        Load(GetCurIndex() + 1);
    }

    //获得当前屏幕的index
    public static uint GetCurIndex()
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

    static public Nullable<uint> GetNextLevelIndex() {
        var index_next = GetCurIndex()+1;
        return index_next >= levelRange ?new uint?() : index_next;
    }

}

