using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Persistence.DataSeeds
{
    public class LanguageSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IRepository<Language>>();

            if (!repository.GetAll().Any())
            {
                await repository.Add(new Language
                {
                    Id = Guid.Parse("b6a7d1f6-3d23-4c5c-9a01-7f8e9b8b68e2"),
                    Name = "English",
                    Abbreviation = "en",
                    IsDeleted = false
                });

                await repository.Add(new Language
                {
                    Id = Guid.Parse("d9f2c9d7-8b6f-4a28-90de-4f58d3c68c44"),
                    Name = "Arabic",
                    Abbreviation = "ar",
                    IsDeleted = false
                });

            }
            ;

            await repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
