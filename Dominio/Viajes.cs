using System;

namespace Dominio
{
    public class Viajes
    {
        public int IdViaje { get; set; }
        public int IdCiudad { get; set; }
        public string IdTipoVehiculos { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreReserva { get; set; }
    }
}
