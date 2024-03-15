using Domain.Abstractions.Data;
using Domain.Abstractions.Service;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class PratoService : IPratoService
    {
        private readonly IDbContext _context;

        public PratoService(IDbContext context)
        {
            _context = context;
        }

        public async Task<Prato?> Create(Prato prato)
        {
            if (prato == null)
            {
                return null;
            }
            _context.Pratos.Add(prato);
            await _context.SaveChangesAsync();
            return prato;
        }
        public IEnumerable<Prato> GetAllPratosAndRestaurantes()
        {
            return _context.Pratos.Include(p => p.Restaurante).Where(r=>r.Id<=10).AsNoTracking().ToList();
        }

        public async Task<IEnumerable<Prato>> GetAllPratosAsync()
        {
            return await _context.Pratos.Take(20).AsNoTracking().ToListAsync();
        }
        public async Task<Prato?> GetPratoByIdAsync(int id)
        {
            var prato = await _context.Pratos.FirstOrDefaultAsync(p => p.Id == id);
            if (prato == null)
            {
                return null;
            }
            return prato;
        }

        public async Task<Prato?> Update(int id, Prato prato)
        {
            if (id != prato.Id)
            {
                return null;
            }
            _context.Pratos.Entry(prato).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return prato;
        }

        public async Task<Prato?> Patch(int id, JsonPatchDocument<Prato> newPratoInfo, ModelStateDictionary modelState)
        {
            if (newPratoInfo != null)
            {
                var pratoToUpdate = await _context.Pratos.FindAsync(id);

                if (pratoToUpdate == null)
                {
                    return null;
                }

                newPratoInfo.ApplyTo(pratoToUpdate, modelState);

                await _context.SaveChangesAsync();

                return pratoToUpdate;
            }
            else
            {
                return null;
            }
        }

        public async Task<Prato?> Delete(int id)
        {
            var pratoToDelete = await _context.Pratos.FirstOrDefaultAsync(p => p.Id == id) ;
            if (pratoToDelete is null)
            {
                return null;
            }
            _context.Pratos.Remove(pratoToDelete);
            await _context.SaveChangesAsync();

           return pratoToDelete;
        }
    }
}
