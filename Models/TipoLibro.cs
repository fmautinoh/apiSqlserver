using System;
using System.Collections.Generic;

namespace apiSqlserver.Models;

public partial class TipoLibro
{
    public int TipoLibroId { get; set; }

    public string TipoLibro1 { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
