using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities;
using InnoSoft.InventorySystem.Core.Entities.Categories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Persistence.DataSeeds
{
    public class CategorySeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IRepository<Category>>();

            if (!repository.GetAll().Any())
            {
                await repository.Add(new Category
                {
                    Id = Guid.Parse("5AD29664-0E8E-4F26-BD51-08DDC5E14BD7"),
                    IsDeleted = false,
                    Translations = new List<CategoryTranslation>() {
                        new CategoryTranslation() {
                            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
                            Name = "Smartphones" ,
                            Description = "Advanced multifunctional smart mobile devices.",
                            TranslationRootId = Guid.Parse("5AD29664-0E8E-4F26-BD51-08DDC5E14BD7")
                        },
                        new CategoryTranslation() {
                            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
                            Name = "الهواتف الذكية" ,
                            Description = "أجهزة الهاتف الذكية المتطورة متعددة الاستخدامات.",
                            TranslationRootId = Guid.Parse("5AD29664-0E8E-4F26-BD51-08DDC5E14BD7")
                        }
                    }
                });

                await repository.Add(new Category
                {
                    Id = Guid.Parse("E709710F-C033-4719-BD52-08DDC5E14BD7"),
                    IsDeleted = false,
                    Translations = new List<CategoryTranslation>() {
                        new CategoryTranslation() {
                            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
                            Name = "Laptops" ,
                            Description = "Portable computers for daily and professional use.",
                            TranslationRootId = Guid.Parse("E709710F-C033-4719-BD52-08DDC5E14BD7")
                        },
                        new CategoryTranslation() {
                            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
                            Name = "الحواسيب المحمولة" ,
                            Description = "أجهزة الكمبيوتر المحمولة للاستخدامات اليومية والمهنية.",
                            TranslationRootId = Guid.Parse("E709710F-C033-4719-BD52-08DDC5E14BD7")
                        }
                    }
                });

                await repository.Add(new Category
                {
                    Id = Guid.Parse("77921FEC-78DC-47C0-BD53-08DDC5E14BD7"),
                    IsDeleted = false,
                    Translations = new List<CategoryTranslation>() {
                        new CategoryTranslation() {
                            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
                            Name = "Tablets" ,
                            Description = "Portable touchscreen devices used for work and entertainment.",
                            TranslationRootId = Guid.Parse("77921FEC-78DC-47C0-BD53-08DDC5E14BD7")
                        },
                        new CategoryTranslation() {
                            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
                            Name = "الأجهزة اللوحية" ,
                            Description = "أجهزة إلكترونية محمولة تعمل باللمس وتستخدم في العمل والترفيه.",
                            TranslationRootId = Guid.Parse("77921FEC-78DC-47C0-BD53-08DDC5E14BD7")
                        }
                    }
                });

                await repository.Add(new Category
                {
                    Id = Guid.Parse("05189D9B-F2C5-43E9-BD54-08DDC5E14BD7"),
                    IsDeleted = false,
                    Translations = new List<CategoryTranslation>() {
                        new CategoryTranslation() {
                            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
                            Name = "Accessories" ,
                            Description = "Additional tools and attachments for electronic devices.",
                            TranslationRootId = Guid.Parse("05189D9B-F2C5-43E9-BD54-08DDC5E14BD7")
                        },
                        new CategoryTranslation() {
                            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
                            Name = "إكسسوارات" ,
                            Description = "ملحقات وأدوات إضافية للأجهزة الإلكترونية.",
                            TranslationRootId = Guid.Parse("05189D9B-F2C5-43E9-BD54-08DDC5E14BD7")
                        }
                    }
                });


            }
            ;

            await repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
