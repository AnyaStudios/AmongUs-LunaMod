using HarmonyLib;
using TMPro;

namespace LunaMod.Patches.Utilities;

[HarmonyPatch(typeof(VersionShower), "Start")]
public static class VersionShowerPatch
{
    public static void Postfix(VersionShower __instance)
    {
        __instance.text.alignment = TextAlignmentOptions.TopJustified;
        __instance.text.fontSize = 2;
        __instance.text.text += $"\nLunaMod {LunaModPlugin.modVersion} - Made By <color=#00ffc8>AnyaStudios</color>";
    }
}
