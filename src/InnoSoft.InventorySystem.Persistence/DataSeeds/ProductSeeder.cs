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
                    CategoryId = Guid.Parse("5AD29664-0E8E-4F26-BD51-08DDC5E14BD7"),
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
                    Id = Guid.Parse("AEF25B3B-3FFA-4067-81CF-9AAC53CF7689"),
                    IsDeleted = false,
                    PricePerPiece = 899.99m,
                    Quantity = 200,
                    AlertThresholdQuantity = 55,
                    CategoryId = Guid.Parse("5AD29664-0E8E-4F26-BD51-08DDC5E14BD7"),
                    Translations = new List<ProductTranslation>()
    {
        new ProductTranslation
        {
            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
            Name = "Samsung Galaxy S24",
            Description = "Flagship Samsung smartphone.",
            TranslationRootId = Guid.Parse("AEF25B3B-3FFA-4067-81CF-9AAC53CF7689")
        },
        new ProductTranslation
        {
            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
            Name = "سامسونج جالكسي S24",
            Description = "هاتف سامسونج الرائد.",
            TranslationRootId = Guid.Parse("AEF25B3B-3FFA-4067-81CF-9AAC53CF7689")
        }
    }
                });

                await repository.Add(new Product
                {
                    Id = Guid.Parse("C3E13D4E-4A7B-4347-9274-48432DC7766C"),
                    IsDeleted = false,
                    PricePerPiece = 1499.99m,
                    Quantity = 3,
                    AlertThresholdQuantity = 5,
                    CategoryId = Guid.Parse("E709710F-C033-4719-BD52-08DDC5E14BD7"),
                    Translations = new List<ProductTranslation>()
    {
        new ProductTranslation
        {
            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
            Name = "Dell XPS 15",
            Description = "High-performance laptop from Dell.",
            TranslationRootId = Guid.Parse("C3E13D4E-4A7B-4347-9274-48432DC7766C")
        },
        new ProductTranslation
        {
            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
            Name = "ديل XPS 15",
            Description = "حاسوب محمول عالي الأداء من ديل.",
            TranslationRootId = Guid.Parse("C3E13D4E-4A7B-4347-9274-48432DC7766C")
        }
    }
                });

                await repository.Add(new Product
                {
                    Id = Guid.Parse("2540A16D-82DA-4799-ADA2-43E44E0E712F"),
                    IsDeleted = false,
                    PricePerPiece = 1999.99m,
                    Quantity = 50,
                    AlertThresholdQuantity = 5,
                    CategoryId = Guid.Parse("E709710F-C033-4719-BD52-08DDC5E14BD7"),
                    Translations = new List<ProductTranslation>()
    {
        new ProductTranslation
        {
            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
            Name = "MacBook Pro 14",
            Description = "Apple's premium laptop for professionals.",
            TranslationRootId = Guid.Parse("2540A16D-82DA-4799-ADA2-43E44E0E712F")
        },
        new ProductTranslation
        {
            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
            Name = "ماك بوك برو 14",
            Description = "حاسوب أبل الفاخر للمحترفين.",
            TranslationRootId = Guid.Parse("2540A16D-82DA-4799-ADA2-43E44E0E712F")
        }
    }
                });


                await repository.Add(new Product
                {
                    Id = Guid.Parse("0B839744-08E7-494A-BEE4-A9EA5D952E93"),
                    IsDeleted = false,
                    PricePerPiece = 799.99m,
                    Quantity = 50,
                    AlertThresholdQuantity = 5,
                    CategoryId = Guid.Parse("77921FEC-78DC-47C0-BD53-08DDC5E14BD7"),
                    Translations = new List<ProductTranslation>()
    {
        new ProductTranslation
        {
            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
            Name = "iPad Air",
            Description = "Lightweight Apple tablet.",
            TranslationRootId = Guid.Parse("0B839744-08E7-494A-BEE4-A9EA5D952E93")
        },
        new ProductTranslation
        {
            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
            Name = "آيباد إير",
            Description = "جهاز لوحي خفيف من أبل.",
            TranslationRootId = Guid.Parse("0B839744-08E7-494A-BEE4-A9EA5D952E93")
        }
    }
                });

                await repository.Add(new Product
                {
                    Id = Guid.Parse("DE23F1A0-7D08-4F5F-A306-6E2FD974FED8"),
                    IsDeleted = false,
                    PricePerPiece = 899.99m,
                    Quantity = 50,
                    AlertThresholdQuantity = 5,
                    CategoryId = Guid.Parse("77921FEC-78DC-47C0-BD53-08DDC5E14BD7"),
                    Translations = new List<ProductTranslation>()
    {
        new ProductTranslation
        {
            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
            Name = "Galaxy Tab S9",
            Description = "Samsung's latest Android tablet.",
            TranslationRootId = Guid.Parse("DE23F1A0-7D08-4F5F-A306-6E2FD974FED8")
        },
        new ProductTranslation
        {
            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
            Name = "جالكسي تاب S9",
            Description = "أحدث جهاز لوحي من سامسونج.",
            TranslationRootId = Guid.Parse("DE23F1A0-7D08-4F5F-A306-6E2FD974FED8")
        }
    }
                });

                await repository.Add(new Product
                {
                    Id = Guid.Parse("E4089068-84AF-4A2D-82BA-6311FCCCD9D5"),
                    IsDeleted = false,
                    PricePerPiece = 1199.99m,
                    Quantity = 50,
                    AlertThresholdQuantity = 5,
                    CategoryId = Guid.Parse("77921FEC-78DC-47C0-BD53-08DDC5E14BD7"),
                    Translations = new List<ProductTranslation>()
    {
        new ProductTranslation
        {
            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
            Name = "Surface Pro 9",
            Description = "Microsoft 2-in-1 laptop/tablet.",
            TranslationRootId = Guid.Parse("E4089068-84AF-4A2D-82BA-6311FCCCD9D5")
        },
        new ProductTranslation
        {
            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
            Name = "سيرفس برو 9",
            Description = "جهاز مايكروسوفت 2 في 1.",
            TranslationRootId = Guid.Parse("E4089068-84AF-4A2D-82BA-6311FCCCD9D5")
        }
    }
                });

                await repository.Add(new Product
                {
                    Id = Guid.Parse("F27B4B7D-A1B6-47CE-882C-8B25DB80EBA8"),
                    IsDeleted = false,
                    PricePerPiece = 99.99m,
                    Quantity = 50,
                    AlertThresholdQuantity = 5,
                    CategoryId = Guid.Parse("05189D9B-F2C5-43E9-BD54-08DDC5E14BD7"),
                    Translations = new List<ProductTranslation>()
    {
        new ProductTranslation
        {
            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
            Name = "Logitech MX Master 3",
            Description = "High precision wireless mouse.",
            TranslationRootId = Guid.Parse("F27B4B7D-A1B6-47CE-882C-8B25DB80EBA8")
        },
        new ProductTranslation
        {
            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
            Name = "لوجيتك ماستر MX 3",
            Description = "فأرة لاسلكية عالية الدقة.",
            TranslationRootId = Guid.Parse("F27B4B7D-A1B6-47CE-882C-8B25DB80EBA8")
        }
    }
                });

                await repository.Add(new Product
                {
                    Id = Guid.Parse("A3C10332-C3C5-4356-81F1-91C6C5FF21E7"),
                    IsDeleted = false,
                    PricePerPiece = 499.99m,
                    Quantity = 7,
                    AlertThresholdQuantity = 5,
                    CategoryId = Guid.Parse("05189D9B-F2C5-43E9-BD54-08DDC5E14BD7"),
                    Translations = new List<ProductTranslation>()
    {
        new ProductTranslation
        {
            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
            Name = "Apple Watch Series 9",
            Description = "Smartwatch with fitness tracking.",
            TranslationRootId = Guid.Parse("A3C10332-C3C5-4356-81F1-91C6C5FF21E7")
        },
        new ProductTranslation
        {
            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
            Name = "ساعة أبل 9",
            Description = "ساعة ذكية مع تتبع اللياقة.",
            TranslationRootId = Guid.Parse("A3C10332-C3C5-4356-81F1-91C6C5FF21E7")
        }
    }
                });

                await repository.Add(new Product
                {
                    Id = Guid.Parse("1D4BDAF9-8C90-4BFC-B3DF-0FC2C66318A6"),
                    IsDeleted = false,
                    PricePerPiece = 349.99m,
                    Quantity = 150,
                    AlertThresholdQuantity = 5,
                    CategoryId = Guid.Parse("05189D9B-F2C5-43E9-BD54-08DDC5E14BD7"),
                    Translations = new List<ProductTranslation>()
    {
        new ProductTranslation
        {
            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
            Name = "Sony WH-1000XM5",
            Description = "Noise cancelling wireless headphones.",
            TranslationRootId = Guid.Parse("1D4BDAF9-8C90-4BFC-B3DF-0FC2C66318A6")
        },
        new ProductTranslation
        {
            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
            Name = "سوني WH-1000XM5",
            Description = "سماعات لاسلكية مانعة للضوضاء.",
            TranslationRootId = Guid.Parse("1D4BDAF9-8C90-4BFC-B3DF-0FC2C66318A6")
        }
    }
                });
            }

            await repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
