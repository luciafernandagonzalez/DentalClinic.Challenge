namespace DentalClinic.Challenge.Core.Entities
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[]? RowVersion { get; set; }
    }
}
