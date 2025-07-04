using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Challenge.Core.Entities
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; } = string.Empty;
        public byte[]? RowVersion { get; set; }
    }
}
