using thanksgiving.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace thanksgiving.Pages
{
	public class PreviewModel : PageModel
	{
		// BRIDGE TO GREETINGS MODEL
		[BindProperty]
		public Greetings bridgeGreetings { get; set; }
		private IConfiguration _myConfiguration { get; set; }

		// DB-RELATED: CONNECT MY DATABASE TO THIS MODEL
		private DbBuilder _myDB;
		public PreviewModel(DbBuilder myDB, IConfiguration myConfiguration)
		{
			_myDB = myDB;
			_myConfiguration = myConfiguration;
		}

		public void OnGet(int ID = 0)
		{
			if (ID > 0)
			{
				bridgeGreetings = _myDB.Greetings.Find(ID);
			}
		}

		// EMAIL-RELATED
		public string Message { get; set; }
		public IActionResult OnPost(int id = 0)
		{
			if (id > 0)
			{
				bridgeGreetings = _myDB.Greetings.Find(id);

				try
				{
					// SEND 
					MailMessage Mailer = new MailMessage();

					Mailer.To.Add(new MailAddress(bridgeGreetings.toEmail, bridgeGreetings.toName));
					Mailer.Subject = bridgeGreetings.subject;
					Mailer.Body = "<img src = 'http://nicole.wowoco.org/images/happythanksgiving.jpg' />"
						+ bridgeGreetings.fromName
						+ " has a Thanksgiving greeting for you! Visit "
						+ "<a href=\"http://nicole.wowoco.org/read/\"" + bridgeGreetings.ID + ">" 
						+ "this site"
						+ "</a>"
						+ " for the full message!";


					Mailer.From = new MailAddress(bridgeGreetings.fromEmail, bridgeGreetings.fromName);

					Mailer.IsBodyHtml = true;


					using (SmtpClient smtpServer = new SmtpClient())
					{
						smtpServer.EnableSsl = Boolean.Parse(_myConfiguration["Smtp:EnableSs1"]);
						smtpServer.Host = _myConfiguration["Smtp:Host"]; // CHANGE
						smtpServer.Port = Int32.Parse(_myConfiguration["Smtp:Port"]); // CHANGE
						smtpServer.UseDefaultCredentials = Boolean.Parse(_myConfiguration["Smtp:UseDefaultCredentials"]);
						smtpServer.Send(Mailer);
					}

					// DB-RELATED: ASSIGN SEND INFO TO DATABASE
					bridgeGreetings.sendDate = DateTime.Now.ToString();
					bridgeGreetings.sendIP = this.HttpContext.Connection.RemoteIpAddress.ToString();


					// DB-RELATED: UPDATE RECORD ON THE DATABASE 
					_myDB.Greetings.Update(bridgeGreetings);
					_myDB.SaveChanges();


					return RedirectToPage("complete", new {ID= bridgeGreetings.ID});

				}
				catch
				{
					Message = "Apologies, there was an error and your greeting can't be sent.";
				}
			}

			return Page();
		}
	}
}