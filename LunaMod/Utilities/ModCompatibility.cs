namespace LunaMod.Utilities;

internal static class ModCompatibility
{
    public static bool ShouldTurnOffTracking { get; private set; }
    public static bool IsToUR { get; private set; }
    public static bool IsTOR { get; private set; }
    public static bool IsStellar { get; private set; }

    public static void Load()
    {
        LunaLogger.Message($"Mod Compatibility Loading :: Checking for mods...");

        ShouldTurnOffTracking = IsToUR || IsTOR || IsStellar;
        LunaLogger.Info($"Mod Compatibility Loading :: ShouldTurnOffTracking: {ShouldTurnOffTracking}");
        LunaLogger.Info($"Mod Compatibility Loading :: All checks completed!");
    }
}
