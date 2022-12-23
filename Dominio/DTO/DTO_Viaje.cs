using System;

namespace Dominio.DTO
{
    public class DTO_Viaje
    {
        public int IdViaje { get; set; }
        public int IdCiudad { get; set; }
        public string IdsVehiculos { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreReserva { get; set; }

        public DTO_Ciudad Ciudad { get; set; }
    }
}
