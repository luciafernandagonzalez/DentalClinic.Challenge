using DentalClinic.Challenge.Application.DTOs;

namespace DentalClinic.Challenge.Application.Interfaces.Services
{
    public interface ISpecialtyService
    {
        Task<IEnumerable<SpecialtyDto>> GetAllSpecialtiesAsync();
        Task<SpecialtyDto?> GetSpecialtyByIdAsync(int id);
        Task<SpecialtyDto> CreateSpecialtyAsync(SpecialtyDto specialtyDto);
        Task UpdateSpecialtyAsync(int id, SpecialtyDto specialtyDto);
        Task DeleteSpecialtyAsync(int id);
    }
}
