using AutoMapper;
using Dominio;
using Dominio.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Vehiculo
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<DTO_Vehiculo>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<DTO_Vehiculo>>
        {
            private readonly Context _context;
            private readonly IMapper _mapper;

            public Manejador(Context context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<DTO_Vehiculo>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var vehiculos = await _context.Vehiculos.Include(v => v.TipoVehiculo).ToListAsync();

                var listaDTO = _mapper.Map<List<Vehiculos>, List<DTO_Vehiculo>>(vehiculos);

                return listaDTO;
            }
        }
    }
}
