using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using LunaMod.Utilities;
using Reactor;

namespace LunaMod;

[BepInPlugin(Id, "LunaMod", modVersion)]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
public class LunaModPlugin : BasePlugin
{
    public const string Id = "me.anyastudios.lunamod";
    public const string modVersion = "0.4";

    public Harmony Harmony { get; } = new(Id);

    public override void Load()
    {
        LunaLogger.Message("LunaMod is now loading!");
        Harmony.PatchAll();
    }
}
