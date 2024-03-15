using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain.Abstractions.Service
{
    public interface IRestauranteService
    {
        Task<IEnumerable<Restaurante>> GetAllRestaurantesAsync();
        Task<Restaurante?> GetRestauranteByIdAsync(int id);
        IEnumerable<Restaurante> GetAllRestaurantesAndPratos();
        Task<Restaurante?> Update(int id, Restaurante restaurante);
        Task<Restaurante?> Delete(int id);
        Task<Restaurante?> Patch(int id, JsonPatchDocument restaurantPatch, ModelStateDictionary modelState);
        Task<Restaurante?> Create(Restaurante restaurante);

    }
}
