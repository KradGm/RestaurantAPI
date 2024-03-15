using Domain.Abstractions.Data;
using Domain.Abstractions.Service;
using Domain.Entities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
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

        public async Task<ActionResult<Prato>> Create(Prato prato)
        {
            try
            {
                _context.Pratos.Add(prato);
                await _context.SaveChangesAsync();
                return prato;
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<IEnumerable<Prato>> GetPratos(string? tag, string? ordering, string? search, int? page)
        {
            var query = _context.Pratos.AsQueryable();

            if (!string.IsNullOrEmpty(tag))
            {
                query = query.Where(p => p.Tag == tag);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Nome.Contains(search));
            }
            if (!string.IsNullOrEmpty(ordering))
            {
                switch (ordering)
                {
                    //Ordenando por descendente o nome
                    case "name":
                        query = query.OrderBy(p => p.Nome);
                        break;
                }
            }
            if (page.HasValue)
            {
                int pageSize = 20;
                query = query.Skip((page.Value - 1) * pageSize).Take(pageSize);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Prato?>> GetAllPratosAndRestaurantes()
        {
            var pratosAndRestaurante = await _context.Pratos.Include(p => p.Restaurante).Where(r => r.Id <= 10).AsNoTracking().ToListAsync();
            return pratosAndRestaurante;
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
            var pratoToDelete = await _context.Pratos.FirstOrDefaultAsync(p => p.Id == id);
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
