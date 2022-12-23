using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Provincia
{
    public class Eliminar
    {
        public class Ejecuta : IRequest<Unit>
        {
            [JsonIgnore]
            public int IdProvincia { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, Unit>
        {
            private readonly Context _context;

            public Manejador(Context context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var provincia = await _context.Provincias.Where(x => x.IdProvincia == request.IdProvincia).FirstOrDefaultAsync();
                if (provincia == null)
                    throw new ManejadorErrores.ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La provincia que quiere eliminar no existe" });

                try
                {
                    _context.Remove(provincia);
                    await _context.SaveChangesAsync();
                    return Unit.Value;
                }
                catch (Exception ex)
                {
                    throw new ManejadorErrores.ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ha ocurrido un errore en el servidor", dataError = ex });
                }
            }
        }
    }
}
