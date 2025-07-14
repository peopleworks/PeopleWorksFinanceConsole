using PeopleWorksFinanceConsole.Services.Models;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

public class ApiService
{
    private readonly HttpClient _http;
    private static readonly string[] TiposEntidadOficiales = new[]
    {
        "AAyP",   // Asociaciones de Ahorros y Préstamos
        "BAyC",   // Bancos de Ahorro y Crédito
        "BM",     // Bancos Múltiples
        "CC",     // Corporaciones de Crédito
        "EP"      // Entidades Públicas de Intermediación Financiera
    };

    public ApiService(string apiKey)
    {
        _http = new HttpClient();
        _http.BaseAddress = new Uri("https://apis.sb.gob.do/estadisticas/v2/");
        _http.DefaultRequestHeaders.Add("apikey", apiKey);
    }

    public async Task<List<Entidad>> ObtenerInstitucionesAsync()
    {
        var tiposEntidad = TiposEntidadOficiales;
        var resultados = new List<Entidad>();

        foreach (var tipo in tiposEntidad)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"detalle-entidades/acceso?periodoInicial=2023-01&tipoEntidad={tipo}");

            request.Headers.Add("User-Agent", "Mozilla/5.0");
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("apikey", _http.DefaultRequestHeaders.GetValues("apikey").First());
            request.Headers.Add("Ocp-Apim-Subscription-Key", _http.DefaultRequestHeaders.GetValues("apikey").First());

            try
            {
                var response = await _http.SendAsync(request);
                var body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var lista = JsonSerializer.Deserialize<List<Entidad>>(
                        body,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (lista != null)
                        resultados.AddRange(lista);
                }
                else
                {
                    Console.WriteLine($"⚠️ {tipo}: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en tipo {tipo}: {ex.Message}");
            }
        }

