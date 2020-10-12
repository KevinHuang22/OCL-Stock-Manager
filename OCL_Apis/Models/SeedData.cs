using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCL_Apis.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StockManagerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<StockManagerContext>>()))
            {
                // Look for any Customers.
                if (context.Customers.Any())
                {
                    return;   // DB has been seeded
                }
                context.Customers.AddRange(
                    new Customer
                    {
                        Name = "Yashili"
                    },
                    new Customer
                    {
                        Name = "Pure Dairy"
                    }
                ) ;
                context.SaveChanges();
            }
        }
    }
}
