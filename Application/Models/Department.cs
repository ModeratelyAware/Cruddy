using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cruddy.Application.Models
{
	public class Department
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "{0} cannot be blank.")]
		[DisplayName("Department")]
		[StringLength(100)]
		public string Name { get; set; }
	}
}