using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeProject.Models
{
    [Index(nameof(Email), IsUnique = true)]
	public class Employee
	{
		[Key]
		public int EmployeeId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
		public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
		public string LastName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        [EmailAddress]
       
        public string Email { get; set; }

		[Required]
		public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
		public string Department { get; set; }

		[Required]

        [Range(0, int.MaxValue, ErrorMessage = "Salary must be a non-negative number.")]
        public decimal Salary { get; set; }

	}
}
