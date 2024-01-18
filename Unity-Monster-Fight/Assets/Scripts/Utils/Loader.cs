using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Utils
{
    public static class Loader
    {
        public enum Scene
        {
            SCN_Menu,
            SCN_Game,
        }

        private static Action onLoaderCallback;
        
        public static void Load(Scene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }
    }
}