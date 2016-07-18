using EmojiBot.Web.Infrastructure;
using EmojiBot.Web.Models;
using Ninject;
using System;
using System.Web.Mvc;

namespace EmojiBot.Web.Controllers
{
	[Authorize]
	public class AccountController : BaseController
	{
		[Inject]
		public IAuthenticationProvider _authenticationProvider { get; set; }

		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginViewModel model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (!_authenticationProvider.ValidateUser(model.Email, model.Password))
			{
				ModelState.AddModelError("*", "We couldn't find your account. Check your password and try again");
			}

			_authenticationProvider.SignIn(model.Email, model.RememberMe);

			if (!String.IsNullOrWhiteSpace(returnUrl))
				return Redirect(returnUrl);

			return RedirectToAction("Index", "Home");
		}

		[AllowAnonymous]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = _authenticationProvider.CreateUser(model.Email, model.Password);

				DatabaseSession.Save(user);

				DatabaseSession.Transaction.Commit();
				DatabaseSession.BeginTransaction();

				return RedirectToAction("Login");
			}

			return View(model);
		}

		public ActionResult LogOut()
		{
			_authenticationProvider.SignOut();

			return RedirectToAction("Index", "Home");
		}
	}
}