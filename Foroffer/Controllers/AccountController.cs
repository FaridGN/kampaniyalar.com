﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foroffer.Models;
using Foroffer.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Foroffer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ForofferDbContext _offerDbContext;
        private readonly ILogger _logger;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ForofferDbContext offerDbContext, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _offerDbContext = offerDbContext;
            _logger = logger;
        }

        //Helper
        private void AddErrors(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)){

                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterModel registermodel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByEmailAsync(registermodel.Email);
                if (appUser != null)
                {
                    ModelState.AddModelError("", "Such user already exists");
                }
                else
                {
                    appUser = new AppUser
                    {
                        UserName = registermodel.UserName,
                        Email = registermodel.Email
                    };

                 IdentityResult result = await _userManager.CreateAsync(appUser, registermodel.Password);
                
                    if (result.Succeeded)
                    {

                       // _logger.LogInformation("New account was created successfully");
                           ViewBag.Alert = "Siz uğurla qeydiyyatdan keçdiniz";
                        await _signInManager.SignInAsync(appUser, isPersistent: false);

                        return View(registermodel);
                    }
                    AddErrors(result);
                }
                   
            }
            return View(registermodel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser(LoginModel loginmodel, string returnUrl=null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                AppUser currentuser = await _userManager.FindByEmailAsync(loginmodel.UserName);
                
                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(loginmodel.UserName, loginmodel.Password, loginmodel.RememberMe, lockoutOnFailure: true);
                    if (signInResult.Succeeded)
                    {
                        var user = this.User;
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        if (signInResult.IsLockedOut)
                        {
                            _logger.LogWarning("User account locked out");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Username or password is incorrect");

                        }
                    }

            }

            return View(loginmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutUser()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("You have logged out");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //Admin registration

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByEmailAsync(registerModel.Email);

                if(appUser != null)
                {
                    ModelState.AddModelError("", "This user already exists");
                }
                else
                {
                    appUser = new AppUser
                    {
                        UserName = registerModel.UserName,
                        Email = registerModel.Email
                    };

                    IdentityResult adminResult = await _userManager.CreateAsync(appUser, registerModel.Password);

                    if (adminResult.Succeeded)
                    {

                        IdentityResult result = await _userManager.AddToRoleAsync(appUser, RoleType.Admin.ToString());

                        if (result.Succeeded)
                        {
                            _logger.LogInformation("New account was created successfully");
                            await _signInManager.SignInAsync(appUser, isPersistent: false);

                            return RedirectToAction(nameof(AdminController.Admin), "Admin");
                        }       

                    }
                    AddErrors(adminResult);
                }
            }
            return View(registerModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAdmin(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(AdminController.Admin), "Admin");
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or password is incorrect");
                    }
                }
            }
            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutAdmin()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("You have logged out");
            return RedirectToAction(nameof(LoginAdmin));
        }

        [HttpGet]
        public IActionResult PostRequest()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PrivaPolicy()
        {
            return View();
        }

        //Partner registration

        [HttpGet]
        [AllowAnonymous]
         public async Task<IActionResult> RegisterPartner(int Id)
        {
            RegisterModel registerModel = new RegisterModel();
            registerModel.Company = await _offerDbContext.Companies.SingleOrDefaultAsync(x => x.Id == Id);
            return View(registerModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterPartner(RegisterModel registerModel, int Id)
        {
            if (ModelState.IsValid)
            {
                registerModel.Company = await _offerDbContext.Companies.SingleOrDefaultAsync(x => x.Id == Id);

                AppUser partnerUser = await _userManager.FindByEmailAsync(registerModel.Email);

                if(partnerUser != null)
                {
                    ModelState.AddModelError("", "This partner already exists");
                }
                else
                {
                    partnerUser = new AppUser
                    {
                        UserName = registerModel.UserName,
                        Email = registerModel.Email,
                        CompanyId = registerModel.Company.Id
                    };

                    IdentityResult partnerResult = await _userManager.CreateAsync(partnerUser, registerModel.Password);

                    if (partnerResult.Succeeded)
                    {
                        IdentityResult result = await _userManager.AddToRoleAsync(partnerUser, RoleType.Partner.ToString());

                        if (result.Succeeded)
                        {
                            await _signInManager.SignInAsync(partnerUser, isPersistent: false);
                            return RedirectToAction(nameof(PartnerController.Partner), "Partner");
                        }
                    }
                    AddErrors(partnerResult);
                }
            }
            return View(registerModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoginPartner()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginPartner(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction(nameof(PartnerController.Partner), "Partner");
                }
                else
                {
                    if (signInResult.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or password is incorrect");
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutPartner()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("You have logged out");
            return RedirectToAction(nameof(LoginPartner));
        }
    }
}