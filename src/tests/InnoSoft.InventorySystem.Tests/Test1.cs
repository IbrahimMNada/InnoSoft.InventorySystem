using InnoSoft.InventorySystem.Core.Entities.Categories;
using InnoSoft.InventorySystem.Application.Features.Categories.Services;
using InnoSoft.InventorySystem.Application.Features.Categories.Handlers;
using InnoSoft.InventorySystem.Tests.Helpers;
using FluentAssertions;
using Moq;
using AutoMapper;

namespace InnoSoft.InventorySystem.Tests
{
    [TestClass]
    public sealed class CategoriesIntegrationTests
    {
        [TestMethod]
        public void Category_Entity_Should_Be_Instantiable()
        {
            // Arrange & Act
            var category = CategoryTestDataBuilder.CreateValidCategory();

            // Assert
            category.Should().NotBeNull();
            category.Id.Should().NotBeEmpty();
            category.Translations.Should().NotBeEmpty();
            category.Translations.Should().HaveCount(2);
        }

        [TestMethod]
        public void CategoryTranslation_Entity_Should_Be_Instantiable()
        {
            // Arrange & Act
            var translation = CategoryTestDataBuilder.CreateValidCategoryTranslation();

            // Assert
            translation.Should().NotBeNull();
            translation.Name.Should().NotBeNullOrEmpty();
            translation.LanguageId.Should().NotBeEmpty();
            translation.TranslationRootId.Should().NotBeEmpty();
        }

        [TestMethod]
        public void CategoryReadService_Should_Be_Instantiable()
        {
            // Arrange
            var mockRepository = new Mock<InnoSoft.InventorySystem.Core.Abstractions.IRepository<Category>>();
            var mockMapper = new Mock<IMapper>();

            // Act
            var service = new CategoryReadService(mockRepository.Object, mockMapper.Object);

            // Assert
            service.Should().NotBeNull();
            service.Should().BeAssignableTo<InnoSoft.InventorySystem.Application.Features.Categories.Services.ICategoryReadService>();
        }

        [TestMethod]
        public void CreateCategoryCommandHandler_Should_Be_Instantiable()
        {
            // Arrange
            var mockRepository = new Mock<InnoSoft.InventorySystem.Core.Abstractions.IRepository<Category>>();
            var mockMapper = new Mock<IMapper>();

            // Act
            var handler = new CreateCategoryCommandHandler(mockRepository.Object, mockMapper.Object);

            // Assert
            handler.Should().NotBeNull();
        }

        [TestMethod]
        public void UpdateCategoryCommandHandler_Should_Be_Instantiable()
        {
            // Arrange
            var mockRepository = new Mock<InnoSoft.InventorySystem.Core.Abstractions.IRepository<Category>>();
            var mockMapper = new Mock<IMapper>();

            // Act
            var handler = new UpdateCategoryCommandHandler(mockRepository.Object, mockMapper.Object);

            // Assert
            handler.Should().NotBeNull();
        }

        [TestMethod]
        public void DeleteCategoryCommandHandler_Should_Be_Instantiable()
        {
            // Arrange
            var mockRepository = new Mock<InnoSoft.InventorySystem.Core.Abstractions.IRepository<Category>>();
            var mockMapper = new Mock<IMapper>();

            // Act
            var handler = new DeleteCategoryCommandHandler(mockRepository.Object, mockMapper.Object);

            // Assert
            handler.Should().NotBeNull();
        }

        [TestMethod]
        public void Category_Should_Support_Unicode_Characters()
        {
            // Arrange & Act
            var category = CategoryTestDataBuilder.CreateValidCategory();
            var arabicTranslation = category.Translations.FirstOrDefault(t => t.Name == "إلكترونيات");

            // Assert
            arabicTranslation.Should().NotBeNull();
            arabicTranslation!.Name.Should().Be("إلكترونيات");
            arabicTranslation.Description.Should().Contain("الأجهزة");
        }

        [TestMethod]
        public void CategoryTestDataBuilder_Should_Create_Valid_Commands()
        {
            // Arrange & Act
            var createCommand = CategoryTestDataBuilder.CreateValidCreateCategoryCommand();
            var updateCommand = CategoryTestDataBuilder.CreateValidUpdateCategoryCommand();

            // Assert
            createCommand.Should().NotBeNull();
            createCommand.Translations.Should().HaveCount(2);
            
            updateCommand.Should().NotBeNull();
            updateCommand.Id.Should().NotBeEmpty();
            updateCommand.Translations.Should().HaveCount(2);
        }

        [TestMethod]
        public void CategoryTestDataBuilder_Should_Create_Multiple_Categories()
        {
            // Arrange & Act
            var categories = CategoryTestDataBuilder.CreateCategoryList(5);

            // Assert
            categories.Should().HaveCount(5);
            categories.Should().OnlyContain(c => c.Id != Guid.Empty);
            categories.Should().OnlyContain(c => c.Translations.Count == 2);
            categories.Should().OnlyContain(c => !c.IsDeleted);
        }
    }
}
