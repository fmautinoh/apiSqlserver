using System;
using System.Collections.Generic;

namespace apiSqlserver.Models;

public partial class Autore
{
    public int AutorId { get; set; }

    public string NombreAutor { get; set; } = null!;

    public int TipoAutorId { get; set; }

    public virtual ICollection<LibrosAutore> LibrosAutores { get; set; } = new List<LibrosAutore>();

    public virtual TipoAutor TipoAutor { get; set; } = null!;
}
