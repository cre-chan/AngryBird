using UnityEngine;
using UnityEngine.SceneManagement;


//这个脚本极端有害，如有可能尽早处理
public static class Main
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void StartUp()
    {
        uint startMenuIndex = (uint)(SceneManager.sceneCountInBuildSettings-1);
        SceneManager.LoadScene((int)startMenuIndex);
         
    }
}
