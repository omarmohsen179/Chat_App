using Chat_App.Core.Manager;
using Chat_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_App.Core
{
    public interface IUnitOfWork
    {
        public ChatManager ChatManager { get;  }
        public MessageManager MessageManager { get;  }
    }
}
