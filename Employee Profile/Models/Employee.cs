using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Profile.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string? Phone { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public float Salary { get; set; }

        // Foreign key to customer
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department? Department { get; set; }

    }
}
