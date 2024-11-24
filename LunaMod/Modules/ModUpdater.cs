using System.Collections;
using System.IO;
using BepInEx;
using BepInEx.Unity.IL2CPP.Utils;
using HarmonyLib;
using LunaMod.Utilities;
using Newtonsoft.Json.Linq;
using Twitch;
using UnityEngine;
using UnityEngine.Networking;

namespace LunaMod.Modules;

[HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
public class ModUpdater
{
    private static readonly string GithubApiUrl = "https://api.github.com/repos/AnyaStudios/AmongUs-LunaMod/releases/latest";
    private static string latestVersion = "";
    private static string downloadUrl = "";
    public static GenericPopup InfoPopup;
    private static readonly string modPath = Path.Combine(Paths.PluginPath, "LunaMod.dll");

    private static void Postfix(MainMenuManager __instance)
    {
        TwitchManager man = DestroyableSingleton<TwitchManager>.Instance;
        InfoPopup = Object.Instantiate(man.TwitchPopup);
        __instance.StartCoroutine(CheckForUpdatesWithUnity(__instance));
    }

    public static IEnumerator CheckForUpdatesWithUnity(MainMenuManager __instance)
    {
        UnityWebRequest request = UnityWebRequest.Get(GithubApiUrl);
        request.SetRequestHeader("User-Agent", "request");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var json = JObject.Parse(request.downloadHandler.text);
            latestVersion = json["tag_name"].ToString();
            downloadUrl = json["assets"][0]["browser_download_url"].ToString();

            if (latestVersion != LunaModPlugin.modVersion)
            {
                LunaLogger.Warn($"[LunaMod] has a new version: {latestVersion} available! Downloading newest version...");
                InfoPopup.Show("Updated LunaMod!");
                string tempPath = Path.Combine(Application.persistentDataPath, "LunaMod_new.dll");
                __instance.StartCoroutine(DownloadUpdate(downloadUrl, tempPath));
            }
            else
            {
                LunaLogger.Message("[LunaMod] Plugin is now up to date!");
            }
        }
        else
        {
            LunaLogger.Error($"Sadly, [LunaMod] Update check failed: {request.error}");
        }
    }

    public static IEnumerator DownloadUpdate(string downloadUrl, string tempFilePath)
    {
        UnityWebRequest request = UnityWebRequest.Get(downloadUrl);
        request.SetRequestHeader("User-Agent", "request");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            File.WriteAllBytes(tempFilePath, request.downloadHandler.data);
            LunaLogger.Message("[LunaMod] has downloaded the newest version to a temp file!");

            ReplaceOldMod(tempFilePath);
            NotifyRestart();
        }
        else
        {
            LunaLogger.Error($"Sadly, [LunaMod] Failed to download the update: {request.error}");
        }
    }

    private static void ReplaceOldMod(string tempFilePath)
    {
        try
        {
            string backupPath = modPath + ".bak";
            if (File.Exists(modPath))
            {
                File.Move(modPath, backupPath, true);
            }
            File.Move(tempFilePath, modPath, true);
            LunaLogger.Message("[LunaMod] Updated the mod file successfully!");
        }
        catch (IOException ex)
        {
            LunaLogger.Error($"Sadly, [LunaMod] Failed to replace the mod file: {ex.Message}");
        }
    }

    private static void NotifyRestart()
    {
        LunaLogger.Info("[LunaMod] Update complete! Please restart the game to apply the newest version.");
        InfoPopup.Show("Update completed! Please restart the game.\nIf this shows again after restarting. Press confirm to continue");
    }
}
