using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
using InnoSoft.InventorySystem.Application.Features.Categories.Handlers;
using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities.Categories;
using Moq;
using AutoMapper;
using FluentAssertions;

namespace InnoSoft.InventorySystem.Tests.Application.Features.Categories.Handlers
{
    [TestClass]
    public class CreateCategoryCommandHandlerTests
    {
        private Mock<IRepository<Category>> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private CreateCategoryCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<Category>>();
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockRepository.Setup(r => r.UnitOfWork).Returns(_mockUnitOfWork.Object);
            _handler = new CreateCategoryCommandHandler(_mockRepository.Object, _mockMapper.Object);
        }

        [TestMethod]
        public async Task Handle_ValidCommand_Should_Return_CategoryId()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var command = new CreateCategoryCommand
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

            var category = new Category
            {
                Id = categoryId,
                IsDeleted = false
            };

            _mockMapper.Setup(m => m.Map<Category>(command)).Returns(category);
            _mockRepository.Setup(r => r.Add(It.IsAny<Category>())).Returns(Task.FromResult(category));
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(categoryId);
            _mockRepository.Verify(r => r.Add(It.IsAny<Category>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Handle_Should_Map_Command_To_Category()
        {
            // Arrange
            var command = new CreateCategoryCommand
            {
                Translations = new List<CategoryTranslationDto>
                {
                    new CategoryTranslationDto
                    {
                        Name = "Laptops",
                        Description = "Portable computers",
                        Language = "en"
                    }
                }
            };

            var category = new Category
            {
                Id = Guid.NewGuid(),
                IsDeleted = false
            };

            _mockMapper.Setup(m => m.Map<Category>(command)).Returns(category);
            _mockRepository.Setup(r => r.Add(It.IsAny<Category>())).Returns(Task.FromResult(category));
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockMapper.Verify(m => m.Map<Category>(command), Times.Once);
        }

        [TestMethod]
        public async Task Handle_Should_Call_Repository_Add()
        {
            // Arrange
            var command = new CreateCategoryCommand
            {
                Translations = new List<CategoryTranslationDto>
                {
                    new CategoryTranslationDto { Name = "Test", Language = "en" }
                }
            };

            var category = new Category { Id = Guid.NewGuid() };

            _mockMapper.Setup(m => m.Map<Category>(command)).Returns(category);
            _mockRepository.Setup(r => r.Add(It.IsAny<Category>())).Returns(Task.FromResult(category));
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockRepository.Verify(r => r.Add(category), Times.Once);
        }
    }
}