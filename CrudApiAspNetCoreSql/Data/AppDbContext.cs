using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrudApiAspNetCoreSql.Models;

namespace CrudApiAspNetCoreSql.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<CrudApiAspNetCoreSql.Models.Category> Category { get; set; }

        public DbSet<CrudApiAspNetCoreSql.Models.MenuItem> MenuItem { get; set; }

        public DbSet<CrudApiAspNetCoreSql.Models.User> User { get; set; }
    }
}
