using BepInEx.Configuration;

namespace LunaMod;

internal static class ModConfig
{
    public const string GeneralSection = "General";
    public const string RoleTrackingSection = "Role Tracking";

    // General Config
    public static ConfigEntry<bool> DarkMode { get; private set; }

    // Role Tracking Config
    public static ConfigEntry<bool> ShowTeam { get; private set; }
    public static ConfigEntry<bool> ShowRole { get; private set; }

    public static void Bind(ConfigFile configFile)
    {
        DarkMode = configFile.Bind(GeneralSection, "DarkMode", true, "Give your eyes a rest instead of having a csgo flashbang");

        ShowTeam = configFile.Bind(RoleTrackingSection, "ShowTeam", true, "Shows you the team of every player when you die");
        ShowRole = configFile.Bind(RoleTrackingSection, "ShowRole", true, "Shows you the role of every player when you die\nShowTeam needs to be turned on");
    }
}