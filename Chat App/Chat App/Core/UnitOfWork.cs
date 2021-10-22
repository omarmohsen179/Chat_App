using Chat_App.Core.Manager;
using Chat_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_App.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        private ChatManager chatManager;

        public ChatManager ChatManager
        {

            get
            {
                if (chatManager == null)
                {
                    chatManager = new ChatManager(context);
                }
                return chatManager;
            }

        }
        private MessageManager messageManager;

        public MessageManager MessageManager
        {

            get
            {
                if (messageManager == null)
                {
                    messageManager = new MessageManager(context);
                }
                return messageManager;
            }

        }
    }
}
