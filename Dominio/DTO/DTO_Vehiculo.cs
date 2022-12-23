namespace Dominio.DTO
{
    public class DTO_Vehiculo
    {
        public int IdVehiculo { get; set; }
        public int IdTipoVehiculo { get; set; }
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Comentarios { get; set; }

        public DTO_VehiculoTipoVehiculo TipoVehiculo { get; set; }
    }
}
