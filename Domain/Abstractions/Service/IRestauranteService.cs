using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.Service
{
    public interface IRestauranteService
    {
        Task<IEnumerable<Restaurante>> GetAllRestaurantesAsync();
        Task<Restaurante?> GetRestauranteByIdAsync(int id);
        IEnumerable<Restaurante> GetAllPratosRestaurante();

    }
}
