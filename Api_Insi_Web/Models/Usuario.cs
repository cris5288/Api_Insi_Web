using System;
using System.Collections.Generic;

namespace Api_Insi_Web.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }
   
    public string? Email { get; set; }

    public string? Contraseña { get; set; }

    public byte[]? ContraseñaEncriptada { get; set; }
}
