using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameworkWithMediatR
{
    public class SampleData
    {
        public static int customersCount = 1000;
        public static void Initialize(IServiceProvider serviceProvider, int customersCount)
        {
            var context = serviceProvider.GetRequiredService<TestContext>();
            context.Database.EnsureCreated();
            context.Database.ExecuteSqlRaw("DROP TABLE Addresses");
            context.Database.ExecuteSqlRaw("DROP TABLE Customers");
            context.Database.EnsureCreated();
            var random = new Random();
            for (int i = 0; i < customersCount; i++)
            {
                context.Customers.Add(new CustomerEntity()
                {
                    Addresses = new List<AddressEntity>()
                    {
                        new AddressEntity()
                        {
                            City = $"City {i}",
                            Country = $"Country {i}",
                            Code = $"Code {i}",
                            Name = $"Name {i}",
                        }
                    },
                    Revenue = i*random.Next(1,10),
                    Name = $"Name {i}",
                });
            }
            
            context.SaveChanges();
        }
        
    }
}
