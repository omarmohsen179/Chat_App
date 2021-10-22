using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_App.Model
{
    public class Chat
    {
        public int Id { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public string Type { get; set; }
        public string ChatName { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
