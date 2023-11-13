
using System.ComponentModel.DataAnnotations;

namespace Employee_Profile.ViewModel
{
    public class EmployeeViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public required int Salary { get; set; }
        public int DepartmentID { get; set; }

    }
}
