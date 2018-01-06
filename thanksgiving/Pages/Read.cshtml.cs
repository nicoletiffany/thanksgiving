using thanksgiving.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Mail;

namespace thanksgiving.Pages
{
	public class ReadModel : PageModel
	{
		// BRIDGE TO GREETINGS MODEL
		[BindProperty]
		public Greetings bridgeGreetings { get; set; }
		public string Message { get; set; }
		// DB-RELATED: CONNECT MY DATABASE TO THIS MODEL
		private DbBuilder _myDB;
		public ReadModel(DbBuilder myDB)
		{
			_myDB = myDB;
		}

		public IActionResult OnGet(int ID = 0)
		{
			if (ID > 0)
			{
				bridgeGreetings = _myDB.Greetings.Find(ID);
			}

			if (bridgeGreetings == null)
			{
				Response.Redirect("~/");
			}
			return Page();
		}
	}
}