using AutoMapper;
using Dominio;
using Dominio.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Ciudad
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<DTO_Ciudad>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<DTO_Ciudad>>
        {
            private readonly Context _context;
            private readonly IMapper _mapper;

            public Manejador(Context context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<DTO_Ciudad>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var ciudades = await _context.Ciudades.Include(c => c.Provincia).ToListAsync();

                var listaDTO = _mapper.Map<List<Ciudades>, List<DTO_Ciudad>>(ciudades);

                return listaDTO;
            }
        }
    }
}
