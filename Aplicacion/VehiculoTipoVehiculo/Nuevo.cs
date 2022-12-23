using Dominio;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.VehiculoTipoVehiculo
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Descripcion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Descripcion).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly Context _context;
            public Manejador(Context context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                EjecutaValidacion validator = new EjecutaValidacion();
                ValidationResult results = validator.Validate(request);
                if (!results.IsValid)
                    throw new ManejadorErrores.ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = string.Join(" / ", results.Errors.Select(s => s.ErrorMessage)) });

                var tipoVehiculo = await _context.VehiculosTipoVehiculo.Where(tv => tv.Descripcion == request.Descripcion).FirstOrDefaultAsync();
                if (tipoVehiculo != null)
                    throw new ManejadorErrores.ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El tipo de vehiculo que desea ingresar ya esta cargado" });
                else
                    tipoVehiculo = new VehiculosTipoVehiculo()
                    {
                        Descripcion = request.Descripcion
                    };

                try
                {
                    _context.VehiculosTipoVehiculo.Add(tipoVehiculo);
                    await _context.SaveChangesAsync();
                    return Unit.Value;
                }
                catch (Exception ex)
                {
                    throw new ManejadorErrores.ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "Ha ocurrido un error en el servidor", dataError = ex });
                }
            }
        }
    }
}
