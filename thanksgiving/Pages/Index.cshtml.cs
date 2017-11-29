using thanksgiving.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace thanksgiving.Pages
{
	public class IndexModel : PageModel
	{
		// DEFAULT MODE
		public void OnGet()
		{
			bridgeGreetings = new Greetings();
		}

		// PREVIEW MODE (AFTER SUBMITTING)
		public async Task<IActionResult> OnPost()
		{
			if (await isValid())
			{
				if (ModelState.IsValid)
				{

					try
					{
						// DB-RELATED: CUSTOMIZE VALUES TO BE ADDED TO THE DB
						bridgeGreetings.createDate = DateTime.Now.ToString();
						bridgeGreetings.createIP = this.HttpContext.Connection.RemoteIpAddress.ToString();
						bridgeGreetings.fromEmail = bridgeGreetings.fromEmail.ToLower();
						bridgeGreetings.toEmail = bridgeGreetings.toEmail.ToLower();
						bridgeGreetings.agree = "true";
						bridgeGreetings.mesg = bridgeGreetings.mesg.ToLower();
						bridgeGreetings.mesg = bridgeGreetings.mesg.Replace("fuck", "duck");



						// DB-RELATED: ADD NEW RECORD TO THE DATABASE 
						_myDB.Greetings.Add(bridgeGreetings);
						_myDB.SaveChanges();

						// DB-RELATED: SEND USER TO THE PREVIEW PAGE SHOWING THE NEW RECORD
						return RedirectToPage("Preview", new { id = bridgeGreetings.ID });
					}
					catch
					{
						return RedirectToPage("Index");
					}
				}
			}
			else
			{
				ModelState.AddModelError("bridgeGreetings.reCaptcha", "Hahaha!!");
			}

			return Page();
		}

		// BRIDGE TO GREETINGS MODEL
		[BindProperty]
		public Greetings bridgeGreetings { get; set; }

		// HEY, CONNECT MY DATABASE TO THIS MODEL
		private DbBuilder _myDB;
		public IndexModel(DbBuilder myDB)
		{
			_myDB = myDB;
		}



		// RE-CAPTCHA VALIDATION
		private async Task<bool> isValid()
		{
			var response = this.HttpContext.Request.Form["g-recaptcha-response"];
			if (string.IsNullOrEmpty(response))
				return false;

			try
			{
				using (var client = new HttpClient())
				{
					var values = new Dictionary<string, string>();
					values.Add("secret", "6LeM_i0UAAAAAH1TDEPx7tflYMolbG6byr52fq0f");
					values.Add("response", response);
					values.Add("remoteip", this.HttpContext.Connection.RemoteIpAddress.ToString());

					var query = new FormUrlEncodedContent(values);


					var post = client.PostAsync("https://www.google.com/recaptcha/api/siteverify", query);

					var json = await post.Result.Content.ReadAsStringAsync();

					if (json == null)
						return false;

					var results = JsonConvert.DeserializeObject<dynamic>(json);

					return results.success;
				}

			}
			catch { }


			return false;
		}



	}
}
