using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityAttendee> ActivityAttendees { get; set; }
    public DbSet<Photo> Photos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ActivityAttendee>(x =>
        {
            x.HasKey(aa => new { aa.UserId, aa.ActivityId });

            x.HasOne(aa => aa.User)
                .WithMany(u => u.Activities)
                .HasForeignKey(aa => aa.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            x.HasOne(aa => aa.Activity)
                .WithMany(a => a.Attendees)
                .HasForeignKey(aa => aa.ActivityId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}
