using Aplicacion.Provincia;
using Dominio;
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
        public async Task<ActionResult<List<Provincias>>> GetViajes()
        {
            return await mediator.Send(new Consulta.Ejecuta());
        }
    }
}
