using Reactor.Utilities;

namespace LunaMod.Utilities;

public static class LunaLogger
{
    public static void Debug(string message)
    {
        Logger<LunaModPlugin>.Debug(message);
    }

    public static void Info(string message)
    {
        Logger<LunaModPlugin>.Info(message);
    }

    public static void Message(string message)
    {
        Logger<LunaModPlugin>.Message(message);
    }

    public static void Warn(string message)
    {
        Logger<LunaModPlugin>.Warning(message);
    }

    public static void Error(string message)
    {
        Logger<LunaModPlugin>.Debug(message);
    }

    public static void Fatal(string message)
    {
        Logger<LunaModPlugin>.Fatal(message);
    }
}
