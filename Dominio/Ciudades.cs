using System.Collections.Generic;

namespace Dominio
{
    public class Ciudades
    {
        public Ciudades()
        {
            Viajes = new HashSet<Viajes>();
        }
        public int IdCiudad { get; set; }
        public int IdProvincia { get; set; }
        public string Ciudad { get; set; }
        public int? IdOpenWeather { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string? Descripcion { get; set; }

        public Provincias Provincia { get; set; }
        public virtual ICollection<Viajes> Viajes { get; set; }
    }
}
