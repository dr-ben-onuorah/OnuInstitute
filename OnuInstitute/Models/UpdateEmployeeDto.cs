using System.ComponentModel.DataAnnotations.Schema;

namespace OnuInstitute.Models
{
    public class UpdateEmployeeDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; } //nullable

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
    }
}
