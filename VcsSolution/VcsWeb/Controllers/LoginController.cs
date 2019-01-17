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
		// GET: Login
		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}
		[HttpPost]

		public ActionResult Index(Account account)
		{
			int idaccount = mySql.CheckAccount(account.Login, account.Password);
			ViewBag.Classrooms = mySql.GetClassrooms();
			ViewBag.Subjects = mySql.GetSubjects();
			ViewBag.TypeOFClass = mySql.GetTypeOfClass();
			ViewBag.IdAccount = idaccount;
			return View("~/Views/Login/Account.cshtml");
		}

		[HttpGet]
		public ActionResult Logining()
		{
			return View();
		}

		[HttpPost]
		public string Logining(Account account)
		{
			account.IdAccount = mySql.CheckAccount(account.Login, account.Password);
			ViewBag.IdAccount = account.IdAccount;
			return account.IdAccount.ToString();
		}
    }
}