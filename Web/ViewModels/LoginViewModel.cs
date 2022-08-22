using Cruddy.Application.Models;
using Cruddy.Data.Identity.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Cruddy.Web.ViewModels
{
	public class LoginViewModel
	{
		[Required]
		public string Username { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}