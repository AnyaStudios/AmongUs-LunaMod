using Discord;
using HarmonyLib;
using UnityEngine;

namespace LunaMod.Modules;

[HarmonyPatch(typeof(ActivityManager), nameof(ActivityManager.UpdateActivity))]
public static class DiscordStatus
{
    public static void Prefix([HarmonyArgument(0)] Activity activity)
    {
        if (activity == null) return;

        var isBeta = false;
        int maxPlayers = GameOptionsManager.Instance.currentNormalGameOptions.MaxPlayers;
        var lobbyCode = GameStartManager.Instance.GameRoomNameCode.text;
        var platform = Application.platform;

        string details = $"LunaMod v0.5" + " | " + (isBeta ? " (Beta)" : " (Dev)");
        string state = $"Players: {maxPlayers} | LobbyCode {lobbyCode} | Platform: {platform}";

        activity.Details = details;
        activity.State = state;
    }
}
