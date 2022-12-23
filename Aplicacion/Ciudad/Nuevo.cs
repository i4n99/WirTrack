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

namespace Aplicacion.Ciudad
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public int IdProvincia { get; set; }
            public string Ciudad { get; set; }
            public int? IdOpenWeather { get; set; }
            public double? Latitud { get; set; }
            public double? Longitud { get; set; }
            public string? Descripcion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.IdProvincia).NotEmpty().LessThanOrEqualTo(23);
                RuleFor(x => x.Ciudad).NotEmpty();
                RuleFor(x => x.IdOpenWeather).NotNull().WithMessage("Debe cargar un Id de OpenWeather si no carga una logitud y latitud").GreaterThan(0).When(x => x.Latitud == null && x.Longitud == null);
                RuleFor(x => x.Latitud).NotNull().WithMessage("Si carga una longitud debe cargar su latitud tambien").When(x => x.Longitud != null);
                RuleFor(x => x.Longitud).NotNull().WithMessage("Si carga una latitud debe cargar su longitud tambien").When(x => x.Latitud != null);
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

                var ciudad = await _context.Ciudades.Where(c => c.Ciudad == request.Ciudad && c.IdProvincia == request.IdProvincia).FirstOrDefaultAsync();
                if (ciudad != null)
                    throw new ManejadorErrores.ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "La ciudad que desea ingresar ya esta cargada en esta provincia" });
                else
                {
                    ciudad = new Ciudades();
                    ciudad.IdProvincia = request.IdProvincia;
                    ciudad.Ciudad = request.Ciudad;
                    ciudad.IdOpenWeather = request.IdOpenWeather;
                    ciudad.Latitud = request.Latitud;
                    ciudad.Longitud = request.Longitud;
                    ciudad.Descripcion = request.Descripcion;
                }

                try
                {
                    _context.Ciudades.Add(ciudad);
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
