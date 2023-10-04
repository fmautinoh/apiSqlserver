using System;
using System.Collections.Generic;

namespace apiSqlserver.Models;

public partial class TipoUsuario
{
    public int TipousuarioId { get; set; }

    public string Tipousuario { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
