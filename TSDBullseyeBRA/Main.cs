using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using UnityEngine;

namespace TSDBullseyeBRA
{
    public class Main : VTOLMOD
    {
        public override void ModLoaded()
        {
            Logging.Log("Mod loaded");

            HarmonyInstance harmony = HarmonyInstance.Create("xyz.031410.tsdbullseyebra");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            base.ModLoaded();
        }
    }
}