using Microsoft.Data.SqlClient;
using PeopleWorksFinanceConsole.Services.Models;

namespace PeopleWorksFinanceConsole.Helpers;

public class DatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(string connectionString) { _connectionString = connectionString; }


    public void PoblarCatalogoEntidades(List<Entidad> entidades)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        foreach(var ent in entidades)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                IF NOT EXISTS (
                    SELECT 1 FROM Entidad WHERE Nombre = @Nombre AND TipoEntidad = @TipoEntidad
                )
                BEGIN
                    INSERT INTO Entidad (Nombre, TipoEntidad) VALUES (@Nombre, @TipoEntidad)
                END";

            cmd.Parameters.AddWithValue("@Nombre", ent.Nombre ?? "");
            cmd.Parameters.AddWithValue("@TipoEntidad", ent.TipoEntidad ?? "");
            cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"✅ Insertadas {entidades.Count} entidades del catálogo (solo nuevas).");

    }

    public void InsertCaptaciones(List<CaptacionDetalle> data)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        foreach(var item in data)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
INSERT INTO Captacion (Periodo, Entidad, TipoEntidad, Region, Provincia, Persona, Genero, InstrumentoCaptacion, Moneda, Divisa, Balance, TasaPromedioPonderadoPorBalance, TasaPromedioPonderado)
VALUES (@Periodo, @Entidad, @TipoEntidad, @Region, @Provincia, @Persona, @Genero, @Instrumento, @Moneda, @Divisa, @Balance, @TPPB, @TPP)";

            cmd.Parameters.AddWithValue("@Periodo", item.periodo ?? "");
            cmd.Parameters.AddWithValue("@Entidad", item.entidad ?? "");
            cmd.Parameters.AddWithValue("@TipoEntidad", item.tipoEntidad ?? "");
            cmd.Parameters.AddWithValue("@Region", item.region ?? "");
            cmd.Parameters.AddWithValue("@Provincia", item.provincia ?? "");
            cmd.Parameters.AddWithValue("@Persona", item.persona ?? "");
            cmd.Parameters.AddWithValue("@Genero", item.genero ?? "");
            cmd.Parameters.AddWithValue("@Instrumento", item.instrumentoCaptacion ?? "");
            cmd.Parameters.AddWithValue("@Moneda", item.moneda ?? "");
            cmd.Parameters.AddWithValue("@Divisa", item.divisa ?? "");
            cmd.Parameters.AddWithValue("@Balance", item.balance);
            cmd.Parameters.AddWithValue("@TPPB", item.tasaPrimedioPonderadoPorBalance);
            cmd.Parameters.AddWithValue("@TPP", item.tasaPrimedioPonderado);

            cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"💾 Insertadas {data.Count} captaciones en base de datos.");
    }

    public void InsertCarteras(List<CarteraDetalle> data)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        foreach(var item in data)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
INSERT INTO Cartera (Periodo, Entidad, TipoEntidad, TipoCredito, SectorEconomico, Region, Provincia, Moneda, Deuda, TasaPorDeuda, DeudaCapital)
VALUES (@Periodo, @Entidad, @TipoEntidad, @TipoCredito, @Sector, @Region, @Provincia, @Moneda, @Deuda, @Tasa, @Capital)";

            cmd.Parameters.AddWithValue("@Periodo", item.periodo ?? "");
            cmd.Parameters.AddWithValue("@Entidad", item.entidad ?? "");
            cmd.Parameters.AddWithValue("@TipoEntidad", item.tipoEntidad ?? "");
            cmd.Parameters.AddWithValue("@TipoCredito", item.tipoCredito ?? "");
            cmd.Parameters.AddWithValue("@Sector", item.sectorEconomico ?? "");
            cmd.Parameters.AddWithValue("@Region", item.region ?? "");
            cmd.Parameters.AddWithValue("@Provincia", item.provincia ?? "");
            cmd.Parameters.AddWithValue("@Moneda", item.moneda ?? "");
            cmd.Parameters.AddWithValue("@Deuda", item.deuda);
            cmd.Parameters.AddWithValue("@Tasa", item.tasaPorDeuda);
            cmd.Parameters.AddWithValue("@Capital", item.deudaCapital);

            cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"💾 Insertadas {data.Count} carteras en base de datos.");
    }

    public void InsertEstadosFinancieros(List<EstadoDetalle> data)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        foreach(var item in data)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
INSERT INTO EstadoFinanciero (Periodo, Entidad, TipoEntidad, ConceptoNivel1, ConceptoNivel2, ConceptoNivel3, Valor)
VALUES (@Periodo, @Entidad, @TipoEntidad, @N1, @N2, @N3, @Valor)";

            cmd.Parameters.AddWithValue("@Periodo", item.periodo ?? "");
            cmd.Parameters.AddWithValue("@Entidad", item.entidad ?? "");
            cmd.Parameters.AddWithValue("@TipoEntidad", item.tipoEntidad ?? "");
            cmd.Parameters.AddWithValue("@N1", item.conceptoNivel1 ?? "");
            cmd.Parameters.AddWithValue("@N2", item.conceptoNivel2 ?? "");
            cmd.Parameters.AddWithValue("@N3", item.conceptoNivel3 ?? "");
            cmd.Parameters.AddWithValue("@Valor", item.valor);

            cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"💾 Insertados {data.Count} estados financieros.");
    }

    public void InsertIndicadores(List<IndicadorDetalle> data)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        foreach(var item in data)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
INSERT INTO IndicadorFinanciero (Periodo, Entidad, TipoEntidad, Indicador, TipoIndicador, Valor, Unidad)
VALUES (@Periodo, @Entidad, @TipoEntidad, @Indicador, @TipoIndicador, @Valor, @Unidad)";

            cmd.Parameters.AddWithValue("@Periodo", item.periodo ?? "");
            cmd.Parameters.AddWithValue("@Entidad", item.entidad ?? "");
            cmd.Parameters.AddWithValue("@TipoEntidad", item.tipoEntidad ?? "");
            cmd.Parameters.AddWithValue("@Indicador", item.indicador ?? "");
            cmd.Parameters.AddWithValue("@TipoIndicador", item.tipoIndicador ?? "");
            cmd.Parameters.AddWithValue("@Valor", item.valor);
            cmd.Parameters.AddWithValue("@Unidad", item.unidad ?? "");

            cmd.ExecuteNonQuery();
        }

        Console.WriteLine($"💾 Insertados {data.Count} indicadores financieros.");
    }
}
