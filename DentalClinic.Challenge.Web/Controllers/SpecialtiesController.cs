using DentalClinic.Challenge.Application.DTOs;
using DentalClinic.Challenge.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Challenge.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;

        public SpecialtiesController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SpecialtyDto>))]
        public async Task<ActionResult<IEnumerable<SpecialtyDto>>> GetAllSpecialities()
        {
            var specialties = await _specialtyService.GetAllSpecialtiesAsync();
            return Ok(specialties);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpecialtyDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpecialtyDto>> GetSpecialtyById(int id)
        {
            try
            {
                var specialty = await _specialtyService.GetSpecialtyByIdAsync(id);
                return Ok(specialty);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("not found")) return NotFound(new { message = ex.Message });
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error ocurred while retrieving the specialty.", details = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SpecialtyDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<SpecialtyDto>> CreateSpecialty([FromBody] SpecialtyDto specialtyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdSpecialty = await _specialtyService.CreateSpecialtyAsync(specialtyDto);
                return CreatedAtAction(nameof(GetSpecialtyById), new { id = createdSpecialty.Id }, createdSpecialty);
            }
            catch (ArgumentException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error ocurred while creating the specialty.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateSpecialty(int id, [FromBody] SpecialtyDto specialtyDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _specialtyService.UpdateSpecialtyAsync(id, specialtyDto);
                return NoContent();
            }
            catch(InvalidOperationException ex)
            {
                if(ex.Message.Contains("not found"))
                {
                    return NotFound(new { message = ex.Message });
                }
                else if(ex.Message.Contains("Concurrency conflict"))
                {
                    return Conflict(new { message = ex.Message });
                }
                return BadRequest(new {message = ex.Message});
            }
            catch(ArgumentException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return Conflict(new { message= "Concurrency conflict: The specialty was modified by another user directly in the database.", details = ex.Message});
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error ocurred while updating the specialty.", details = ex.Message } );
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSpecialty(int id)
        {
            try
            {
                await _specialtyService.DeleteSpecialtyAsync(id);
                return NoContent();
            }
            catch(InvalidOperationException ex)
            {
                if(ex.Message.Contains("not found"))
                {
                    return NotFound(new { message = ex.Message });
                }
                return BadRequest(new { message = ex.Message});
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error ocurred while deleting the specialty.", details = ex.Message });
            }
        }
    }
}
