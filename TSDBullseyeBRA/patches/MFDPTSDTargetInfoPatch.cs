using Harmony;
using UnityEngine;
using UnityEngine.UI;

namespace TSDBullseyeBRA.patches
{
    [HarmonyPatch(typeof(MFDPTacticalSituationDisplay), "UpdateTargetInfo")]
    class MFDPTSDTargetInfoPatch
    {
        public static void Postfix(ref Text ___braNumsText, MeasurementManager ___measurements, TacticalSituationController.TSActorTargetInfo aInfo)
        {
            //Logging.Log("Started UpdateTargetInfo Patch");
            WaypointManager wpManager = WaypointManager.instance;
            if (!wpManager)
            {
                Logging.Log("WaypointManager.instance was null");
                return;
            }

            float rawBearing;
            float rawRange;
            float rawAltitude;

            wpManager.GetBullsBRA(aInfo.estimatedPosition, out rawBearing, out rawRange, out rawAltitude);

            //Logging.Log($"Got BRA, {rawBearing} {rawRange} {rawAltitude}");

            int bearing = Mathf.RoundToInt(rawBearing);

            int range = Mathf.RoundToInt(___measurements.ConvertedDistance(rawRange));
            if (___measurements.distanceMode == MeasurementManager.DistanceModes.Feet || ___measurements.distanceMode == MeasurementManager.DistanceModes.Meters)
            {
                range /= 1000;
            }

            int altitude = Mathf.RoundToInt(___measurements.ConvertedAltitude(rawAltitude) / 1000f);

            //Logging.Log($"Got converted BRA, {bearing} {range} A{altitude}");

            ___braNumsText.text = $"{bearing}\n{range}\n{altitude}";

            //Logging.Log("Completed UpdateTargetInfo Patch");
        }
    }
}
