using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.Data
{
    public interface IDbContext
    {
        DbSet<Restaurante> Restaurantes { get; set; }
        DbSet<Prato> Pratos { get; set; }
        Task<int> SaveChangesAsync();
    }
}
