using Domain.Abstractions.Data;
using Domain.Abstractions.Service;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class RestauranteService : IRestauranteService
    {

        private readonly IDbContext _context;

        public RestauranteService(IDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Restaurante> GetAllPratosRestaurante()
        {
            return _context.Restaurantes.Include(r => r.Pratos).Where(p => p.Id <= 20).AsNoTracking().ToList();
        }
        public async Task<IEnumerable<Restaurante>> GetAllRestaurantesAsync()
        {
            return await _context.Restaurantes.Take(10).AsNoTracking().ToListAsync();
        }

        public async Task<Restaurante?> GetRestauranteByIdAsync(int id)
        {
            var restaurante = await _context.Restaurantes.FirstOrDefaultAsync(r => r.Id == id);
            if(restaurante == null)
            {
                return null;
            }
            return restaurante;
        }
    }
}
