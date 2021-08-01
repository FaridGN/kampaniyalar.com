using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Foroffer.Models;
using Foroffer.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Foroffer.Controllers
{
    public class PartnerController : Controller
    {
        private readonly ForofferDbContext _offerDbContext;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public PartnerController(ForofferDbContext offerDbContext, IHostingEnvironment env, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _offerDbContext = offerDbContext;
            _env = env;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
         public async Task<IActionResult> Partner()
        {
            if (_signInManager.IsSignedIn(User) && User.IsInRole("Partner"))
            {
                AppUser user = await _userManager.GetUserAsync(HttpContext.User);

                PartnerPostModel postModel = new PartnerPostModel();
                postModel.Posts = await _offerDbContext.Posts.Where(x => x.CompanyId == user.CompanyId).ToListAsync();
                postModel.Companies = await _offerDbContext.Companies.ToListAsync();

                foreach (Post item in postModel.Posts)
                {
                    if (item.ExpirationDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                    {
                        _offerDbContext.Posts.Remove(item);
                        await _offerDbContext.SaveChangesAsync();
                    }
                }
                return View(postModel);
            }
            else
            {
                ModelState.AddModelError("", "You are not signed in as a Partner");
            }
            return View();
        }

        //CRUD

        [HttpGet]
        public async Task<IActionResult> AddPost()
        {
            PartnerPostModel pModel = new PartnerPostModel();
            pModel.Categories = await _offerDbContext.Categories.ToListAsync();
            pModel.CategoryList = await _offerDbContext.Categories.Select(y => new SelectListItem()
            {
                Value = y.Id.ToString(),
                Text = y.Name

            }).ToListAsync();

            if (_signInManager.IsSignedIn(User) && User.IsInRole("Partner"))
            {
                return View(pModel);
            }
            else
            {
                 return RedirectToAction(nameof(AccountController.LoginPartner), "Account");
            }               
        }

        public IActionResult getSubItems(int Id)
        {
            List<Subcategory> list = new List<Subcategory>();
            list = _offerDbContext.Subcategories.Where(a => a.CategoryId == Id).ToList();
            return Json(new SelectList(list, "Id", "Name"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPost(Post post, IFormFile file, DateTime startDate, DateTime endDate)
        {
            if (ModelState.IsValid)
            {
                if (file == null || file.Length == 0)
                {
                    ModelState.AddModelError("", "No file selected");
                }

                string filepath = Path.Combine(_env.WebRootPath, "images", Path.GetFileName(file.FileName));

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                PartnerPostModel pModel = new PartnerPostModel();
                pModel.Categories = await _offerDbContext.Categories.ToListAsync();
                pModel.CategoryList = await _offerDbContext.Categories.Select(y => new SelectListItem()
                {
                    Value = y.Id.ToString(),
                    Text = y.Name
                }).ToListAsync();

                if (_signInManager.IsSignedIn(User))
                {
                    AppUser currentuser = await _userManager.GetUserAsync(HttpContext.User);

                    if(currentuser.CompanyId != null)
                    {
                        post.CompanyId = (int)currentuser.CompanyId;
                    }
                }
                else
                {
                    return View();
                }

                post.Gifted = false;
                post.SpecDiscount = false;
                post.CreatedDate = startDate;
                post.ExpirationDate = endDate;
                post.Image = file.FileName;
                _offerDbContext.Posts.Add(post);
                await _offerDbContext.SaveChangesAsync();
                return RedirectToAction("Partner", "Partner");
            }
            else
            {
                ModelState.AddModelError("", "Couldn't create");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> PostEdit(int Id)
        {
            PartnerPostModel pModel = new PartnerPostModel();
            pModel.Categories = await _offerDbContext.Categories.ToListAsync();
            pModel.Post = await _offerDbContext.Posts.SingleOrDefaultAsync(c => c.Id == Id);

            pModel.SubcategoryList = await _offerDbContext.Subcategories.OrderBy(a => a.Name).Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name

            }).ToListAsync();

            if (_signInManager.IsSignedIn(User) && User.IsInRole("Partner"))
            {
                return View(pModel);
            }
            else
            {
                return RedirectToAction(nameof(AccountController.LoginPartner), "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostEdit(Post post, int Id, DateTime startDate, DateTime endDate)
        {
            if (ModelState.IsValid)
            {
                PartnerPostModel pModel = new PartnerPostModel();
                pModel.Post = await _offerDbContext.Posts.SingleOrDefaultAsync(c => c.Id == Id);

                if (pModel.Post != null && _signInManager.IsSignedIn(User))
                {
                    AppUser appUser = await _userManager.GetUserAsync(HttpContext.User);

                    post.CompanyId = (int)appUser.CompanyId;
                    post.CreatedDate = startDate;
                    post.ExpirationDate = endDate;
                    pModel.Post.Title = post.Title;
                    pModel.Post.Description = post.Description;
                    pModel.Post.CreatedDate = post.CreatedDate;
                    pModel.Post.ExpirationDate = post.ExpirationDate;
                    pModel.Post.URL = post.URL;
                    pModel.Post.CompanyId = post.CompanyId;
                    pModel.Post.SubcategoryId = post.SubcategoryId;
                    pModel.Post.CompanyId = post.CompanyId;

                    await _offerDbContext.SaveChangesAsync();
                }
                return RedirectToAction("Partner", "Partner");
            }
            else
            {
                ModelState.AddModelError("", "Couldn't edit");
                return View();
            } 
        }

        [HttpGet]
        public async Task<IActionResult> EditImage(int Id)
        {
            PartnerPostModel pModel = new PartnerPostModel();
            pModel.Post = await _offerDbContext.Posts.SingleOrDefaultAsync(z => z.Id == Id);
            if (_signInManager.IsSignedIn(User) && User.IsInRole("Partner"))
            {
                return View(pModel);
            }
            else
            {
                return RedirectToAction(nameof(AccountController.LoginPartner), "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImage(Post post, int Id, IFormFile file)
        {
            PartnerPostModel pModel = new PartnerPostModel();
            pModel.Post = await _offerDbContext.Posts.SingleOrDefaultAsync(z => z.Id == Id);
           
            if(file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "no file selected");
                return View();
            }

            string filepath = Path.Combine(_env.WebRootPath, "images", Path.GetFileName(file.FileName));

            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            post.Image = file.FileName;
            pModel.Post.Image = post.Image;

            await _offerDbContext.SaveChangesAsync();

            return RedirectToAction("Partner", "Partner");
        }

        [HttpGet]
        public async Task<IActionResult> PostDelete(int Id)
        {
            PartnerPostModel pModel = new PartnerPostModel();
            pModel.Post = await _offerDbContext.Posts.SingleOrDefaultAsync(z => z.Id == Id);
            if (_signInManager.IsSignedIn(User) && User.IsInRole("Partner"))
            {
                return View(pModel);
            }
            else
            {
                return RedirectToAction(nameof(AccountController.LoginPartner), "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(int Id, Post post)
        {
            PartnerPostModel pModel = new PartnerPostModel();
            pModel.Post = await _offerDbContext.Posts.SingleOrDefaultAsync(z => z.Id == Id);
            post = pModel.Post;

            _offerDbContext.Posts.Remove(post);
            await _offerDbContext.SaveChangesAsync();

            return RedirectToAction("Partner", "Partner");
        }
    }
}