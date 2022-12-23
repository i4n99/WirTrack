using FluentValidation;
using FluentValidation.Results;
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
    public class Actualizar
    {
        public class Ejecuta : IRequest
        {
            [JsonIgnore]
            public int IdVehiculo { get; set; }
            public int IdTipoVehiculo { get; set; }
            public string Patente { get; set; }
            public string Marca { get; set; }
            public string Modelo { get; set; }
            public string Comentarios { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.IdVehiculo).NotEmpty();
                RuleFor(x => x.IdTipoVehiculo).NotNull().GreaterThan(0);
                RuleFor(x => x.Patente).NotEmpty().Length(7);
                RuleFor(x => x.Marca).NotEmpty();
                RuleFor(x => x.Modelo).NotEmpty();
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

                var vehiculo = await _context.Vehiculos.Where(v => v.IdVehiculo == request.IdVehiculo).FirstOrDefaultAsync();
                if (vehiculo == null)
                    throw new ManejadorErrores.ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El vehiculo que desea actualizar no existe" });
                    
                var vehiculoMismaPatente = await _context.Vehiculos.Where(v => v.Patente == request.Patente && v.IdVehiculo != vehiculo.IdVehiculo).FirstOrDefaultAsync();
                if (vehiculoMismaPatente != null)
                    throw new ManejadorErrores.ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "La patente del vehiculo que desea actualizar ya esta cargada" });

                vehiculo.IdTipoVehiculo = request.IdTipoVehiculo;
                vehiculo.Patente = request.Patente;
                vehiculo.Marca = request.Marca;
                vehiculo.Modelo = request.Modelo;
                vehiculo.Comentarios = request.Comentarios;

                try
                {
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
