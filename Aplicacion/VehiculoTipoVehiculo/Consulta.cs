using AutoMapper;
using Dominio;
using Dominio.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.VehiculoTipoVehiculo
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<DTO_VehiculoTipoVehiculo>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<DTO_VehiculoTipoVehiculo>>
        {
            private readonly Context _context;
            private readonly IMapper _mapper;

            public Manejador(Context context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<DTO_VehiculoTipoVehiculo>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var tiposVehiculo = await _context.VehiculosTipoVehiculo.ToListAsync();

                var listaDTO = _mapper.Map<List<VehiculosTipoVehiculo>, List<DTO_VehiculoTipoVehiculo>>(tiposVehiculo);

                return listaDTO;
            }
        }
    }
}
