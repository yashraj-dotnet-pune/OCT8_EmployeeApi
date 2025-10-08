using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.Models
{
    public class Employee
    {
     
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive integer.")]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name can contain only letters and spaces.")]
        public string Name { get; set; }

       
        public string Department { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "MobileNo must be exactly 10 digits.")]
        public string MobileNo { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
    }
}
