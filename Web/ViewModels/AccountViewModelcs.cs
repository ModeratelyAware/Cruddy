using Cruddy.Application.Models;
using Cruddy.Data.Identity.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Cruddy.Web.ViewModels
{
	public class AccountViewModel
	{
		[Required]
		public CruddyUser User { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}