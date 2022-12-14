using Aplicacion.Provincia;
using Dominio.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WirTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProvinciasController : MiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<DTO_Provincia>>> Consulta()
        {
            return await mediator.Send(new Consulta.Ejecuta());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await mediator.Send(data);
        }

        [HttpDelete("{idProvincia}")]
        public async Task<ActionResult<Unit>> Eliminar(int idProvincia)
        {
            return await mediator.Send(new Eliminar.Ejecuta { IdProvincia = idProvincia });
        }
    }
}
