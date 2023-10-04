using System.ComponentModel.DataAnnotations;

namespace apiSqlserver.Models.ModelsDto
{
    public class LibroCreatedDto
    {
        [Required]
        [StringLength(80)]
        public string NombreLib { get; set; } = null!;
        [Required]
        public int TipoId { get; set; }
        [Required]
        public int? Edicion { get; set; }
        [Required]
        public int Año { get; set; }
        [Required]
        [StringLength(80)]
        public string? Editorial { get; set; }
        [Required]
        public List<int> Autor { get; set; }
    }
}
