
using BethanysPieShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShopAPI.DbContexts
{
    public class BethanysPieShopDbContext: DbContext
    {
        public BethanysPieShopDbContext(DbContextOptions<BethanysPieShopDbContext>  options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
