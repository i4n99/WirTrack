using System.Collections.Generic;

namespace Dominio
{
    public class VehiculosTipoVehiculo
    {
        public VehiculosTipoVehiculo()
        {
            Vehiculos = new HashSet<Vehiculos>();
        }
        public int IdTipoVehiculo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Vehiculos> Vehiculos { get; set; }
    }
}
