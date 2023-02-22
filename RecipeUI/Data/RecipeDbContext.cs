using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RecipeUI.Models;

namespace RecipeUI.Data
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options)
        {

        }
        public DbSet<Recipe> Recipe { get; set; }

    }
}
