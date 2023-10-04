using System;
using System.Collections.Generic;

namespace apiSqlserver.Models;

public partial class VInvReporte
{
    public string Codigo { get; set; } = null!;

    public string NombreLib { get; set; } = null!;

    public string? Autores { get; set; }

    public int? Edicion { get; set; }

    public string? Editorial { get; set; }

    public int Año { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Autenticidad { get; set; } = null!;

    public int Valor { get; set; }

    public int LibroId { get; set; }
}
