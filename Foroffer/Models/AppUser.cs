using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models
{
    public class AppUser:IdentityUser
    {
        public Company Company { get; set; }
        public int? CompanyId { get; set; }
    }
}
