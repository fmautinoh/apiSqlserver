using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace apiSqlserver.Models;
[Keyless]
public partial class VInventario
{
    public int LibroId { get; set; }

    public int InventarioId { get; set; }

    public int EstadoId { get; set; }

    public int AutenticidadId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Autenticidad { get; set; } = null!;

    public int Valor { get; set; }

    public string Color { get; set; } = null!;
}
