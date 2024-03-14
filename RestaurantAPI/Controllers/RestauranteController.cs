using Domain.Abstractions.Service;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurantes")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private readonly IRestauranteService _restauranteService;

        public RestauranteController(IRestauranteService restauranteService)
        {
            _restauranteService = restauranteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurante>>> GetRestaurantes()
        {
            var restaurantes = await _restauranteService.GetAllRestaurantesAsync();
            if(restaurantes is null)
            {
                return NotFound("Restaurantes não encontrados");
            }
            return Ok(restaurantes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Restaurante>> Get(int id)
        {
            var restaurante = await _restauranteService.GetRestauranteByIdAsync(id);
            if(restaurante == null)
            {
                return NotFound("Esse restaurante não existe");
            }
            return Ok(restaurante);

        }
        [HttpGet("pratos")]
        public IEnumerable<Restaurante> GetPratosRestaurantes()
        {
            return _restauranteService.GetAllPratosRestaurante();
        }
    }
       
}
