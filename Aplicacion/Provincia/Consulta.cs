using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Provincia
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<Provincias>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<Provincias>>
        {
            private readonly Context _context;
            private readonly IMapper _mapper;

            public Manejador(Context context, IMapper IMapper)
            {
                _mapper = IMapper;
                _context = context;
            }
            public async Task<List<Provincias>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var provincias = await _context.Provincias.ToListAsync();

                if (provincias.Count == 0)
                {
                    var provincia = new Provincias();
                    provincias.Add(provincia);
                }

                //var listaDTO = _mapper.Map<List<Dominio.Beneficios>, List<DTO_Beneficio>>(beneficios);

                //return listaDTO;
                return provincias;
            }
        }
    }
}
