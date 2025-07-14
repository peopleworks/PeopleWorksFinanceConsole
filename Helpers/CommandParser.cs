namespace PeopleWorksFinanceConsole.Helpers;

public static class CommandParser
{
    public static Dictionary<string, string> Parse(string[] args)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var arg in args)
        {
            if (!arg.StartsWith("--")) continue;

            var parts = arg[2..].Split('=', 2);
            if (parts.Length == 2)
                result[parts[0]] = parts[1];
        }

        return result;
    }
}
