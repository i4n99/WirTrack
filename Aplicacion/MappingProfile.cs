using AutoMapper;
using Dominio;
using Dominio.DTO;

namespace Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ciudades, DTO_Ciudad>();
            CreateMap<Provincias, DTO_Provincia>();
            CreateMap<Vehiculos, DTO_Vehiculo>();
            CreateMap<VehiculosTipoVehiculo, DTO_VehiculoTipoVehiculo>();
            CreateMap<Viajes, DTO_Viaje>();
        }
    }
}
