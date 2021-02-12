using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrudApiAspNetCoreSql.Models;

namespace CrudApiAspNetCoreSql.Data
{
    public class CategoryContext : DbContext
    {
        public CategoryContext (DbContextOptions<CategoryContext> options)
            : base(options)
        {
        }

        public DbSet<CrudApiAspNetCoreSql.Models.Category> Category { get; set; }
    }
}
