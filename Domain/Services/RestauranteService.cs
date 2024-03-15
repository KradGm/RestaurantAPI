using Domain.Abstractions.Data;
using Domain.Abstractions.Service;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public IEnumerable<Restaurante> GetAllRestaurantesAndPratos()
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
            if (restaurante == null)
            {
                return null;
            }
            return restaurante;
        }

        public async Task<Restaurante?> Create(Restaurante restaurante)
        {
            if (restaurante == null)
            {
                return null;
            }
            _context.Restaurantes.Add(restaurante);
            await _context.SaveChangesAsync();
            return restaurante;
        }
        public async Task<Restaurante?> Delete(int id)
        {
            var taskToDelete = await _context.Restaurantes.FirstOrDefaultAsync(r => r.Id == id);
            if (taskToDelete == null)
            {
                return null;
            }
            _context.Restaurantes.Remove(taskToDelete);
            await _context.SaveChangesAsync();

            return taskToDelete;

        }

        public async Task<Restaurante?> Patch(int id, JsonPatchDocument restaurantPatch, ModelStateDictionary modelState)
        {
            if (restaurantPatch != null)
            {
                var restauranteToUpdate = await _context.Restaurantes.FindAsync(id);

                if (restauranteToUpdate == null)
                {
                    return null;
                }

                restaurantPatch.ApplyTo(restauranteToUpdate, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)modelState);

                await _context.SaveChangesAsync();

                return restauranteToUpdate;
            }
            else
            {
                return null;
            }
        }

        public async Task<Restaurante?> Update(int id, Restaurante restaurante)
        {
            if (id != restaurante.Id)
            {
                return null;
            }
            _context.Restaurantes.Entry(restaurante).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return restaurante;
        }
    }
}
