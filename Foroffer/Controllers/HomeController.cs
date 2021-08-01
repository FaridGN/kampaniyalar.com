using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Foroffer.Models;
using Microsoft.EntityFrameworkCore;
using Foroffer.Models.ViewModels;
using PagedList.Core;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace Foroffer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ForofferDbContext _offerDbContext;
        private readonly IStringLocalizer<HomeController> _localizer;
        static int category_id = 0;
        static int subId = 0;
        private string pageURL;
        private int today = DateTime.Today.Day;
        private int month = DateTime.Today.Month;
        private int year = DateTime.Today.Year;
        private int viewcounter = 0;

        public HomeController(ForofferDbContext offerDbContext, IStringLocalizer<HomeController> localizer)
        {
            _offerDbContext = offerDbContext;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            SlideViewModel svmodel = new SlideViewModel();
            svmodel.Images = await _offerDbContext.Images.ToListAsync();
            svmodel.Posts = _offerDbContext.Posts.Where(x => x.Company.Membership == "Paid").OrderByDescending(x => x.CreatedDate).Take(10);
            ViewBag.specDiscounts = await _offerDbContext.Posts.Where(y => y.SpecDiscount == true).ToListAsync();
            svmodel.Companies = await _offerDbContext.Companies.ToListAsync();
            ViewBag.Gifted = await _offerDbContext.Posts.Where(z => z.Gifted == true).ToListAsync();
            ViewBag.Others = _offerDbContext.Posts.OrderBy(k => k.CreatedDate).Take(4);
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            pageURL = "Home";
            svmodel.MainView = await _offerDbContext.MainViews.LastOrDefaultAsync();
            svmodel.MainView.ViewCount++;
            viewcounter = svmodel.MainView.ViewCount;
            await _offerDbContext.SaveChangesAsync();
            getStats();
            return View(svmodel);
        }

        public async Task<IActionResult> Category(int Id)
        {
            category_id = Id;
            SubViewModel subModel = new SubViewModel();
            subModel.Subcategories = await _offerDbContext.Subcategories.Where(x => x.CategoryId == Id).ToListAsync();
            subModel.Posts = await _offerDbContext.Posts.Where(y => y.Subcategory.CategoryId == Id).OrderByDescending(y => y.Id).ToListAsync();
            subModel.Companies = await _offerDbContext.Companies.ToListAsync();
            ViewBag.Posts = await _offerDbContext.Posts.Where(u => u.Subcategory.CategoryId == Id).GroupBy(u => new { u.Company.Name }).Select(u => u.First()).ToListAsync();
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            subModel.Category = await _offerDbContext.Categories.SingleOrDefaultAsync(c => c.Id == Id);
            pageURL = subModel.Category.Name;
            subModel.Category.ViewCount++;
            viewcounter = subModel.Category.ViewCount;
            await _offerDbContext.SaveChangesAsync();
            getStats();
            return View(subModel);
        }

        public async Task<IActionResult> ClothCat(int Id)
        {
            category_id = Id;
            SubViewModel subModel = new SubViewModel();
            subModel.Subcategories = await _offerDbContext.Subcategories.Where(x => x.CategoryId == Id).OrderBy(x => x.Name).ToListAsync();
            subModel.Posts = await _offerDbContext.Posts.Where(y => y.Subcategory.CategoryId == Id).OrderByDescending(y => y.Id).ToListAsync();
            subModel.Companies = _offerDbContext.Companies.ToList();
            ViewBag.Posts = _offerDbContext.Posts.Where(u => u.Subcategory.CategoryId == Id).ToList();
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            subModel.Category = await _offerDbContext.Categories.SingleOrDefaultAsync(c => c.Id == Id);
            pageURL = subModel.Category.Name;
            subModel.Category.ViewCount++;
            viewcounter = subModel.Category.ViewCount;
            await _offerDbContext.SaveChangesAsync();
            getStats();
            return View(subModel);
        }

        public async Task<IActionResult> Subcategory(int Id)
        {
            subId = Id;
            SubViewModel subView = new SubViewModel();
            subView.Subcategory = await _offerDbContext.Subcategories.Where(y => y.Id == Id).SingleOrDefaultAsync();
            subView.Posts = await _offerDbContext.Posts.Where(z => z.SubcategoryId == Id).OrderByDescending(z => z.Id).ToListAsync();
            subView.Companies = await _offerDbContext.Companies.ToListAsync();
            ViewBag.subCategories = await _offerDbContext.Subcategories.Where(c => c.CategoryId == subView.Subcategory.CategoryId).ToListAsync();
            ViewBag.SubPosts = await _offerDbContext.Posts.Where(n => n.SubcategoryId == Id).GroupBy(n => new { n.Company.Name }).Select(n => n.First()).ToListAsync();
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            /*
            pageURL = subView.Subcategory.Name;
            subView.Subcategory.ViewCount++;
            viewcounter = subView.Subcategory.ViewCount;
            getStats();
            */
            return View(subView);
        }

        public async Task<IActionResult> ClothSub(int Id)
        {
            subId = Id;
            SubViewModel subView = new SubViewModel();
            subView.Subcategory = await _offerDbContext.Subcategories.Where(y => y.Id == Id).SingleOrDefaultAsync();
            subView.Posts = await _offerDbContext.Posts.Where(z => z.SubcategoryId == Id).OrderByDescending(z => z.Id).ToListAsync();
            subView.Companies = await _offerDbContext.Companies.ToListAsync();
            subView.Subcategories = _offerDbContext.Subcategories.Where(c => c.CategoryId == subView.Subcategory.CategoryId).ToList();
            ViewBag.Posts = await _offerDbContext.Posts.Where(n => n.SubcategoryId == Id).ToListAsync();
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            return View(subView);
        }

        public async Task<IActionResult> ManCloth()
        {
            SubViewModel subView = new SubViewModel();
            subView.Subcategories = await _offerDbContext.Subcategories.Where(y => y.CategoryId == 7).OrderBy(y => y.Name).ToListAsync();
            subView.Posts = await _offerDbContext.Posts.Where(z => z.SubcategoryId > 71 && z.SubcategoryId < 81 || z.SubcategoryId == 97).OrderByDescending(z => z.Id).ToListAsync();
            subView.Companies = await _offerDbContext.Companies.ToListAsync();
            ViewBag.subCategories = _offerDbContext.Subcategories.Where(c => c.CategoryId == 7).ToList();
            ViewBag.Posts = await _offerDbContext.Posts.Where(n => n.SubcategoryId > 71 && n.SubcategoryId < 81 || n.SubcategoryId == 97).ToListAsync();
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            return View(subView);
        }

        public async Task<IActionResult> WomanCloth()
        {
            SubViewModel subView = new SubViewModel();
            subView.Subcategories = await _offerDbContext.Subcategories.Where(y => y.CategoryId == 7).OrderBy(y => y.Name).ToListAsync();
            subView.Posts = await _offerDbContext.Posts.Where(z => z.SubcategoryId > 80 && z.SubcategoryId < 90 || z.SubcategoryId == 97).OrderByDescending(z => z.Id).ToListAsync();
            subView.Companies = await _offerDbContext.Companies.ToListAsync();
            ViewBag.subCategories = _offerDbContext.Subcategories.Where(c => c.CategoryId == 7).ToList();
            ViewBag.Posts = await _offerDbContext.Posts.Where(n => n.SubcategoryId > 80 && n.SubcategoryId < 90 || n.SubcategoryId == 97).ToListAsync();
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            return View(subView);
        }

        public async Task<IActionResult> BabyCloth()
        {
            SubViewModel subView = new SubViewModel();
            subView.Subcategories = await _offerDbContext.Subcategories.Where(y => y.CategoryId == 7).OrderBy(y => y.Name).ToListAsync();
            subView.Posts = await _offerDbContext.Posts.Where(z => z.SubcategoryId > 89 && z.SubcategoryId < 96 || z.SubcategoryId == 97).OrderByDescending(z => z.Id).ToListAsync();
            subView.Companies = await _offerDbContext.Companies.ToListAsync();
            ViewBag.subCategories = _offerDbContext.Subcategories.Where(c => c.CategoryId == 7).ToList();
            ViewBag.Posts = await _offerDbContext.Posts.Where(n => n.SubcategoryId > 89 && n.SubcategoryId < 96 || n.SubcategoryId == 97).ToListAsync();
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            return View(subView);
        }

        //from menu dropdown
        public async Task<IActionResult> MenuSub(int Id)
        {
            SubViewModel subView = new SubViewModel();
            subView.Subcategory = await _offerDbContext.Subcategories.Where(y => y.Id == Id).SingleOrDefaultAsync();
            //category_id = subView.Subcategory.CategoryId;
            subView.Posts = await _offerDbContext.Posts.Where(z => z.SubcategoryId == Id).OrderByDescending(z => z.Id).ToListAsync();
            subView.Companies = await _offerDbContext.Companies.ToListAsync();
            ViewBag.subCategories = _offerDbContext.Subcategories.Where(c => c.CategoryId == subView.Subcategory.CategoryId).ToList();
            ViewBag.SubPosts = await _offerDbContext.Posts.Where(n => n.SubcategoryId == Id).ToListAsync();
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            return View("Subcategory", subView);
        }

        public async Task<IActionResult> getCompany(int Id)
        {
            SubViewModel subViewModel = new SubViewModel();
            subViewModel.Subcategories = await _offerDbContext.Subcategories.Where(x => x.CategoryId == category_id).ToListAsync();
            subViewModel.Companies = await _offerDbContext.Companies.Where(a => a.Id == Id).ToListAsync();
            subViewModel.Posts = await _offerDbContext.Posts.Where(b => b.CompanyId == Id && b.Subcategory.CategoryId == category_id).ToListAsync();
            ViewBag.subCategories = await _offerDbContext.Subcategories.Where(c => c.CategoryId == category_id).ToListAsync();
            ViewBag.Companies = await _offerDbContext.Companies.ToListAsync();
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            return View(subViewModel);
        }

        public async Task<IActionResult> getSubCompany(int Id)
        {
            SubViewModel svmodel = new SubViewModel();
            svmodel.Subcategory = await _offerDbContext.Subcategories.Where(z => z.Id == subId).SingleOrDefaultAsync();
            svmodel.Posts = await _offerDbContext.Posts.Where(t => t.SubcategoryId == subId && t.CompanyId == Id).ToListAsync();
            svmodel.Companies = await _offerDbContext.Companies.Where(k => k.Id == Id).ToListAsync();
            ViewBag.subCategories = await _offerDbContext.Subcategories.Where(c => c.CategoryId == category_id).ToListAsync();
            ViewBag.Companies = await _offerDbContext.Companies.ToListAsync();
            return View(svmodel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(int page = 1, int pageSize = 12)
        {
            var keyword = Request.Query["keyword"].ToString();
            var results = _offerDbContext.Posts.Where(p => p.Title.Contains(keyword) || p.Description.Contains(keyword));
            var comps = await _offerDbContext.Companies.ToListAsync();
            PagedList<Post> pagemodel = new PagedList<Post>(results, page, pageSize);
            ViewBag.keyword = keyword;
            return View(pagemodel);
        }

        [HttpGet]
        public IActionResult About()
        {
            ViewData["Category"] = _localizer["Kateqoriyalar"];
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        private void getStats()
        {
            var lastVisit = _offerDbContext.Webstats.Where(w => w.Page == pageURL).LastOrDefault();
            if(lastVisit != null && lastVisit.VisitDay < today-1)
            {
                Webstat webstat = new Webstat();
                webstat.Page = pageURL;
                webstat.VisitDate = DateTime.Today.AddDays(-1);
                webstat.VisitDay = webstat.VisitDate.Day;
                webstat.AsOfDate = viewcounter;
                webstat.VisitMonth = webstat.VisitDate.Month;
                webstat.VisitYear = webstat.VisitDate.Year;
                webstat.Daily = viewcounter - lastVisit.AsOfDate;
                _offerDbContext.Webstats.Add(webstat);
                _offerDbContext.SaveChanges();
            }
            else if(lastVisit != null && lastVisit.VisitMonth < month)
            {
                Webstat webstat = new Webstat();
                webstat.Page = pageURL;
                webstat.VisitDate = DateTime.Today.AddDays(-1);
                webstat.VisitDay = webstat.VisitDate.Day;
                webstat.AsOfDate = viewcounter;
                webstat.VisitMonth = webstat.VisitDate.Month;
                webstat.VisitYear = webstat.VisitDate.Year;
                webstat.Daily = viewcounter - lastVisit.AsOfDate;
                var checker = _offerDbContext.Webstats.Where(w => w.Page == webstat.Page && w.VisitDate.ToString("dd/MM/yyyy") == webstat.VisitDate.ToString("dd/MM/yyyy")).LastOrDefault();
                if (checker == null)
                {
                    _offerDbContext.Webstats.Add(webstat);
                    _offerDbContext.SaveChanges();
                }
            }
            else if(lastVisit != null && lastVisit.VisitYear < year)
            {
                Webstat webstat = new Webstat();
                webstat.Page = pageURL;
                webstat.VisitDate = DateTime.Today.AddDays(-1);
                webstat.VisitDay = webstat.VisitDate.Day;
                webstat.AsOfDate = viewcounter;
                webstat.VisitMonth = webstat.VisitDate.Month;
                webstat.VisitYear = webstat.VisitDate.Year;
                webstat.Daily = viewcounter - lastVisit.AsOfDate;
                _offerDbContext.Webstats.Add(webstat);
                _offerDbContext.SaveChanges();
            }
            else
            {
              /*
                Webstat webstat = new Webstat();
                webstat.Page = pageURL;
                webstat.VisitDate = DateTime.Today;
                webstat.VisitDay = webstat.VisitDate.Day;
                webstat.AsOfDate = viewcounter;
                webstat.Daily = viewcounter;
                webstat.VisitMonth = webstat.VisitDate.Month;
                webstat.VisitYear = webstat.VisitDate.Year;
                _offerDbContext.Webstats.Add(webstat);
                _offerDbContext.SaveChanges();
              */
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
