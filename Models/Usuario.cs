using System;
using System.Collections.Generic;

namespace apiSqlserver.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Usu { get; set; } = null!;

    public string Pwsd { get; set; } = null!;

    public int TipousuarioId { get; set; }

    public virtual TipoUsuario Tipousuario { get; set; } = null!;
}
