using System.ComponentModel.DataAnnotations;

namespace ExpOptII.Models
{
	class IntValidation : ValidationAttribute {
		public override bool IsValid(object value) {
			int d;
			return int.TryParse(value.ToString(), out d);
		}
	}
}
