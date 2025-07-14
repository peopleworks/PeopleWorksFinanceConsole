namespace PeopleWorksFinanceConsole.Services.Models;

public class Entidad
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? TipoEntidad { get; set; }

    public static List<Entidad> GetCatalogo()
    {
        return new List<Entidad>
        {
            new() { Nombre = "BANCO POPULAR DOMINICANO, C. POR A.", TipoEntidad = "BANCO MÚLTIPLE" },
            new() { Nombre = "BANCO BHD, S. A.", TipoEntidad = "BANCO MÚLTIPLE" },
            new() { Nombre = "BANCO DE RESERVAS DE LA REPÚBLICA DOMINICANA", TipoEntidad = "BANCO MÚLTIPLE" },
            new() { Nombre = "BANCO SANTA CRUZ, S. A.", TipoEntidad = "BANCO MÚLTIPLE" },
            new() { Nombre = "BANCO PROMERICA DE LA REPÚBLICA DOMINICANA, S. A.", TipoEntidad = "BANCO MÚLTIPLE" },
            new() { Nombre = "BANCO CARIBE INTERNACIONAL, S. A.", TipoEntidad = "BANCO MÚLTIPLE" },
            new() { Nombre = "BANCO LAFISE, S. A.", TipoEntidad = "BANCO MÚLTIPLE" },
            new() { Nombre = "BANCO VIMENCA, S. A.", TipoEntidad = "BANCO MÚLTIPLE" },
            new() { Nombre = "BANCO MOCANO, S. A.", TipoEntidad = "BANCO MÚLTIPLE" },
            new() { Nombre = "BANCO BACC, S. A.", TipoEntidad = "BANCO MÚLTIPLE" },
            new() { Nombre = "BANCO FICOHSA, S. A.", TipoEntidad = "BANCO MÚLTIPLE" },
            new() { Nombre = "BANCO UNI", TipoEntidad = "BANCO MÚLTIPLE" },
            new() { Nombre = "ASOCIACIÓN POPULAR DE AHORROS Y PRÉSTAMOS", TipoEntidad = "ASOCIACIÓN DE AHORROS Y PRÉSTAMOS" },
            new() { Nombre = "ASOCIACIÓN CIBAO DE AHORROS Y PRÉSTAMOS", TipoEntidad = "ASOCIACIÓN DE AHORROS Y PRÉSTAMOS" },
            new() { Nombre = "ASOCIACIÓN DUARTE DE AHORROS Y PRÉSTAMOS", TipoEntidad = "ASOCIACIÓN DE AHORROS Y PRÉSTAMOS" },
            new() { Nombre = "ASOCIACIÓN LA VEGA REAL DE AHORROS Y PRÉSTAMOS", TipoEntidad = "ASOCIACIÓN DE AHORROS Y PRÉSTAMOS" },
            new() { Nombre = "ASOCIACIÓN PERAVIA DE AHORROS Y PRÉSTAMOS", TipoEntidad = "ASOCIACIÓN DE AHORROS Y PRÉSTAMOS" },
            new() { Nombre = "BANCO AGRÍCOLA DE LA REPÚBLICA DOMINICANA", TipoEntidad = "BANCO DE DESARROLLO" },
            new() { Nombre = "BANCO NACIONAL DE LAS EXPORTACIONES (BANDEX)", TipoEntidad = "BANCO DE DESARROLLO" },
            new() { Nombre = "BANCO DE DESARROLLO Y EXPORTACIONES, S. A.", TipoEntidad = "BANCO DE DESARROLLO" }
        };
    }
}

public class CaptacionDetalle
{
    public string? periodo { get; set; }
    public string? entidad { get; set; }
    public string? tipoEntidad { get; set; }
    public string? region { get; set; }
    public string? provincia { get; set; }
    public string? persona { get; set; }
    public string? genero { get; set; }
    public string? instrumentoCaptacion { get; set; }
    public string? moneda { get; set; }
    public string? divisa { get; set; }
    public decimal balance { get; set; }
    public decimal tasaPrimedioPonderadoPorBalance { get; set; }
    public decimal tasaPrimedioPonderado { get; set; }
}

public class CarteraDetalle
{
    public string? periodo { get; set; }
    public string? entidad { get; set; }
    public string? tipoEntidad { get; set; }
    public string? tipoCredito { get; set; }
    public string? sectorEconomico { get; set; }
    public string? region { get; set; }
    public string? provincia { get; set; }
    public string? moneda { get; set; }
    public decimal deuda { get; set; }
    public decimal tasaPorDeuda { get; set; }
    public decimal deudaCapital { get; set; }
}

public class EstadoDetalle
{
    public string? periodo { get; set; }
    public string? entidad { get; set; }
    public string? tipoEntidad { get; set; }
    public string? conceptoNivel1 { get; set; }
    public string? conceptoNivel2 { get; set; }
    public string? conceptoNivel3 { get; set; }
    public decimal valor { get; set; }
}

public class IndicadorDetalle
{
    public string? periodo { get; set; }
    public string? entidad { get; set; }
    public string? tipoEntidad { get; set; }
    public string? indicador { get; set; }
    public string? tipoIndicador { get; set; }
    public decimal valor { get; set; }
    public string? unidad { get; set; }
}
