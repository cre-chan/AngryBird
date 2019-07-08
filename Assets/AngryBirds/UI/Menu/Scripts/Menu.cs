using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.AngryBirds.UI.levelloaders;

//本文件内的函数均为UI控件触发的函数，功能重叠请在公共脚本LevelLoader里合并！
public class Menu : MonoBehaviour
{

    private LevelLoader levelLoader;

    // Use this for initialization
    void Awake()
    {
        levelLoader = new LevelLoader();
        Debug.Log(Time.timeScale + "timescale"); /////////////////////////////////////////////////////////////////////////////////////////
    }


    //打开窗口
    public void Activate()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            Debug.Log(gameObject.name+"已激活");
            Pause();
        }
    }

   
    //在esc界面激活的情况下点继续关闭暂停界面
    public void Inactivate()
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            Debug.Log(gameObject.name + "已关闭");
            Resume();
        }
    }


    //在游戏暂停时调用，将游戏恢复到暂停前的速率
    public void  Resume()
    {
        Time.timeScale = 1.0f;
        this.Inactivate();
        Debug.Log("游戏继续");
    }

    //暂停游戏
    public void Pause()
    {
        Time.timeScale = 0;
        Debug.Log("游戏暂停");
    }

    //重置本关
    public void ResetScene()
    {
        Time.timeScale = 1.0f;
        levelLoader.ReLoadScene();
    }
    //退出游戏
    public void Quit()
    {
        levelLoader.Quit();
    }

    public void NextLevel()
    {
        Time.timeScale = 1.0f;
        levelLoader.LoadNextLevel();
        
    }


}
