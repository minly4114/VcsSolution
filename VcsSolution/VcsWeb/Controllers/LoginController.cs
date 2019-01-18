using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAdapter.Outside;
using DataAdapter.Inside;

namespace VcsWeb.Controllers
{
	public class LoginController : Controller
	{
		MySql mySql = new MySql();
		private Account _mainAccount;
		// GET: Login
		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}
		[HttpPost]

		public ActionResult Index(Account account)
		{
			string view;
			try
			{
				account = mySql.CheckAccount(account.Login, account.Password);
				view = "~/Views/Login/Account.cshtml";
				List<Student> students = mySql.GetStudent(account.IdStudent);
				ViewBag.FirstName = students[0].FirstName;
				ViewBag.LastName = students[0].LastName;
				ViewBag.Classrooms = mySql.GetClassrooms();
				ViewBag.Subjects = mySql.GetSubjects();
				ViewBag.TypeOFClass = mySql.GetTypeOfClass();
			}
			catch(Exception)
			{
				view = "~/Views/Login/Error.cshtml";
			}
			_mainAccount = account;
			return View(view);
		}
		[HttpGet]
		public ActionResult Error()
		{
			return View();
		}
		[HttpPost]
		public void Error(int i)
		{
			Index();
		}
	

	[HttpGet]
		public ActionResult Logining()
		{
			return View();
		}

		[HttpPost]
		public string Logining(Account account)
		{
			account = mySql.CheckAccount(account.Login, account.Password);
			ViewBag.IdAccount = account.IdAccount;
			return account.IdAccount.ToString();
		}
    }
}