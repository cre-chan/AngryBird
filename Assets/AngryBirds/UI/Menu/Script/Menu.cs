using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.AngryBirds.UI.levelloaders;

//本文件内的函数均为UI控件触发的函数，功能重叠请在公共脚本LevelLoader里合并！
public class Menu : MonoBehaviour
{

    private LevelLoader levelLoader;
    private float theWorldTime;
    // Use this for initialization
    void Start()
    {
        levelLoader = new LevelLoader();
        theWorldTime = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {

    }


    //在游戏暂停时调用，将游戏恢复到暂停前的速率
    public void Resume()
    {
        Time.timeScale = theWorldTime;
        Debug.Log("游戏继续");
    }

    //暂停游戏
    public void Pause()
    {
        theWorldTime = Time.timeScale;
        Time.timeScale = 0;
        Debug.Log("游戏暂停");
    }

    //重置本关
    public void Reset()
    {
        levelLoader.ReLoadScene();//这个reset函数名是蓝色的，我怀疑有问题/////////////////////
    }

    //退出游戏
    public void Quit()
    {
        levelLoader.Quit();
    }

}
