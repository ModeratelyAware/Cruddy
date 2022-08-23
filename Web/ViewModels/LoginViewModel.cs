using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class LoginViewModel
{
	[Required]
	public string Username { get; set; }

	[DataType(DataType.Password)]
	public string Password { get; set; }
}