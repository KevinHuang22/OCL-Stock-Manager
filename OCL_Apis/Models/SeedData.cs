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
                // Look for any Tinplates.
                if (context.Tinplates.Any())
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
                );
                //context.SaveChanges();

                context.Orders.AddRange(
                    new Order
                    {
                        Id = 3914,
                        OrderNumber = "NMP3914",
                        CustomerId = 6
                    },
                    new Order
                    {
                        Id = 14092,
                        OrderNumber = "PO014092",
                        CustomerId = 5
                    }
                );
                context.SaveChanges();

                context.Tinplates.AddRange(
                    new Tinplate
                    {
                        Id = 20061320,
                        OrderId = 3914,
                        Brand = "900g 纽奶乐3段",
                        Batch = "JH200613-20",
                        TinplateQty = 1089,
                        Rejection = 211,
                        Good = 7520,
                        Bad = 569,
                        CanType = CanType._502,
                        TinplateStatus = TinplateStatus.Consumed,
                        Note = "Accepted by Evans on 12:09 08/08/2020\n"
                    },
                    new Tinplate
                    {
                        Id = 20082340,
                        OrderId = 14092,
                        Brand = "800g Yashily Kieember a1",
                        Batch = "JH200823-40",
                        TinplateQty = 1200,
                        Rejection = 0,
                        Good = 0,
                        Bad = 0,
                        CanType = CanType._502,
                        TinplateStatus = TinplateStatus.Arrived,
                        Note = "Accepted by Evans on 07:09 08/08/2020\n"
                    },
                    new Tinplate
                    {
                        Id = 20061350,
                        OrderId = 3914,
                        Brand = "900g 纽奶乐3段",
                        Batch = "JH200613-50",
                        TinplateQty = 1100,
                        Rejection = 111,
                        Good = 7520,
                        Bad = 569,
                        CanType = CanType._502,
                        TinplateStatus = TinplateStatus.Production,
                        Note = "Accepted by Evans on 12:12 08/08/2020\n"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
