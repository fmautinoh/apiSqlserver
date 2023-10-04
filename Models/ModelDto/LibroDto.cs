using System.ComponentModel.DataAnnotations;

namespace apiSqlserver.Models.ModelsDto
{
    public class LibroDto
    {
        [Required]
        public int LibroId { get; set; }
        [Required]
        [StringLength(80)]
        public string NombreLib { get; set; } = null!;
        [Required]
        public int TipoId { get; set; }
        [Required]
        public string TipoLibro { get; set; }
        [Required]
        public int? Edicion { get; set; }
        [Required]
        public int Año { get; set; }
        [Required]
        [StringLength(80)]
        public string? Editorial { get; set; }
        [Required]
        public List<int> AutorId { get; set; }
        [Required]
        public List<string> NombreAutor { get; set; }
    }
}
