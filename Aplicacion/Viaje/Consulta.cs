using MediatR;
using System.Collections.Generic;
using Dominio;
using AutoMapper;
using System.Threading.Tasks;
using System.Threading;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using Dominio.DTO;

namespace Aplicacion.Viaje
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<DTO_Viaje>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<DTO_Viaje>>
        {
            private readonly Context _context;
            private readonly IMapper _mapper;

            public Manejador(Context context, IMapper IMapper)
            {
                _mapper = IMapper;
                _context = context;
            }
            public async Task<List<DTO_Viaje>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var viajes = await _context.Viajes.Include(v => v.Ciudad).ThenInclude(c => c.Provincia).ToListAsync();

                var listaDTO = _mapper.Map<List<Viajes>, List<DTO_Viaje>>(viajes);

                return listaDTO;
            }
        }
    }
    
}
