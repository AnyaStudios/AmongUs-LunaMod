using System;
using Discord;
using HarmonyLib;
using LunaMod.Utilities;
using UnityEngine;

namespace LunaMod.Modules;

[HarmonyPatch(typeof(ActivityManager), nameof(ActivityManager.UpdateActivity))]
public static class DiscordStatus
{
    public static void Prefix([HarmonyArgument(0)] Activity activity)
    {
        if (activity == null) return;

        var isBeta = false;

        string details = $"LunaMod v0.7" + " | " + (isBeta ? " (Beta)" : " (Dev)");
        try
        {
            if (activity.State == "In Menus")
            {
                int maxPlayers = GameOptionsManager.Instance.currentNormalGameOptions.MaxPlayers;
                var lobbyCode = GameStartManager.Instance.GameRoomNameCode.text;

                details += $" Players: {maxPlayers} | LobbyCode {lobbyCode}";
            }
            else if (activity.State == "In Game")
            {
                if (MeetingHud.Instance)
                {
                    details += " | \nIn Meeting";
                }
            }
            activity.Assets.SmallText = "LunaMod Made By AnyaStudios";
        }
        catch (Exception e)
        {
            LunaLogger.Error($"Error updating Discord activity: {e.Message}\nStackTrace {e.StackTrace}");
        }
    }
}
