using DentalClinic.Challenge.Core.Entities;

namespace DentalClinic.Challenge.Application.Interfaces.Repositories
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialty>> GetAllAsync();
        Task<Specialty?> GetByIdAsync(int id);
        Task<Specialty> AddAsync(Specialty specialty);
        Task UpdateAsync(Specialty specialty);
        Task DeleteAsync(Specialty specialty);
        Task<bool> ExistsByCodeAsync(string code, int? excludeId = null);
        Task<bool> ExistsByDescriptionAsync(string description, int? excludeId = null);
    }
}
