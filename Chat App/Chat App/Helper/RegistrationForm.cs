using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_App.Helper
{
    public class RegistrationForm
    {
        public string username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }
        public string password { get; set; }
    }
}
