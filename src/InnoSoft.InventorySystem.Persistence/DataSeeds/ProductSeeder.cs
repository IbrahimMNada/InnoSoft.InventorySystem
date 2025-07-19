using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities.Products;
using InnoSoft.InventorySystem.Core.Entities.Categories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Persistence.DataSeeds
{
    public class ProductSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IRepository<Product>>();

            if (!repository.GetAll().Any())
            {
                await repository.Add(new Product
                {
                    Id = Guid.Parse("A1B2C3D4-E5F6-4719-BD51-08DDC5E14BD7"),
                    IsDeleted = false,
                    PricePerPiece = 999.99m,
                    Quantity = 50,
                    AlertThresholdQuantity = 5,
                    CategoryId = Guid.Parse("5AD29664-0E8E-4F26-BD51-08DDC5E14BD7"), // Smartphones
                    Translations = new List<ProductTranslation>()
                    {
                        new ProductTranslation
                        {
                            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
                            Name = "iPhone 15 Pro",
                            Description = "Apple's latest flagship smartphone.",
                            TranslationRootId = Guid.Parse("A1B2C3D4-E5F6-4719-BD51-08DDC5E14BD7")
                        },
                        new ProductTranslation
                        {
                            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
                            Name = "آيفون 15 برو",
                            Description = "أحدث هاتف ذكي رائد من أبل.",
                            TranslationRootId = Guid.Parse("A1B2C3D4-E5F6-4719-BD51-08DDC5E14BD7")
                        }
                    }
                });

                await repository.Add(new Product
                {
                    Id = Guid.Parse("B2C3D4E5-F6A1-4719-BD52-08DDC5E14BD7"),
                    IsDeleted = false,
                    PricePerPiece = 1499.99m,
                    Quantity = 30,
                    AlertThresholdQuantity =55,
                    CategoryId = Guid.Parse("E709710F-C033-4719-BD52-08DDC5E14BD7"), // Laptops
                    Translations = new List<ProductTranslation>()
                    {
                        new ProductTranslation
                        {
                            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
                            Name = "Dell XPS 15",
                            Description = "High-performance laptop for professionals.",
                            TranslationRootId = Guid.Parse("B2C3D4E5-F6A1-4719-BD52-08DDC5E14BD7")
                        },
                        new ProductTranslation
                        {
                            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
                            Name = "ديل إكس بي إس 15",
                            Description = "حاسوب محمول عالي الأداء للمحترفين.",
                            TranslationRootId = Guid.Parse("B2C3D4E5-F6A1-4719-BD52-08DDC5E14BD7")
                        }
                    }
                });
            }

            await repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
