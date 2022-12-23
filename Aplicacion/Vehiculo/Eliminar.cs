using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Vehiculo
{
    public class Eliminar
    {
        public class Ejecuta : IRequest<Unit>
        {
            [JsonIgnore]
            public int IdVehiculo { get; set; }
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
                var vehiculo = await _context.Vehiculos.Where(x => x.IdVehiculo == request.IdVehiculo).FirstOrDefaultAsync();
                if (vehiculo == null)
                    throw new ManejadorErrores.ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El vehiculo que quiere eliminar no existe" });

                try
                {
                    _context.Remove(vehiculo);
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
