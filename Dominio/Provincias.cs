using System.Collections.Generic;

namespace Dominio
{
    public class Provincias
    {
        public Provincias()
        {
            Ciudades = new HashSet<Ciudades>();
        }
        public int IdProvincia { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Ciudades> Ciudades { get; set; }
    }
}
