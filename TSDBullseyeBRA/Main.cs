global using static TSDBullseyeBRA.Logger;
using System.Reflection;
using ModLoader.Framework;
using ModLoader.Framework.Attributes;
using HarmonyLib;
using TSDBullseyeBRA.patches;

namespace TSDBullseyeBRA
{
    [ItemId("xyz.031410.tsdbullseyebra")] // Harmony ID for your mod, make sure this is unique
    public class Main : VtolMod
    {
        public string ModFolder;

        private void Awake()
        {
            ModFolder = Assembly.GetExecutingAssembly().Location;
            Log($"Awake at {ModFolder}");

            Harmony.CreateAndPatchAll(typeof(MFDPTSDTargetInfoPatch));
        }

        public override void UnLoad()
        {
            // Destroy any objects
        }
    }
}