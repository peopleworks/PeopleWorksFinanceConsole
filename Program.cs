using Microsoft.Extensions.Configuration;
using PeopleWorksFinanceConsole.Helpers;
using PeopleWorksFinanceConsole.Services.Models;

var argsDict = CommandParser.Parse(args);

if(!argsDict.TryGetValue("endpoint", out var endpoint))
{
    Console.WriteLine("Debe especificar el parámetro --endpoint (captaciones, cartera, estado, indicadores)");
    return;
}

string inicio = argsDict.GetValueOrDefault("inicio", "2023-01");
string fin = argsDict.GetValueOrDefault("fin", DateTime.Now.ToString("yyyy-MM"));
bool guardarEnBD = argsDict.TryGetValue("guardarbd", out var val) && val.ToLower() == "true";

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

string apiKey = config["ApiKey"];
string connectionString = config["ConnectionString"];

var api = new ApiService(apiKey);
var db = new DatabaseService(connectionString);

switch(endpoint.ToLower())
{
    case "entidades":
        var catalogo = Entidad.GetCatalogo();
        db.PoblarCatalogoEntidades(catalogo);
        Console.WriteLine("✅ Catálogo de entidades insertado correctamente.");
        break;


    case "captaciones":
        inicio = argsDict.GetValueOrDefault("inicio", "2023-01");
        fin = argsDict.GetValueOrDefault("fin", "2024-12");
        var persona = argsDict.GetValueOrDefault("persona", "todos");

        var captaciones = await api.ObtenerCaptacionesAsync(inicio, fin, persona);
        db.InsertCaptaciones(captaciones);

        Console.WriteLine($"✅ Proceso finalizado: {captaciones.Count} captaciones insertadas.");
        break;

    case "carteras":
        inicio = argsDict.GetValueOrDefault("inicio", "2023-01");
        fin = argsDict.GetValueOrDefault("fin", "2024-12");

        var cart = await api.ObtenerCarterasAsync(inicio, fin);
        Console.WriteLine($"📊 Carteras obtenidas: {cart.Count}");
        ExportService.SaveAsJson(cart, $"carteras_{inicio}_{fin}.json");
        ExportService.SaveAsCsv(cart, $"carteras_{inicio}_{fin}.csv");
        if(guardarEnBD)
            db.InsertCarteras(cart);
        break;

    case "estados":

        inicio = argsDict.GetValueOrDefault("inicio", "2023-01");
        fin = argsDict.GetValueOrDefault("fin", "2024-12");

        var estados = await api.ObtenerEstadosSituacionAsync(inicio, fin);
        Console.WriteLine($"📊 Estados situación obtenidos: {estados.Count}");
        ExportService.SaveAsJson(estados, $"estados_{inicio}_{fin}.json");
        ExportService.SaveAsCsv(estados, $"estados_{inicio}_{fin}.csv");
        if(guardarEnBD)
            db.InsertEstadosFinancieros(estados);
        break;

    case "indicadores":
        inicio = argsDict.GetValueOrDefault("inicio", "2023-01");
        fin = argsDict.GetValueOrDefault("fin", "2024-12");

        var ind = await api.ObtenerIndicadoresFinancierosAsync(inicio, fin);
        Console.WriteLine($"📊 Indicadores financieros: {ind.Count}");
        ExportService.SaveAsJson(ind, $"indicadores_{inicio}_{fin}.json");
        ExportService.SaveAsCsv(ind, $"indicadores_{inicio}_{fin}.csv");
        if(guardarEnBD)
            db.InsertIndicadores(ind);
        break;

    default:
        Console.WriteLine("❌ Endpoint desconocido. Usa: entidades, captaciones, cartera, estado, indicadores");
        break;
}
