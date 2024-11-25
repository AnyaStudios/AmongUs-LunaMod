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
public class LunaModPlugin : BasePlugin
{
    public const string Id = "me.anyastudios.lunamod";
    public const string modVersion = "1.0.0";

    public const string devModVersion = "1.0.1-dev";

    public Harmony Harmony { get; } = new(Id);

    public override void Load()
    {
        LunaLogger.Message("LunaMod is now loading!");
        LunaLogger.Message($"Current Development Version: {devModVersion}");

        ModConfig.Bind(Config);

        ReactorCredits.Register<LunaModPlugin>(ReactorCredits.AlwaysShow);
        Harmony.PatchAll();
    }
}
