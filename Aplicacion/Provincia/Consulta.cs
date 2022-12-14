using AutoMapper;
using Dominio;
using Dominio.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Provincia
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<DTO_Provincia>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<DTO_Provincia>>
        {
            private readonly Context _context;
            private readonly IMapper _mapper;

            public Manejador(Context context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<DTO_Provincia>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var provincias = await _context.Provincias.ToListAsync();

                var listaDTO = _mapper.Map<List<Provincias>, List<DTO_Provincia>>(provincias);

                return listaDTO;
            }
        }
    }
}
