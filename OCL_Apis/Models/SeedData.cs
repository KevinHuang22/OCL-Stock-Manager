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
                // Look for any Resources.
                if (context.Resources.Any())
                {
                    return;   // DB has been seeded
                }

                context.Resources.AddRange(
                    new Resource
                    {
                        Id = 20061320,
                        Order = "NMP3914",
                        Brand = "900g 纽奶乐3段",
                        Batch = "JH200613-20",
                        Tinplate = 1089,
                        Rejection = 211,
                        Good = 7520,
                        Bad = 569,
                        CanType = CanType._502,
                        ResourceStatus = ResourceStatus.Consumed,
                        Note = "Accepted by Evans on 12:09 08/08/2020\n"
                    },
                    new Resource
                    {
                        Id = 20082340,
                        Order = "YASHI14092",
                        Brand = "800g Yashily Kieember a1",
                        Batch = "JH200823-40",
                        Tinplate = 1200,
                        Rejection = 0,
                        Good = 0,
                        Bad = 0,
                        CanType = CanType._502,
                        ResourceStatus = ResourceStatus.Arrived,
                        Note = "Accepted by Evans on 07:09 08/08/2020\n"
                    },
                    new Resource
                    {
                        Id = 20061350,
                        Order = "NMP3914",
                        Brand = "900g 纽奶乐3段",
                        Batch = "JH200613-50",
                        Tinplate = 1100,
                        Rejection = 111,
                        Good = 7520,
                        Bad = 569,
                        CanType = CanType._502,
                        ResourceStatus = ResourceStatus.Production,
                        Note = "Accepted by Evans on 12:12 08/08/2020\n"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
