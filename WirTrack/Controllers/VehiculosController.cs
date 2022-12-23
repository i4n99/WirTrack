using Aplicacion.Vehiculo;
using Dominio.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WirTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : MiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<DTO_Vehiculo>>> Consulta()
        {
            return await mediator.Send(new Consulta.Ejecuta());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await mediator.Send(data);
        }

        [HttpPut("{idVehiculo}")]
        public async Task<ActionResult<Unit>> Actualizar(int idVehiculo, Actualizar.Ejecuta data)
        {
            data.IdVehiculo = idVehiculo;
            return await mediator.Send(data);
        }

        [HttpDelete("{idVehiculo}")]
        public async Task<ActionResult<Unit>> Eliminar(int idVehiculo)
        {
            return await mediator.Send(new Eliminar.Ejecuta { IdVehiculo = idVehiculo });
        }
    }
}
