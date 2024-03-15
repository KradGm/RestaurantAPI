using Domain.Abstractions.Service;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [Route("api/pratos")]
    [ApiController]
    public class PratoController : ControllerBase
    {
        private readonly IPratoService _pratoService;

        public PratoController(IPratoService pratoService)
        {
            _pratoService = pratoService;
        }

        [HttpPost]
        public async Task<ActionResult<Prato>> Post(Prato prato)
        {
            if (prato == null)
            {
                return BadRequest("Há algo errado na criação do produto");
            }
            await _pratoService.Create(prato);

            return new CreatedAtRouteResult("ObterProduto", new { id = prato.Id }, prato);
        }

        [HttpGet("restaurantes")]
        public async Task<IEnumerable<Prato>> GetPratosRestaurantes()
        {
           var pratosAndRestaurantes = await _pratoService.GetAllPratosAndRestaurantes();
            return pratosAndRestaurantes;
        }

        [HttpGet("pratos_infos")]
        public async Task<ActionResult<IEnumerable<Prato>>> GetPratos(string? tag, string? ordering, string? search, int? page)
        {
            var pratos = await _pratoService.GetPratos(tag, ordering, search, page);
            return Ok(pratos);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prato>>> GetPratos()
        {
            var pratos = await _pratoService.GetAllPratosAsync();
            return Ok(pratos);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<Prato?>> Get(int id)
        {
            var prato = await _pratoService.GetPratoByIdAsync(id);
            if (prato == null)
            {
                return NotFound("Esse prato não existe");
            }
            return Ok(prato);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Prato>> Put(int id, Prato prato)
        {
            var pratoToUpdate = await _pratoService.Update(id, prato);
            if (pratoToUpdate == null)
            {
                return NotFound("Prato não encontrado");
            }
            return Ok(prato);
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Prato?>> Patch(int id, [FromBody] JsonPatchDocument<Prato> newPratoInfo)
        {
            var atualizadoComSucesso = await _pratoService.Patch(id, newPratoInfo, ModelState);

            if (atualizadoComSucesso != null)
            {
                return Ok(atualizadoComSucesso);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Prato?>> Delete(int id)
        {
            var pratoToDelete = await _pratoService.Delete(id);
            if (pratoToDelete == null)
            {
                return NotFound("Prato não encontrado");
            }
            return Ok(pratoToDelete);
        }
    }
}
