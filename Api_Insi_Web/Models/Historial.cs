using System;
using System.Collections.Generic;

namespace Api_Insi_Web.Models;

public partial class Historial
{
    public DateTime? Fecha { get; set; }

    public TimeSpan? Hora { get; set; }

    public string? Descripcion { get; set; }

    public string? Usuario { get; set; }

    public int? IdMatricula { get; set; }
}
