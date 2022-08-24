using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Regex;

public static class RegexConstants
{
	public const string EmailAddress =
		@"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?";

	public const string PhoneNumber =
		@"((\+?\d*)\s?\(?(\d{3})\)?\s?\.?-?(\d{3})\s?\.?-?(\d{4}))";
}