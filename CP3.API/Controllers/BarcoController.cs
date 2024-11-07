using CP3.Application.Dtos;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CP3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcoController : ControllerBase
    {
        private readonly IBarcoApplicationService _applicationService;

        public BarcoController(IBarcoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BarcoEntity>), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            var barcos = _applicationService.ObterTodosBarcos();

            if (barcos != null)
                return Ok(barcos);

            return BadRequest("Não foi possível obter os dados");
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BarcoEntity), (int)HttpStatusCode.OK)]
        public IActionResult GetPorId(int id)
        {
            var barco = _applicationService.ObterBarcoPorId(id);

            if (barco != null)
                return Ok(barco);

            return BadRequest("Não foi possível obter os dados");
        }

        [HttpPost]
        [ProducesResponseType(typeof(BarcoEntity), (int)HttpStatusCode.OK)]
        public IActionResult Post([FromBody] BarcoDto entity)
        {
            try
            {
                var barco = _applicationService.AdicionarBarco(entity);

                if (barco != null)
                    return Ok(barco);

                return BadRequest("Não foi possível salvar os dados");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest,
                });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BarcoEntity), (int)HttpStatusCode.OK)]
        public IActionResult Put(int id, [FromBody] BarcoDto entity)
        {
            try
            {
                var barco = _applicationService.EditarBarco(id, entity);

                if (barco != null)
                    return Ok(barco);

                return BadRequest("Não foi possível editar os dados");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest,
                });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BarcoEntity), (int)HttpStatusCode.OK)]
        public IActionResult Delete(int id)
        {
            try
            {
                var barco = _applicationService.RemoverBarco(id);

                if (barco != null)
                    return Ok(barco);

                return BadRequest("Não foi possível deletar os dados");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest,
                });
            }
        }
    }
}
