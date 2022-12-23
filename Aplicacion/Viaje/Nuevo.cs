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

namespace Aplicacion.Viaje
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public int IdCiudad { get; set; }
            public int[] IdsVehiculos { get; set; }
            public DateTime Fecha { get; set; }
            public string NombreReserva { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.IdCiudad).NotEmpty();
                RuleFor(x => x.IdsVehiculos).Must( (x) => x.Length > 0 ).WithMessage("Debe haber al menos un Id de vehiculo en el array");
                RuleFor(x => x.Fecha).Must( (x) => x >= DateTime.Now.Date && x <= DateTime.Now.Date.AddDays(5) ).WithMessage("La fecha no puede ser anterior a la fecha actual ni ser posterior a 5 dias");
                RuleFor(x => x.NombreReserva).NotEmpty();
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

                var ciudad = await _context.Ciudades.Where(c => c.IdCiudad == request.IdCiudad).FirstOrDefaultAsync();
                if (ciudad == null)
                    throw new ManejadorErrores.ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El IdCiudad proporcionado no corresponde a ninguna ciudad cargada" });
                
                var IdsBD = await _context.Vehiculos.Select(v => v.IdVehiculo).ToListAsync();

                foreach (var id in request.IdsVehiculos)
                {
                    if (!IdsBD.Contains(id))
                        throw new ManejadorErrores.ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = string.Format("El id {0} proporcionado en el array no pertenece a ningun vehiculo cargado", id) });
                }

                var viaje = new Viajes();
                viaje.IdCiudad = ciudad.IdCiudad;
                viaje.IdsVehiculos = string.Join(",", request.IdsVehiculos); ;
                viaje.Fecha = request.Fecha;
                viaje.NombreReserva = request.NombreReserva;
                
                try
                {
                    _context.Viajes.Add(viaje);
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
