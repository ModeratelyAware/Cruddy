using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cruddy.Application.Models
{
	public class Employee
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "{0} cannot be blank.")]
		[DisplayName("First Name")]
		[StringLength(50)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "{0} cannot be blank.")]
		[DisplayName("Last Name")]
		[StringLength(50)]
		public string LastName { get; set; }

		[Required(ErrorMessage = "{0} cannot be blank.")]
		[ForeignKey("Department")]
		[DisplayName("Department")]
		public int DepartmentId { get; set; }

		public virtual Department? Department { get; set; }

		[Required(ErrorMessage = "{0} cannot be blank.")]
		[StringLength(100)]
		public string Title { get; set; }

		[StringLength(320)]
		[EmailAddress]
		public string? Email { get; set; }

		[StringLength(50)]
		[RegularExpression(@"\(?(\d{3})\)?-?\s?(\d{3})\)?-?(\d{4})", ErrorMessage = "Invalid phone number.")]
		public string? Phone1 { get; set; }

		[StringLength(50)]
		[RegularExpression(@"\(?(\d{3})\)?-?\s?(\d{3})\)?-?(\d{4})", ErrorMessage = "Invalid phone number.")]
		public string? Phone2 { get; set; }

		public int? Ext { get; set; }

		[StringLength(200)]
		public string? Notes { get; set; }
	}
}