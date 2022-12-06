using Aplicacion.Viaje;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WirTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ViajesController : MiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Viajes>>> GetViajes()
        {
            return await mediator.Send(new Consulta.Ejecuta());
        }
    }
}
