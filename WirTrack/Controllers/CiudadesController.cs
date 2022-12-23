using Aplicacion.Ciudad;
using Dominio.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WirTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadesController : MiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<DTO_Ciudad>>> Consulta()
        {
            return await mediator.Send(new Consulta.Ejecuta());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await mediator.Send(data);
        }

        [HttpPut("{idCiudad}")]
        public async Task<ActionResult<Unit>> Actualizar(int idCiudad, Actualizar.Ejecuta data)
        {
            data.IdCiudad = idCiudad;
            return await mediator.Send(data);
        }

        [HttpDelete("{idCiudad}")]
        public async Task<ActionResult<Unit>> Eliminar(int idCiudad)
        {
            return await mediator.Send(new Eliminar.Ejecuta { IdCiudad = idCiudad });
        }
    }
}