        return resultados.DistinctBy(e => e.Nombre).ToList();
    }

    public async Task<List<CaptacionDetalle>> ObtenerCaptacionesAsync(string inicio, string fin, string persona = "todos")
    {
        var tiposEntidad = TiposEntidadOficiales;
        var resultados = new List<CaptacionDetalle>();

        string personaParam = persona.ToLower() switch
        {
            "fisica" => "Persona física",
            "juridica" => "Persona jurídica",
            _ => ""
        };

        foreach (var tipo in tiposEntidad)
        {
            int pagina = 1;
            while (true)
            {
                var url = $"captaciones/detalle?periodoInicial={inicio}&periodoFinal={fin}&tipoEntidad={tipo}&registros=1000&paginas={pagina}";
                if (!string.IsNullOrWhiteSpace(personaParam))
                    url += $"&persona={Uri.EscapeDataString(personaParam)}";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("User-Agent", "Mozilla/5.0");
                request.Headers.Add("accept", "application/json");
                request.Headers.Add("apikey", _http.DefaultRequestHeaders.GetValues("apikey").First());
                request.Headers.Add("Ocp-Apim-Subscription-Key", _http.DefaultRequestHeaders.GetValues("apikey").First());

                try
                {
                    var response = await _http.SendAsync(request);
                    var body = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var lista = JsonSerializer.Deserialize<List<CaptacionDetalle>>(body, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (lista == null || !lista.Any())
                        {
                            if (pagina == 1)
                                Console.WriteLine($"⚠️ Sin datos para tipoEntidad: {tipo}");
                            break;
                        }

                        resultados.AddRange(lista);
                        Console.WriteLine($"✅ {tipo} página {pagina}: {lista.Count} captaciones.");

                        if (lista.Count < 1000)
                            break; // No hay más páginas

                        pagina++;
                        await Task.Delay(250); // espera 250ms para evitar bloqueos
                    }
                    else
                    {
                        Console.WriteLine($"❌ {tipo} página {pagina}: {response.StatusCode} - {body}");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error en {tipo} página {pagina}: {ex.Message}");
                    break;
                }
            }
        }

        return resultados;
    }


    public async Task<List<CarteraDetalle>> ObtenerCarterasAsync(string inicio, string fin)
    {
        var tiposEntidad = TiposEntidadOficiales;
        var resultados = new List<CarteraDetalle>();

        foreach (var tipo in tiposEntidad)
        {
            int pagina = 1;
            while (true)
            {
                var url = $"carteras/creditos?periodoInicial={inicio}&periodoFinal={fin}&tipoEntidad={tipo}&registros=1000&paginas={pagina}";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("User-Agent", "Mozilla/5.0");
                request.Headers.Add("accept", "application/json");
                request.Headers.Add("apikey", _http.DefaultRequestHeaders.GetValues("apikey").First());
                request.Headers.Add("Ocp-Apim-Subscription-Key", _http.DefaultRequestHeaders.GetValues("apikey").First());

                try
                {
                    var response = await _http.SendAsync(request);
                    var body = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var lista = JsonSerializer.Deserialize<List<CarteraDetalle>>(body, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (lista == null || !lista.Any())
                        {
                            if (pagina == 1)
                                Console.WriteLine($"⚠️ Sin datos cartera para tipoEntidad: {tipo}");
                            break;
                        }

                        resultados.AddRange(lista);
                        Console.WriteLine($"✅ Carteras {tipo} página {pagina}: {lista.Count} registros.");

                        if (lista.Count < 1000)
                            break;

                        pagina++;
                        await Task.Delay(250);
                    }
                    else
                    {
                        Console.WriteLine($"❌ {tipo} página {pagina}: {response.StatusCode} - {body}");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error en cartera {tipo} página {pagina}: {ex.Message}");
                    break;
                }
            }
        }

        return resultados;
    }

    public async Task<List<EstadoDetalle>> ObtenerEstadosSituacionAsync(string inicio, string fin)
    {
        var tiposEntidad = TiposEntidadOficiales;
        var resultados = new List<EstadoDetalle>();

        foreach (var tipo in tiposEntidad)
        {
            int pagina = 1;
            while (true)
            {
                var url = $"estados/situacion/eif?periodoInicial={inicio}&periodoFinal={fin}&tipoEntidad={tipo}&registros=1000&paginas={pagina}";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("User-Agent", "Mozilla/5.0");
                request.Headers.Add("accept", "application/json");
                request.Headers.Add("apikey", _http.DefaultRequestHeaders.GetValues("apikey").First());
                request.Headers.Add("Ocp-Apim-Subscription-Key", _http.DefaultRequestHeaders.GetValues("apikey").First());

                try
                {
                    var response = await _http.SendAsync(request);
                    var body = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var lista = JsonSerializer.Deserialize<List<EstadoDetalle>>(body, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (lista == null || !lista.Any())
                        {
                            if (pagina == 1)
                                Console.WriteLine($"⚠️ Sin datos estado para tipoEntidad: {tipo}");
                            break;
                        }

                        resultados.AddRange(lista);
                        Console.WriteLine($"✅ Estado situación {tipo} página {pagina}: {lista.Count} registros.");

                        if (lista.Count < 1000)
                            break;

                        pagina++;
                        await Task.Delay(250);
                    }
                    else
                    {
                        Console.WriteLine($"❌ {tipo} página {pagina}: {response.StatusCode} - {body}");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error estado {tipo} página {pagina}: {ex.Message}");
                    break;
                }
            }
           
        }

        return resultados;
    }


    public async Task<List<IndicadorDetalle>> ObtenerIndicadoresFinancierosAsync(string inicio, string fin)
    {
        var tiposEntidad = TiposEntidadOficiales;
        var resultados = new List<IndicadorDetalle>();

        foreach (var tipo in tiposEntidad)
        {
            int pagina = 1;
            while (true)
            {
                var url = $"indicadores/financieros?periodoInicial={inicio}&periodoFinal={fin}&tipoEntidad={tipo}&registros=1000&paginas={pagina}";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("User-Agent", "Mozilla/5.0");
                request.Headers.Add("accept", "application/json");
                request.Headers.Add("apikey", _http.DefaultRequestHeaders.GetValues("apikey").First());
                request.Headers.Add("Ocp-Apim-Subscription-Key", _http.DefaultRequestHeaders.GetValues("apikey").First());

                try
                {
                    var response = await _http.SendAsync(request);
                    var body = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var lista = JsonSerializer.Deserialize<List<IndicadorDetalle>>(body, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (lista == null || !lista.Any())
                        {
                            if (pagina == 1)
                                Console.WriteLine($"⚠️ Sin datos indicadores para tipoEntidad: {tipo}");
                            break;
                        }

                        resultados.AddRange(lista);
                        Console.WriteLine($"✅ Indicadores {tipo} página {pagina}: {lista.Count} registros.");

                        if (lista.Count < 1000)
                            break;

                        pagina++;
                        await Task.Delay(250);
                    }
                    else
                    {
                        Console.WriteLine($"❌ {tipo} página {pagina}: {response.StatusCode} - {body}");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error indicador {tipo} página {pagina}: {ex.Message}");
                    break;
                }
            }

        }
        return resultados;

    }
}
