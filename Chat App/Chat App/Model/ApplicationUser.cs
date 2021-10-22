using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_App.Model
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Chat> Users { get; set; }
    }
}
