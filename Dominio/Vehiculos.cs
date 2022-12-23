namespace Dominio
{
    public class Vehiculos
    {
        public int IdVehiculo { get; set; }
        public int IdTipoVehiculo { get; set; }
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Comentarios { get; set; }

        public VehiculosTipoVehiculo TipoVehiculo { get; set; }
    }
}
