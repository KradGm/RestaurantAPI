using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain.Abstractions.Service
{
    public interface IPratoService
    {
        Task<IEnumerable<Prato>> GetAllPratosAsync();

        Task<Prato?> GetPratoByIdAsync(int id);

        Task<Prato?> Create(Prato prato);

        Task<Prato?> Update(int id, Prato prato);

        Task<Prato?> Patch(int id, JsonPatchDocument<Prato> newPratoInfo, ModelStateDictionary modelState);

        Task<Prato?> Delete(int id);

        IEnumerable<Prato> GetAllPratosRestaurante();
    }
}
