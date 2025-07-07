using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Challenge.Application.DTOs
{
    public class SpecialtyDto
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "The specialty code is required.")]
        [StringLength(15, ErrorMessage = "The specialty code cannot exceed 15 characters.")]
        public string Code {  get; set; } = string.Empty;
        [Required(ErrorMessage = "The description is required.")]
        [StringLength(50, ErrorMessage = "The description cannot exceed 50 characters.")]
        public string Description { get; set; } = string.Empty;
        public string? RowVersion {  get; set; }
    }
}
