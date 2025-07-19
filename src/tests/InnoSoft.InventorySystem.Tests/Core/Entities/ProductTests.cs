using InnoSoft.InventorySystem.Core.Entities.Products;
using InnoSoft.InventorySystem.Core.Entities.Categories;
using FluentAssertions;
using System.Linq;

namespace InnoSoft.InventorySystem.Tests.Core.Entities
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void Product_Creation_Should_Set_Properties_Correctly()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var category = new Category { Id = categoryId };
            var pricePerPiece = 999.99m;
            var quantity = 50.0;
            var alertThreshold = 5.0;

            // Act
            var product = new Product
            {
                Id = productId,
                PricePerPiece = pricePerPiece,
                Quantity = quantity,
                AlertThresholdQuantity = alertThreshold,
                CategoryId = categoryId,
                Category = category,
                IsDeleted = false
            };

            // Assert
            product.Id.Should().Be(productId);
            product.PricePerPiece.Should().Be(pricePerPiece);
            product.Quantity.Should().Be(quantity);
            product.AlertThresholdQuantity.Should().Be(alertThreshold);
            product.CategoryId.Should().Be(categoryId);
            product.Category.Should().Be(category);
            product.IsDeleted.Should().BeFalse();
        }

        [TestMethod]
        public void Product_Should_Allow_Null_LastLowStockWarningDate()
        {
            // Arrange & Act
            var product = new Product
            {
                Id = Guid.NewGuid(),
                PricePerPiece = 100m,
                Quantity = 10,
                AlertThresholdQuantity = 5,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid() },
                LastLowStockWarningDate = null
            };

            // Assert
            product.LastLowStockWarningDate.Should().BeNull();
        }

        [TestMethod]
        public void Product_Should_Set_LastLowStockWarningDate()
        {
            // Arrange
            var warningDate = DateTime.UtcNow;
            var product = new Product
            {
                Id = Guid.NewGuid(),
                PricePerPiece = 100m,
                Quantity = 10,
                AlertThresholdQuantity = 5,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid() }
            };

            // Act
            product.LastLowStockWarningDate = warningDate;

            // Assert
            product.LastLowStockWarningDate.Should().Be(warningDate);
        }

        [TestMethod]
        public void Product_Should_Support_Translations_Collection()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                PricePerPiece = 100m,
                Quantity = 10,
                AlertThresholdQuantity = 5,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid() }
            };

            var translations = new List<ProductTranslation>
            {
                new ProductTranslation
                {
                    LanguageId = Guid.NewGuid(),
                    Name = "Test Product",
                    Description = "Test Description",
                    TranslationRootId = productId
                }
            };

            // Act
            product.Translations = translations;

            // Assert
            product.Translations.Should().NotBeNull();
            product.Translations.Should().HaveCount(1);
            product.Translations.First().Name.Should().Be("Test Product");
            product.Translations.First().Description.Should().Be("Test Description");
            product.Translations.First().TranslationRootId.Should().Be(productId);
        }

        [TestMethod]
        public void Product_Should_Handle_Multiple_Translations()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var englishLanguageId = Guid.Parse("B6A7D1F6-3D23-4C5C-9A01-7F8E9B8B68E2");
            var arabicLanguageId = Guid.Parse("D9F2C9D7-8B6F-4A28-90DE-4F58D3C68C44");

            var product = new Product
            {
                Id = productId,
                PricePerPiece = 999.99m,
                Quantity = 50,
                AlertThresholdQuantity = 5,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid() }
            };

            var translations = new List<ProductTranslation>
            {
                new ProductTranslation
                {
                    LanguageId = englishLanguageId,
                    Name = "iPhone 15 Pro",
                    Description = "Apple's latest flagship smartphone.",
                    TranslationRootId = productId
                },
                new ProductTranslation
                {
                    LanguageId = arabicLanguageId,
                    Name = "آيفون 15 برو",
                    Description = "أحدث هاتف ذكي رائد من أبل.",
                    TranslationRootId = productId
                }
            };

            // Act
            product.Translations = translations;

            // Assert
            product.Translations.Should().HaveCount(2);
            product.Translations.Should().Contain(t => t.Name == "iPhone 15 Pro");
            product.Translations.Should().Contain(t => t.Name == "آيفون 15 برو");
            product.Translations.Should().OnlyContain(t => t.TranslationRootId == productId);
        }

        [TestMethod]
        public void Product_Should_Handle_Zero_Price()
        {
            // Arrange & Act
            var product = new Product
            {
                Id = Guid.NewGuid(),
                PricePerPiece = 0m,
                Quantity = 10,
                AlertThresholdQuantity = 5,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid() }
            };

            // Assert
            product.PricePerPiece.Should().Be(0m);
        }

        [TestMethod]
        public void Product_Should_Handle_High_Precision_Decimal_Price()
        {
            // Arrange
            var highPrecisionPrice = 1234.56789m;

            // Act
            var product = new Product
            {
                Id = Guid.NewGuid(),
                PricePerPiece = highPrecisionPrice,
                Quantity = 10,
                AlertThresholdQuantity = 5,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid() }
            };

            // Assert
            product.PricePerPiece.Should().Be(highPrecisionPrice);
        }

        [TestMethod]
        public void Product_Should_Handle_Fractional_Quantities()
        {
            // Arrange
            var fractionalQuantity = 10.5;
            var fractionalThreshold = 2.25;

            // Act
            var product = new Product
            {
                Id = Guid.NewGuid(),
                PricePerPiece = 100m,
                Quantity = fractionalQuantity,
                AlertThresholdQuantity = fractionalThreshold,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid() }
            };

            // Assert
            product.Quantity.Should().Be(fractionalQuantity);
            product.AlertThresholdQuantity.Should().Be(fractionalThreshold);
        }

        [TestMethod]
        public void Product_Should_Identify_Low_Stock_Condition()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                PricePerPiece = 100m,
                Quantity = 3, // Below threshold
                AlertThresholdQuantity = 5,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid() }
            };

            // Act & Assert
            (product.Quantity <= product.AlertThresholdQuantity).Should().BeTrue("Product should be in low stock condition");
        }

        [TestMethod]
        public void Product_Should_Identify_Sufficient_Stock_Condition()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                PricePerPiece = 100m,
                Quantity = 10, // Above threshold
                AlertThresholdQuantity = 5,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid() }
            };

            // Act & Assert
            (product.Quantity > product.AlertThresholdQuantity).Should().BeTrue("Product should have sufficient stock");
        }

        [TestMethod]
        public void Product_Should_Handle_Zero_Quantity()
        {
            // Arrange & Act
            var product = new Product
            {
                Id = Guid.NewGuid(),
                PricePerPiece = 100m,
                Quantity = 0,
                AlertThresholdQuantity = 5,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid() }
            };

            // Assert
            product.Quantity.Should().Be(0);
            (product.Quantity <= product.AlertThresholdQuantity).Should().BeTrue("Zero quantity should trigger low stock alert");
        }

        [TestMethod]
        public void Product_Should_Set_Category_Relationship()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new Category 
            { 
                Id = categoryId,
                IsDeleted = false
            };

            // Act
            var product = new Product
            {
                Id = Guid.NewGuid(),
                PricePerPiece = 100m,
                Quantity = 10,
                AlertThresholdQuantity = 5,
                CategoryId = categoryId,
                Category = category
            };

            // Assert
            product.CategoryId.Should().Be(categoryId);
            product.Category.Should().NotBeNull();
            product.Category.Id.Should().Be(categoryId);
            product.Category.Should().BeSameAs(category);
        }

        [TestMethod]
        public void Product_Should_Allow_Null_Category_Navigation_Property()
        {
            // Arrange & Act
            var product = new Product
            {
                Id = Guid.NewGuid(),
                PricePerPiece = 100m,
                Quantity = 10,
                AlertThresholdQuantity = 5,
                CategoryId = Guid.NewGuid(),
                Category = null // Navigation property can be null
            };

            // Assert
            product.Category.Should().BeNull();
            product.CategoryId.Should().NotBeEmpty(); // But CategoryId should still be set
        }

        [TestMethod]
        public void Product_Should_Handle_Empty_Translations_Collection()
        {
            // Arrange & Act
            var product = new Product
            {
                Id = Guid.NewGuid(),
                PricePerPiece = 100m,
                Quantity = 10,
                AlertThresholdQuantity = 5,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid() },
                Translations = new List<ProductTranslation>()
            };

            // Assert
            product.Translations.Should().NotBeNull();
            product.Translations.Should().BeEmpty();
        }

        [TestMethod]
        public void Product_Should_Track_Warning_Date_History()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                PricePerPiece = 100m,
                Quantity = 2,
                AlertThresholdQuantity = 5,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid() }
            };

            var firstWarningDate = DateTime.UtcNow.AddHours(-2);
            var secondWarningDate = DateTime.UtcNow;

            // Act
            product.LastLowStockWarningDate = firstWarningDate;
            var firstDate = product.LastLowStockWarningDate;

            product.LastLowStockWarningDate = secondWarningDate;
            var secondDate = product.LastLowStockWarningDate;

            // Assert
            firstDate.Should().Be(firstWarningDate);
            secondDate.Should().Be(secondWarningDate);
            secondDate.Should().BeAfter(firstDate!.Value);
        }

        [TestMethod]
        public void Product_Should_Handle_Large_Numbers()
        {
            // Arrange
            var largePrice = decimal.MaxValue;
            var largeQuantity = double.MaxValue;
            var largeThreshold = 999999999.99;

            // Act
            var product = new Product
            {
                Id = Guid.NewGuid(),
                PricePerPiece = largePrice,
                Quantity = largeQuantity,
                AlertThresholdQuantity = largeThreshold,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid() }
            };

            // Assert
            product.PricePerPiece.Should().Be(largePrice);
            product.Quantity.Should().Be(largeQuantity);
            product.AlertThresholdQuantity.Should().Be(largeThreshold);
        }

        [TestMethod]
        public void Product_Should_Inherit_From_Entity_And_Implement_Interfaces()
        {
            // Arrange & Act
            var product = new Product
            {
                Id = Guid.NewGuid(),
                PricePerPiece = 100m,
                Quantity = 10,
                AlertThresholdQuantity = 5,
                CategoryId = Guid.NewGuid(),
                Category = new Category { Id = Guid.NewGuid() }
            };

            // Assert
            product.Should().BeAssignableTo<InnoSoft.InventorySystem.Core.Abstractions.Entity>();
            product.Should().BeAssignableTo<InnoSoft.InventorySystem.Core.Abstractions.IAuditable>();
            product.Should().BeAssignableTo<InnoSoft.InventorySystem.Core.Abstractions.ITranslationRootOf<ProductTranslation>>();
        }
    }
}