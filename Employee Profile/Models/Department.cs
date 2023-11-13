using System.ComponentModel.DataAnnotations;

namespace Employee_Profile.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
