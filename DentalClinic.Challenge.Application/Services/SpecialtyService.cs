using AutoMapper;
using DentalClinic.Challenge.Application.DTOs;
using DentalClinic.Challenge.Application.Interfaces.Repositories;
using DentalClinic.Challenge.Application.Interfaces.Services;
using DentalClinic.Challenge.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Challenge.Application.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IMapper _mapper;

        public SpecialtyService(ISpecialtyRepository specialtyRepository, IMapper mapper)
        {
            _specialtyRepository = specialtyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SpecialtyDto>> GetAllSpecialtiesAsync()
        {
            var specialties = await _specialtyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SpecialtyDto>>(specialties);
        }

        public async Task<SpecialtyDto?> GetSpecialtyByIdAsync(int id)
        {
            var specialty = await _specialtyRepository.GetByIdAsync(id);
            if(specialty == null)
            {
                throw new InvalidOperationException($"Specialty with ID {id} not found.");
            }
            return _mapper.Map<SpecialtyDto>(specialty);
        }

        public async Task<SpecialtyDto> CreateSpecialtyAsync(SpecialtyDto specialtyDto)
        {
            var codeExists = await _specialtyRepository.ExistsByCodeAsync(specialtyDto.Code!);
            if(codeExists)
            {
                throw new ArgumentException($"A Specialty with code '{specialtyDto.Code}' already exists.");
            }

            var descriptionExists = await _specialtyRepository.ExistsByDescriptionAsync(specialtyDto.Description!);
            if(descriptionExists)
            {
                throw new ArgumentException($"A specialty with description '{specialtyDto.Description}' already exists.");
            }

            var specialty = _mapper.Map<Specialty>(specialtyDto);
            specialty.Id = 0;
            specialty.RowVersion = null;

            var createdSpecialty = await _specialtyRepository.AddAsync(specialty);
            return _mapper.Map<SpecialtyDto>(createdSpecialty);
        }

        public async Task UpdateSpecialtyAsync(int id, SpecialtyDto specialtyDto)
        {
            var existingSpecialty = await _specialtyRepository.GetByIdAsync(id);
            if(existingSpecialty == null)
            {
                throw new InvalidOperationException($"Specialty with ID {id} not found for update");
            }

            var codeExists = await _specialtyRepository.ExistsByCodeAsync(specialtyDto.Code!, id);
            if (codeExists)
            {
                throw new ArgumentException($"A specialty with code '{specialtyDto.Code}' already exists for another specialty");
            }

            var descriptionExists = await _specialtyRepository.ExistsByDescriptionAsync(specialtyDto.Description!, id);
            if(descriptionExists)
            {
                throw new ArgumentException($"A specialty with description '{specialtyDto.Description}' already exists for another specialty.");
            }

            if (string.IsNullOrEmpty(specialtyDto.RowVersion))
            {
                throw new ArgumentException("RowVersion is required for updating a specialty to prevent concurrency conflicts.");
            }

            _mapper.Map(specialtyDto, existingSpecialty);

            existingSpecialty.Id = id;

            try
            {
                await _specialtyRepository.UpdateAsync(existingSpecialty);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidOperationException("Concurrency conflict: The specialty was modified by another user.", ex);
            }
        }
        public async Task DeleteSpecialtyAsync(int id)
        {
            var specialty = await _specialtyRepository.GetByIdAsync(id);
            if (specialty == null)
            {
                throw new InvalidOperationException($"Specialty with ID {id} not found for deletion.");
            }

            await _specialtyRepository.DeleteAsync(specialty);
        }
    }
}
