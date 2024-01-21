using System;
using UnityEngine;

namespace Game.Utils
{
    public class StringFormat : MonoBehaviour
    {
        public static string TimerToString(float timer)
        {
            return TimeSpan.FromSeconds(timer).ToString("ss\\.fff");
        }
    }
}