using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apiSqlserver.Models.ModelsDto
{
    public class AutorCreatedDto
    {
        [Required]
        [StringLength(80)]
        public string NombreAutor { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(TipoAutorId))]
        public int TipoAutorId { get; set; }
    }
}
