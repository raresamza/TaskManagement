using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskManagement.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks { get; set; } 

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}