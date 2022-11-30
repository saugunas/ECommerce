using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Db
{
    public class ProductsDbcontext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductsDbcontext(DbContextOptions options): base(options)
        {

        }
    }
}
