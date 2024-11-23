using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using LunaMod.Utilities;
using Reactor;
using Reactor.Utilities;

namespace LunaMod;

[BepInPlugin(Id, "LunaMod", modVersion)]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
public partial class LunaModPlugin : BasePlugin
{
    public const string Id = "me.anyastudios.lunamod";
    public const string modVersion = "1.0";
    public static bool isDev = false;

    public static System.Version Version = System.Version.Parse(modVersion);
    public Harmony Harmony { get; } = new(Id);

    public override void Load()
    {
        LunaLogger.Message("LunaMod is now loading!");

        bool dev = CheckDevRelease().Result;
        isDev = dev;

        ReactorCredits.Register("LunaMod", modVersion + (dev ? "-indev" : ""), false, ReactorCredits.AlwaysShow);
    }

    public static async Task<bool> CheckDevRelease()
    {
        string url = $"https://github.com/AnyaStudios/AmongUs-LunaMod/tags";
        bool[] exists = await HttpVersionExists(url);
        LunaLogger.Info($"IsDevRelease: {!exists[0]} ({(exists[1] ? "success" : "fail")})");
        return !exists[0];
    }

    static async Task<bool[]> HttpVersionExists(string url)
    {
        bool found = false;
        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "C# App");
        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            using JsonDocument jsonDoc = JsonDocument.Parse(content);
            var root = jsonDoc.RootElement;
            foreach (var tag in root.EnumerateArray())
            {
                string name = tag.GetProperty("name").GetString();
                if (name == modVersion)
                {
                    found = true;
                    break;
                }
            }
            return [found, true];
        }
        catch (HttpRequestException ex)
        {
            LunaLogger.Error($"Failed To Check For Dev Release: {ex.Message}");
            return [false, false];
        }
    }
}
