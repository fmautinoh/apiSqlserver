using System;
using System.Collections.Generic;

namespace apiSqlserver.Models;

public partial class EstadoConservacion
{
    public int EstadoId { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Valor { get; set; }

    public string Color { get; set; } = null!;

    public virtual ICollection<InventarioLibro> InventarioLibros { get; set; } = new List<InventarioLibro>();
}
