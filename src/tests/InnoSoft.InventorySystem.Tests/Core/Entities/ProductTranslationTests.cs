using FluentAssertions;
using InnoSoft.InventorySystem.Core.Entities.Products;

namespace InnoSoft.InventorySystem.Tests.Core.Entities
{
    [TestClass]
    public class ProductTranslationTests
    {
        [TestMethod]
        public void ProductTranslation_Creation_Should_Set_Properties_Correctly()
        {
            // Arrange
            var languageId = Guid.NewGuid();
            var translationRootId = Guid.NewGuid();
            var name = "iPhone 15 Pro";
            var description = "Apple's latest flagship smartphone.";

            // Act
            var translation = new ProductTranslation
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
        public void ProductTranslation_Should_Allow_Null_Description()
        {
            // Arrange & Act
            var translation = new ProductTranslation
            {
                LanguageId = Guid.NewGuid(),
                Name = "Test Product",
                Description = null,
                TranslationRootId = Guid.NewGuid()
            };

            // Assert
            translation.Description.Should().BeNull();
            translation.Name.Should().NotBeNull();
        }

        [TestMethod]
        public void ProductTranslation_Should_Allow_Null_Name()
        {
            // Arrange & Act
            var translation = new ProductTranslation
            {
                LanguageId = Guid.NewGuid(),
                Name = null,
                Description = "Test Description",
                TranslationRootId = Guid.NewGuid()
            };

            // Assert
            translation.Name.Should().BeNull();
            translation.Description.Should().NotBeNull();
        }

        [TestMethod]
        public void ProductTranslation_Should_Handle_Unicode_Characters()
        {
            // Arrange
            var arabicName = "آيفون 15 برو";
            var arabicDescription = "أحدث هاتف ذكي رائد من أبل.";

            // Act
            var translation = new ProductTranslation
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

        [TestMethod]
        public void ProductTranslation_Should_Handle_Special_Characters()
        {
            // Arrange
            var nameWithSpecialChars = "Product™ & Co. - Model#123 (v2.0)";
            var descriptionWithSpecialChars = "Description with special chars: @#$%^&*()_+{}|:<>?[];',./";

            // Act
            var translation = new ProductTranslation
            {
                LanguageId = Guid.NewGuid(),
                Name = nameWithSpecialChars,
                Description = descriptionWithSpecialChars,
                TranslationRootId = Guid.NewGuid()
            };

            // Assert
            translation.Name.Should().Be(nameWithSpecialChars);
            translation.Description.Should().Be(descriptionWithSpecialChars);
        }

        [TestMethod]
        public void ProductTranslation_Should_Handle_Long_Strings()
        {
            // Arrange
            var longName = new string('A', 1000);
            var longDescription = new string('B', 5000);

            // Act
            var translation = new ProductTranslation
            {
                LanguageId = Guid.NewGuid(),
                Name = longName,
                Description = longDescription,
                TranslationRootId = Guid.NewGuid()
            };

            // Assert
            translation.Name.Should().Be(longName);
            translation.Name.Should().HaveLength(1000);
            translation.Description.Should().Be(longDescription);
            translation.Description.Should().HaveLength(5000);
        }

        [TestMethod]
        public void ProductTranslation_Should_Handle_Empty_Strings()
        {
            // Arrange & Act
            var translation = new ProductTranslation
            {
                LanguageId = Guid.NewGuid(),
                Name = string.Empty,
                Description = string.Empty,
                TranslationRootId = Guid.NewGuid()
            };

            // Assert
            translation.Name.Should().BeEmpty();
            translation.Description.Should().BeEmpty();
        }

        [TestMethod]
        public void ProductTranslation_Should_Handle_Whitespace_Strings()
        {
            // Arrange
            var whitespaceString = "   \t\n\r   ";

            // Act
            var translation = new ProductTranslation
            {
                LanguageId = Guid.NewGuid(),
                Name = whitespaceString,
                Description = whitespaceString,
                TranslationRootId = Guid.NewGuid()
            };

            // Assert
            translation.Name.Should().Be(whitespaceString);
            translation.Description.Should().Be(whitespaceString);
        }

        [TestMethod]
        public void ProductTranslation_Should_Set_Valid_Guids()
        {
            // Arrange
            var languageId = Guid.NewGuid();
            var translationRootId = Guid.NewGuid();

            // Act
            var translation = new ProductTranslation
            {
                LanguageId = languageId,
                Name = "Test Product",
                Description = "Test Description",
                TranslationRootId = translationRootId
            };

            // Assert
            translation.LanguageId.Should().NotBeEmpty();
            translation.TranslationRootId.Should().NotBeEmpty();
            translation.LanguageId.Should().Be(languageId);
            translation.TranslationRootId.Should().Be(translationRootId);
        }

        [TestMethod]
        public void ProductTranslation_Should_Allow_Empty_Guids()
        {
            // Arrange & Act
            var translation = new ProductTranslation
            {
                LanguageId = Guid.Empty,
                Name = "Test Product",
                Description = "Test Description",
                TranslationRootId = Guid.Empty
            };

            // Assert
            translation.LanguageId.Should().Be(Guid.Empty);
            translation.TranslationRootId.Should().Be(Guid.Empty);
        }

        [TestMethod]
        public void ProductTranslation_Should_Inherit_From_TranslationBase()
        {
            // Arrange & Act
            var translation = new ProductTranslation
            {
                LanguageId = Guid.NewGuid(),
                Name = "Test Product",
                Description = "Test Description",
                TranslationRootId = Guid.NewGuid()
            };

            // Assert
            translation.Should().BeAssignableTo<InnoSoft.InventorySystem.Core.Abstractions.TranslationBase>();
            translation.Should().BeAssignableTo<InnoSoft.InventorySystem.Core.Abstractions.ITranslation>();
        }

        [TestMethod]
        public void ProductTranslation_Should_Support_Multiple_Languages_For_Same_Product()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var englishLanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2");
            var arabicLanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44");

            // Act
            var englishTranslation = new ProductTranslation
            {
                LanguageId = englishLanguageId,
                Name = "iPhone 15 Pro",
                Description = "Apple's latest flagship smartphone.",
                TranslationRootId = productId
            };

            var arabicTranslation = new ProductTranslation
            {
                LanguageId = arabicLanguageId,
                Name = "آيفون 15 برو",
                Description = "أحدث هاتف ذكي رائد من أبل.",
                TranslationRootId = productId
            };

            // Assert
            englishTranslation.TranslationRootId.Should().Be(productId);
            arabicTranslation.TranslationRootId.Should().Be(productId);
            englishTranslation.TranslationRootId.Should().Be(arabicTranslation.TranslationRootId);
            englishTranslation.LanguageId.Should().NotBe(arabicTranslation.LanguageId);
        }

        [TestMethod]
        public void ProductTranslation_Should_Handle_HTML_Content()
        {
            // Arrange
            var htmlName = "<b>Product</b> &amp; <i>Company</i>";
            var htmlDescription = "<p>This is a <strong>description</strong> with <em>HTML</em> content.</p>";

            // Act
            var translation = new ProductTranslation
            {
                LanguageId = Guid.NewGuid(),
                Name = htmlName,
                Description = htmlDescription,
                TranslationRootId = Guid.NewGuid()
            };

            // Assert
            translation.Name.Should().Be(htmlName);
            translation.Description.Should().Be(htmlDescription);
        }

        [TestMethod]
        public void ProductTranslation_Should_Handle_JSON_Content()
        {
            // Arrange
            var jsonDescription = "{\"key\": \"value\", \"number\": 123, \"nested\": {\"inner\": \"text\"}}";

            // Act
            var translation = new ProductTranslation
            {
                LanguageId = Guid.NewGuid(),
                Name = "JSON Product",
                Description = jsonDescription,
                TranslationRootId = Guid.NewGuid()
            };

            // Assert
            translation.Description.Should().Be(jsonDescription);
        }

        [TestMethod]
        public void ProductTranslation_Should_Support_Equals_Comparison()
        {
            // Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();

            var translation1 = new ProductTranslation
            {
                LanguageId = id1,
                Name = "Test",
                Description = "Description",
                TranslationRootId = id2
            };

            var translation2 = new ProductTranslation
            {
                LanguageId = id1,
                Name = "Test",
                Description = "Description",
                TranslationRootId = id2
            };

            var translation3 = new ProductTranslation
            {
                LanguageId = Guid.NewGuid(),
                Name = "Different",
                Description = "Different",
                TranslationRootId = Guid.NewGuid()
            };

            // Act & Assert
            translation1.Should().NotBeSameAs(translation2);
            translation1.Should().NotBe(translation3);
        }
    }
}