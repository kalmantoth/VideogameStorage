using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;


namespace VideogameStorage.Models
{
    public class VideogameContext : DbContext
    {
        public DbSet<Videogame> Videogames { get; set; }
        public VideogameContext(DbContextOptions<VideogameContext> options)
            : base(options){}
    }
}
