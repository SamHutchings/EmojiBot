﻿using EmojiBot.Web.Models;
using System;
using System.Web.Mvc;

namespace EmojiBot.Web.Controllers
{
	[Authorize]
	public class AccountController : BaseController
	{
		[AllowAnonymous]
		[HttpGet]
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

			if (!AuthenticationProvider.ValidateUser(model.Email, model.Password))
			{
				ModelState.AddModelError("*", "We couldn't find your account. Check your password and try again");

				return View(model);
			}

			AuthenticationProvider.SignIn(model.Email, model.RememberMe);

			if (!String.IsNullOrWhiteSpace(returnUrl))
				return Redirect(returnUrl);

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public ActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ChangePassword(ChangePasswordModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			if (!AuthenticationProvider.ChangePassword(AuthenticatedUser, model.OldPassword, model.NewPassword))
			{
				ModelState.AddModelError("", "Your password could not be changed. Please ensure your old password is correct");

				return View(model);
			}

			return RedirectToAction("Index", "Home");
		}

		public ActionResult LogOut()
		{
			AuthenticationProvider.SignOut();

			return RedirectToAction("Index", "Home");
		}
	}
}