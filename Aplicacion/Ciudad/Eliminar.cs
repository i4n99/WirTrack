using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Ciudad
{
    public class Eliminar
    {
        public class Ejecuta : IRequest<Unit>
        {
            [JsonIgnore]
            public int IdCiudad { get; set; }
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
                var ciudad = await _context.Ciudades.Where(c => c.IdCiudad == request.IdCiudad).FirstOrDefaultAsync();
                if (ciudad == null)
                    throw new ManejadorErrores.ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "La ciudad que desea eliminar no existe" });

                try
                {
                    _context.Remove(ciudad);
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
