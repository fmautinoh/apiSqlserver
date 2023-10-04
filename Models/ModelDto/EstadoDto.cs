namespace apiSqlserver.Models.ModelsDto
{
    public class EstadoDto
    {
        public int Estadoid { get; set; }

        public string Descripcion { get; set; } = null!;

        public int Valor { get; set; }

        public string Color { get; set; } = null!;
    }
}
