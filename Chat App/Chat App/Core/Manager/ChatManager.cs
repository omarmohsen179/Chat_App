using Chat_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_App.Core.Manager
{
    public class ChatManager : Repository<Chat, AppDbContext>
    {
        public ChatManager(AppDbContext context) : base(context)
        {

        }


    }
   
}
