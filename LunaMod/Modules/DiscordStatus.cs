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
        string details = $"LunaMod v0.3" + " | " + (isBeta ? " (Beta)" : " (Dev)");
        activity.Details = details;

        try
        {
            if (activity.State == "In Menus")
            {
                int maxPlayers = GameOptionsManager.Instance.currentNormalGameOptions.MaxPlayers;
                var lobbyCode = GameStartManager.Instance.GameRoomNameCode.text;
                var platform = Application.platform;

                details += $" Players: {maxPlayers} | LobbyCode {lobbyCode} | Platform: {platform}";
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
