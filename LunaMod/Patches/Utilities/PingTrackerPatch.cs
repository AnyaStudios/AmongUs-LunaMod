using HarmonyLib;
using UnityEngine;

namespace LunaMod.Patches.Utilities;

[HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
public static class PingTrackerPatch
{
    [HarmonyPostfix]
    public static void Postfix(PingTracker __instance)
    {
        var position = __instance.GetComponent<AspectPosition>();
        position.DistanceFromEdge = new Vector3(3.6f, 0.1f, 0);
        position.AdjustPosition();

        __instance.aspectPosition.DistanceFromEdge = new Vector3(4f, 0.1f, -5);
        __instance.aspectPosition.Alignment = AspectPosition.EdgeAlignments.RightTop;

        __instance.text.text =
            "<size=100%>Made By <color=#429E9D>AnyaStudios</color>\n" +
            $"LunaMod {LunaModPlugin.modVersion}\n" +
            $"Ping: {AmongUsClient.Instance?.Ping}ms";
        __instance.text.outlineWidth = 0.3f;
    }
}
