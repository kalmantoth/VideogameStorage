using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  
using Microsoft.EntityFrameworkCore; 

using VideogameStorage.Authentication;
  
namespace VideogameStorage.Models  
{  
    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>  
    {  
        public DbSet<Videogame> Videogames { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}  
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);  
        }  
    }  
} 