using Aplicacion.VehiculoTipoVehiculo;
using Dominio.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WirTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosTipoVehiculoController : MiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<DTO_VehiculoTipoVehiculo>>> Consulta()
        {
            return await mediator.Send(new Consulta.Ejecuta());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await mediator.Send(data);
        }

        [HttpDelete("{idTipoVehiculo}")]
        public async Task<ActionResult<Unit>> Eliminar(int idTipoVehiculo)
        {
            return await mediator.Send(new Eliminar.Ejecuta { IdTipoVehiculo = idTipoVehiculo });
        }
    }
}
