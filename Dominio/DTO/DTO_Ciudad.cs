namespace Dominio.DTO
{
    public class DTO_Ciudad
    {
        public DTO_Ciudad()
        {
            //Viajes = new HashSet<Viajes>();
        }
        public int IdCiudad { get; set; }
        public int IdProvincia { get; set; }
        public string Ciudad { get; set; }
        public int? IdOpenWeather { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string? Descripcion { get; set; }

        public DTO_Provincia Provincia { get; set; }
        //public virtual ICollection<DTO_Viaje> Viajes { get; set; }
    }
}
