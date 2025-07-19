using FluentAssertions;
using InnoSoft.InventorySystem.Core.Entities.Categories;

namespace InnoSoft.InventorySystem.Tests.Core.Entities
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void Category_Creation_Should_Set_Properties_Correctly()
        {
            // Arrange
            var categoryId = Guid.NewGuid();

            // Act
            var category = new Category
            {
                Id = categoryId,
                IsDeleted = false
            };

            // Assert
            category.Id.Should().Be(categoryId);
            category.IsDeleted.Should().BeFalse();
        }

        [TestMethod]
        public void Category_Should_Support_Translations_Collection()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new Category
            {
                Id = categoryId,
                IsDeleted = false
            };

            var translations = new List<CategoryTranslation>
            {
                new CategoryTranslation
                {
                    LanguageId = Guid.NewGuid(),
                    Name = "Electronics",
                    Description = "Electronic devices and accessories",
                    TranslationRootId = categoryId
                }
            };

            // Act
            category.Translations = translations;

            // Assert
            category.Translations.Should().NotBeNull();
            category.Translations.Should().HaveCount(1);
            category.Translations.First().Name.Should().Be("Electronics");
        }

        [TestMethod]
        public void Category_Should_Allow_Multiple_Translations()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new Category
            {
                Id = categoryId,
                IsDeleted = false
            };

            var translations = new List<CategoryTranslation>
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
            };

            // Act
            category.Translations = translations;

            // Assert
            category.Translations.Should().HaveCount(2);
            category.Translations.Should().Contain(t => t.Name == "Electronics");
            category.Translations.Should().Contain(t => t.Name == "إلكترونيات");
        }

        [TestMethod]
        public void Category_IsDeleted_Should_Default_To_False()
        {
            // Arrange & Act
            var category = new Category
            {
                Id = Guid.NewGuid()
            };

            // Assert
            category.IsDeleted.Should().BeFalse();
        }
    }
}