using System;
using System.Collections.Generic;

namespace apiSqlserver.Models;

public partial class LibrosAutore
{
    public int LibroAutorId { get; set; }

    public int LibroId { get; set; }

    public int AutorId { get; set; }

    public virtual Autore Autor { get; set; } = null!;

    public virtual Libro Libro { get; set; } = null!;
}
