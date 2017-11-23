using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using thanksgiving.Model;

namespace thanksgiving.Pages
{
	public class CompleteModel : PageModel
	{
		public Greetings bridgeGreetings;

		public DbBuilder _myDB { get; set; }

		public void OnGet(int ID = 0)
		{
			if (ID > 0)
			{
				bridgeGreetings = _myDB.Greetings.Find(ID);
			}
		}
	}
}