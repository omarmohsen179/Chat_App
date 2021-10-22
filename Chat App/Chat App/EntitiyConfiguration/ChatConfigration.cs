using Chat_App.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_App.EntitiyConfiguration
{
    public class ChatConfigration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasMany(b => b.Messages).WithOne(b => b.Chat).HasForeignKey(b=>b.ChatId);

        }
    }
}
