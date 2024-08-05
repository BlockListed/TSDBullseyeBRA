using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace TSDBullseyeBRA.patches
{
    class MFDPTSDTargetInfoPatch
    {
        [HarmonyPatch(typeof(MFDPTacticalSituationDisplay), "UpdateTargetInfo")]
        [HarmonyPostfix]
        public static void SetBullseyeBRA(ref Text ___braNumsText, MeasurementManager ___measurements, TacticalSituationController.TSActorTargetInfo aInfo)
        {
            //Logger.Log("Started UpdateTargetInfo Patch");
            WaypointManager wpManager = WaypointManager.instance;
            if (!wpManager)
            {
                Logger.Log("WaypointManager.instance was null");
                return;
            }

            float rawBearing;
            float rawRange;
            float rawAltitude;

            wpManager.GetBullsBRA(aInfo.estimatedPosition, out rawBearing, out rawRange, out rawAltitude);

            //Logger.Log($"Got BRA, {rawBearing} {rawRange} {rawAltitude}");
            int bearing = Mathf.RoundToInt(rawBearing);

            int range = Mathf.RoundToInt(___measurements.ConvertedDistance(rawRange));
            if (___measurements.distanceMode == MeasurementManager.DistanceModes.Feet || ___measurements.distanceMode == MeasurementManager.DistanceModes.Meters)
            {
                range /= 1000;
            }

            int altitude = Mathf.RoundToInt(___measurements.ConvertedAltitude(rawAltitude) / 1000f);

            //Logger.Log($"Got converted BRA, {bearing} {range} A{altitude}");

            ___braNumsText.text = $"{bearing}\n{range}\n{altitude}";

            //Logger.Log("Completed UpdateTargetInfo Patch");
        }
    }
}
