using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TweetService.Domain.Models;

namespace TweetService.Infrastructure.Configurations;

public class TweetConfig : IEntityTypeConfiguration<Tweet>
{
    public void Configure(EntityTypeBuilder<Tweet> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasMany(t => t.Stickers)
            .WithMany(s => s.Tweets);
    }
}