using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPF.Entities;
using SPF.Models.Items;

namespace SPF.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<SPF.Models.Items.Type> Types { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<TypeCategory> TypeCategories { get; set; }
        public DbSet<CategoryGroup> CategoryGroups { get; set; }
        public DbSet<ItemsSpecification> ItemsSpecification { get; set; }
    }
}