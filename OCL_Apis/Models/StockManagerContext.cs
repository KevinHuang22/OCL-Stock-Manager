using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCL_Apis.Models
{
    public class StockManagerContext : DbContext
    {
        public StockManagerContext(DbContextOptions<StockManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Resource> Resources { get; set; }

    }
}
