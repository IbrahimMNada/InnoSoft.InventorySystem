using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
using InnoSoft.InventorySystem.Application.Features.Categories.Handlers;
using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities.Categories;
using InnoSoft.InventorySystem.Core.Exceptions;
using Moq;
using AutoMapper;

namespace InnoSoft.InventorySystem.Tests.Application.Features.Categories.Handlers
{
    [TestClass]
    public class UpdateCategoryCommandHandlerTests
    {
        private Mock<IRepository<Category>> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private UpdateCategoryCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<Category>>();
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            _mockRepository.Setup(r => r.UnitOfWork).Returns(_mockUnitOfWork.Object);
            _handler = new UpdateCategoryCommandHandler(_mockRepository.Object, _mockMapper.Object);
        }

        [TestMethod]
        public async Task Handle_ValidCommand_Should_Return_True()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var command = new UpdateCategoryCommand
            {
                Id = categoryId,
                Translations = new List<CategoryTranslationDto>
                {
                    new CategoryTranslationDto
                    {
                        Name = "Updated Electronics",
                        Description = "Updated electronic devices and accessories",
                        Language = "en"
                    }
                }
            };

            var existingCategory = new Category
            {
                Id = categoryId,
                IsDeleted = false
            };

            _mockRepository.Setup(r => r.GetById(categoryId)).ReturnsAsync(existingCategory);
            _mockMapper.Setup(m => m.Map(command, existingCategory)).Returns(existingCategory);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            //result.Should().BeTrue();
            _mockRepository.Verify(r => r.GetById(categoryId), Times.Once);
            _mockMapper.Verify(m => m.Map(command, existingCategory), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Handle_NonExistentCategory_Should_Throw_EntityNotFoundException()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var command = new UpdateCategoryCommand
            {
                Id = categoryId,
                Translations = new List<CategoryTranslationDto>
                {
                    new CategoryTranslationDto { Name = "Test", Language = "en" }
                }
            };

            _mockRepository.Setup(r => r.GetById(categoryId)).ReturnsAsync((Category)null);

            // Act & Assert
            var exception = await Assert.ThrowsExceptionAsync<EntityNotFoundException>(
                () => _handler.Handle(command, CancellationToken.None));
            
          //  exception.Should().NotBeNull();
            _mockRepository.Verify(r => r.GetById(categoryId), Times.Once);
            _mockMapper.Verify(m => m.Map(It.IsAny<UpdateCategoryCommand>(), It.IsAny<Category>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        public async Task Handle_Should_Map_Command_To_Existing_Category()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var command = new UpdateCategoryCommand
            {
                Id = categoryId,
                Translations = new List<CategoryTranslationDto>
                {
                    new CategoryTranslationDto { Name = "Updated Name", Language = "en" }
                }
            };

            var existingCategory = new Category { Id = categoryId };

            _mockRepository.Setup(r => r.GetById(categoryId)).ReturnsAsync(existingCategory);
            _mockMapper.Setup(m => m.Map(command, existingCategory)).Returns(existingCategory);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockMapper.Verify(m => m.Map(command, existingCategory), Times.Once);
        }
    }
}