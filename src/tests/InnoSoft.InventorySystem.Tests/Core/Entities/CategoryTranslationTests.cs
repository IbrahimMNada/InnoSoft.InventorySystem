using FluentAssertions;
using InnoSoft.InventorySystem.Core.Entities.Categories;

namespace InnoSoft.InventorySystem.Tests.Core.Entities
{
    [TestClass]
    public class CategoryTranslationTests
    {
        [TestMethod]
        public void CategoryTranslation_Creation_Should_Set_Properties_Correctly()
        {
            // Arrange
            var languageId = Guid.NewGuid();
            var translationRootId = Guid.NewGuid();
            var name = "Electronics";
            var description = "Electronic devices and accessories";

            // Act
            var translation = new CategoryTranslation
            {
                LanguageId = languageId,
                Name = name,
                Description = description,
                TranslationRootId = translationRootId
            };

            // Assert
            translation.LanguageId.Should().Be(languageId);
            translation.Name.Should().Be(name);
            translation.Description.Should().Be(description);
            translation.TranslationRootId.Should().Be(translationRootId);
        }

        [TestMethod]
        public void CategoryTranslation_Should_Allow_Null_Description()
        {
            // Arrange & Act
            var translation = new CategoryTranslation
            {
                LanguageId = Guid.NewGuid(),
                Name = "Electronics",
                Description = null,
                TranslationRootId = Guid.NewGuid()
            };

            // Assert
            translation.Description.Should().BeNull();
            translation.Name.Should().NotBeNull();
        }

        [TestMethod]
        public void CategoryTranslation_Should_Handle_Unicode_Characters()
        {
            // Arrange
            var arabicName = "إلكترونيات";
            var arabicDescription = "الأجهزة الإلكترونية والملحقات";

            // Act
            var translation = new CategoryTranslation
            {
                LanguageId = Guid.NewGuid(),
                Name = arabicName,
                Description = arabicDescription,
                TranslationRootId = Guid.NewGuid()
            };

            // Assert
            translation.Name.Should().Be(arabicName);
            translation.Description.Should().Be(arabicDescription);
        }
    }
}