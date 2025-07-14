using System.Globalization;
using System.Text;
using System.Text.Json;

namespace PeopleWorksFinanceConsole.Helpers;

public static class ExportService
{
    public static void SaveAsJson<T>(List<T> data, string filename)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, json);
        Console.WriteLine($"💾 Exportado a {filename}");
    }

    public static void SaveAsCsv<T>(List<T> data, string filename)
    {
        if (data == null || data.Count == 0)
        {
            Console.WriteLine("⚠️ No hay datos para exportar.");
            return;
        }

        var props = typeof(T).GetProperties();
        var sb = new StringBuilder();
        sb.AppendLine(string.Join(",", props.Select(p => p.Name)));

        foreach (var item in data)
        {
            var values = props.Select(p => FormatValue(p.GetValue(item))).ToArray();
            sb.AppendLine(string.Join(",", values));
        }

        File.WriteAllText(filename, sb.ToString());
        Console.WriteLine($"💾 Exportado CSV a {filename}");
    }

    private static string FormatValue(object? val)
    {
        return val switch
        {
            null => "",
            double d => d.ToString("0.##", CultureInfo.InvariantCulture),
            DateTime dt => dt.ToString("yyyy-MM-dd"),
            _ => val.ToString()?.Replace(",", " ") ?? ""
        };
    }
}
