using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Regex;

public static class RegexConstants
{
	public const string EmailAddress =
		@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

	public const string PhoneNumber =
		@"\(?(\d{3})\)?-?\s?(\d{3})\)?-?(\d{4})";
}