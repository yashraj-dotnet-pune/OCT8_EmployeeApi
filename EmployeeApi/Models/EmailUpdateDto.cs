using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.Models
{
    public class EmailUpdateDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
