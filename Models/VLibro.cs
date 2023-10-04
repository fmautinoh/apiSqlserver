using apiSqlserver.Models.ModelsDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using apiSqlserver.Models.ModelsDto;

namespace apiSqlserver.Models;

public partial class VLibro
{
    public int LibroId { get; set; }

    public int AutorId { get; set; }

    public int TipoLibroId { get; set; }

    public string NombreLib { get; set; } = null!;

    public string TipoLibro { get; set; } = null!;

    public string NombreAutor { get; set; } = null!;

    public int? Edicion { get; set; }

    public int Año { get; set; }

    public string? Editorial { get; set; }
    [NotMapped] // Exclude this property from database mapping
    public List<AutorDtosList> AutoresIds { get; set; }
}
