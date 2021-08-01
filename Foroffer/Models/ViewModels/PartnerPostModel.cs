using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models.ViewModels
{
    public class PartnerPostModel
    {
        public int Id { get; set; }
        public Post Post { get; set; }
        public IFormFile PostImage { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Company> Companies { get; set; }
        public IEnumerable<Subcategory> Subcategories { get; set; }
        public IEnumerable<SelectListItem> SubcategoryList { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
