using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_App.Model
{
    public class Message
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int ChatId { get; set; }
        public string MessageContent { get; set; }
        public DateTime MessageDate { get; set; }
        [JsonIgnore]
        public Chat Chat { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }
    }
}
