using System;
using System.Collections.Generic;

namespace apiSqlserver.Models;

public partial class TipoAutor
{
    public int TipoAutorId { get; set; }

    public string tipoautor { get; set; } = null!;

    public virtual ICollection<Autore> Autores { get; set; } = new List<Autore>();
}
