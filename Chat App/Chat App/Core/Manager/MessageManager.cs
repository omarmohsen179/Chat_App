using Chat_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_App.Core.Manager
{
    public class MessageManager : Repository<Message, AppDbContext>
    {
        public MessageManager(AppDbContext context) : base(context)
        {

        }
    }
}
