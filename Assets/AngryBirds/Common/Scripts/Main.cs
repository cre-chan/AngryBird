﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.AngryBirds.UI.levelloaders;

namespace Assets.AngryBirds.Common.Scripts
{
    public static class Main
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void StartUp()
        {
            uint startMenuIndex = (uint)(SceneManager.sceneCountInBuildSettings-1);
            SceneManager.LoadScene((int)startMenuIndex);
         
        }
    }

       
}