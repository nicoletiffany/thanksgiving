using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace thanksgiving.Model
{
	public class Greetings
	{
		// HEY, ADD A UNIQUE IDENTIFIER
		[Key]
		public int? ID { get; set; }

		[DisplayName("Your friend's name")]
		[Display(Prompt = "Your friend's name")]
		[Required(ErrorMessage = "Required")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = "You must enter between 3 to 100 characters")]
		public string toName { get; set; }

		[DisplayName("Your friend's email")]
		[Display(Prompt = "Your friend's email")]
		[Required(ErrorMessage = "Required")]
		public string toEmail { get; set; }

		[DisplayName("Your email subject")]
		[Display(Prompt = "Your email subject")]
		[Required(ErrorMessage = "Required")]
		public string subject { get; set; }

		[DisplayName("Your custom message")]
		[Display(Prompt = "Personalize your greeting with your message here.")]
		[Required(ErrorMessage = "Required")]
		public string mesg { get; set; }

		[DisplayName("Your Name")]
		[Display(Prompt = "Your Name")]
		[Required(ErrorMessage = "Required")]
		public string fromName { get; set; }

		[DisplayName("Your Email")]
		[Display(Prompt = "Your Email")]
		[Required(ErrorMessage = "Required")]
		public string fromEmail { get; set; }

		[DisplayName("I agree")]
		public string agree { get; set; }

		public string createDate { get; set; }
		public string createIP { get; set; }

		public string sendDate { get; set; }
		public string sendIP { get; set; }

		public string reCaptcha { get; set; }
	}

}
