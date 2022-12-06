using MediatR;
using System.Collections.Generic;
using Dominio;
using AutoMapper;
using System.Threading.Tasks;
using System.Threading;
using Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Viaje
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<Viajes>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<Viajes>>
        {
            private readonly Context _context;
            private readonly IMapper _mapper;

            public Manejador(Context context, IMapper IMapper)
            {
                _mapper = IMapper;
                _context = context;
            }
            public async Task<List<Viajes>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var viajes = await _context.Viajes.ToListAsync();
                if(viajes.Count == 0)
                {
                    var viaje = new Viajes();
                    viajes.Add(viaje);
                }

                //var listaDTO = _mapper.Map<List<Dominio.Beneficios>, List<DTO_Beneficio>>(beneficios);

                //return listaDTO;
                return viajes;
            }
        }
    }
    
}
