using InnoSoft.InventorySystem.Core.Entities.Categories;
using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;

namespace InnoSoft.InventorySystem.Tests.Helpers
{
    public static class CategoryTestDataBuilder
    {
        public static Category CreateValidCategory(Guid? id = null)
        {
            var categoryId = id ?? Guid.NewGuid();
            
            return new Category
            {
                Id = categoryId,
                IsDeleted = false,
                Translations = new List<CategoryTranslation>
                {
                    new CategoryTranslation
                    {
                        LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"), // English
                        Name = "Electronics",
                        Description = "Electronic devices and accessories",
                        TranslationRootId = categoryId
                    },
                    new CategoryTranslation
                    {
                        LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"), // Arabic
                        Name = "إلكترونيات",
                        Description = "الأجهزة الإلكترونية والملحقات",
                        TranslationRootId = categoryId
                    }
                }
            };
        }

        public static CategoryTranslation CreateValidCategoryTranslation(Guid? categoryId = null, string language = "en")
        {
            var rootId = categoryId ?? Guid.NewGuid();
            var languageId = language == "en" 
                ? Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2")
                : Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44");

            return new CategoryTranslation
            {
                LanguageId = languageId,
                Name = language == "en" ? "Electronics" : "إلكترونيات",
                Description = language == "en" ? "Electronic devices and accessories" : "الأجهزة الإلكترونية والملحقات",
                TranslationRootId = rootId
            };
        }

        public static CreateCategoryCommand CreateValidCreateCategoryCommand()
        {
            return new CreateCategoryCommand
            {
                Translations = new List<CategoryTranslationDto>
                {
                    new CategoryTranslationDto
                    {
                        Name = "Electronics",
                        Description = "Electronic devices and accessories",
                        Language = "en"
                    },
                    new CategoryTranslationDto
                    {
                        Name = "إلكترونيات",
                        Description = "الأجهزة الإلكترونية والملحقات",
                        Language = "ar"
                    }
                }
            };
        }

        public static UpdateCategoryCommand CreateValidUpdateCategoryCommand(Guid? categoryId = null)
        {
            return new UpdateCategoryCommand
            {
                Id = categoryId ?? Guid.NewGuid(),
                Translations = new List<CategoryTranslationDto>
                {
                    new CategoryTranslationDto
                    {
                        Name = "Updated Electronics",
                        Description = "Updated electronic devices and accessories",
                        Language = "en"
                    },
                    new CategoryTranslationDto
                    {
                        Name = "إلكترونيات محدثة",
                        Description = "الأجهزة الإلكترونية والملحقات المحدثة",
                        Language = "ar"
                    }
                }
            };
        }

        public static CategoryAdministrationDto CreateValidCategoryAdministrationDto(Guid? id = null)
        {
            var categoryId = id ?? Guid.NewGuid();
            
            return new CategoryAdministrationDto
            {
                Id = categoryId,
                Translations = new List<CategoryTranslationDto>
                {
                    new CategoryTranslationDto
                    {
                        Name = "Electronics",
                        Description = "Electronic devices and accessories",
                        Language = "en"
                    },
                    new CategoryTranslationDto
                    {
                        Name = "إلكترونيات",
                        Description = "الأجهزة الإلكترونية والملحقات",
                        Language = "ar"
                    }
                }
            };
        }

        public static CategoryDto CreateValidCategoryDto(Guid? id = null)
        {
            return new CategoryDto
            {
                Id = id ?? Guid.NewGuid(),
                Name = "Electronics",
                Description = "Electronic devices and accessories"
            };
        }

        public static CategoryTranslationDto CreateValidCategoryTranslationDto(string language = "en")
        {
            return new CategoryTranslationDto
            {
                Name = language == "en" ? "Electronics" : "إلكترونيات",
                Description = language == "en" ? "Electronic devices and accessories" : "الأجهزة الإلكترونية والملحقات",
                Language = language
            };
        }

        public static List<Category> CreateCategoryList(int count = 3)
        {
            var categories = new List<Category>();
            
            for (int i = 0; i < count; i++)
            {
                var categoryId = Guid.NewGuid();
                categories.Add(new Category
                {
                    Id = categoryId,
                    IsDeleted = false,
                    Translations = new List<CategoryTranslation>
                    {
                        new CategoryTranslation
                        {
                            LanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2"),
                            Name = $"Category {i + 1}",
                            Description = $"Description for category {i + 1}",
                            TranslationRootId = categoryId
                        },
                        new CategoryTranslation
                        {
                            LanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44"),
                            Name = $"فئة {i + 1}",
                            Description = $"وصف الفئة {i + 1}",
                            TranslationRootId = categoryId
                        }
                    }
                });
            }
            
            return categories;
        }

        public static Category CreateCategoryWithSingleTranslation(string language = "en", Guid? id = null)
        {
            var categoryId = id ?? Guid.NewGuid();
            
            return new Category
            {
                Id = categoryId,
                IsDeleted = false,
                Translations = new List<CategoryTranslation>
                {
                    CreateValidCategoryTranslation(categoryId, language)
                }
            };
        }

        public static Category CreateDeletedCategory(Guid? id = null)
        {
            var category = CreateValidCategory(id);
            category.IsDeleted = true;
            return category;
        }
    }
}