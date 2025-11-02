using System;
using System.Collections.Generic;

namespace personapi_dotnet.Models.Entities;

public partial class Persona
{
    public int Cc { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public int Edad { get; set; }

    public virtual ICollection<Estudio> Estudios { get; } = new List<Estudio>();

    public virtual ICollection<Telefono> Telefonos { get; } = new List<Telefono>();
}
