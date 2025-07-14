using DentalClinic.Challenge.Application.Interfaces.Repositories;
using DentalClinic.Challenge.Core.Entities;
using DentalClinic.Challenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Challenge.Infrastructure.Repositories
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly ApplicationDbContext _context;

        public SpecialtyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Specialty>> GetAllAsync()
        {
            return await _context.Specialties.ToListAsync();
        }
        public async Task<Specialty?> GetByIdAsync(int id)
        {
            return await _context.Specialties.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Specialty> AddAsync(Specialty specialty)
        {
            await _context.Specialties.AddAsync(specialty);
            await _context.SaveChangesAsync();
            return specialty;
        }
        public async Task UpdateAsync(Specialty specialty)
        {
            _context.Specialties.Update(specialty);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Specialty specialty)
        {
            _context.Specialties.Remove(specialty);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsByCodeAsync(string code, int? excludeId = null)
        {
            if(excludeId.HasValue)
            {
                return await _context.Specialties.AnyAsync(s => s.Code == code && s.Id != excludeId.Value);
            }
            return await _context.Specialties.AnyAsync(s => s.Code == code);
        }
        public async Task<bool> ExistsByDescriptionAsync(string description, int? excludeId = null)
        {
            if(excludeId.HasValue)
            {
                return await _context.Specialties.AnyAsync(s => s.Description == description && s.Id != excludeId.Value);
            }
            return await _context.Specialties.AnyAsync(s => s.Description == description);
        }
    }
}
