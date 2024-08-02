using System;
using UnityEngine;

namespace TSDBullseyeBRA
{
    internal class Logging
    {
        public static void Log(object msg)
        {
            DateTime now = DateTime.Now;
            String isoTime = now.ToString("o");
            Debug.Log($"[TSDBullseyeBRA] [{isoTime}]: {msg}");
        }
    }
}
