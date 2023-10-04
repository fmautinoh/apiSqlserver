using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace apiSqlserver.Models;
[Keyless]
public partial class VAutor
{
    public int AutorId { get; set; }

    public int TipoAutorId { get; set; }

    public string NombreAutor { get; set; } = null!;

    public string TipoAutor { get; set; } = null!;
}
