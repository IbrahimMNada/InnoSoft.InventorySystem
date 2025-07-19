using AutoMapper;
using FluentAssertions;
using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
using InnoSoft.InventorySystem.Application.Features.Categories.Handlers;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities.Categories;
using InnoSoft.InventorySystem.Core.Exceptions;
using Moq;

namespace InnoSoft.InventorySystem.Tests.Application.Features.Categories.Handlers
{
    [TestClass]
    public class DeleteCategoryCommandHandlerTests
    {
        private Mock<IRepository<Category>> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private DeleteCategoryCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<Category>>();
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            _mockRepository.Setup(r => r.UnitOfWork).Returns(_mockUnitOfWork.Object);
            _handler = new DeleteCategoryCommandHandler(_mockRepository.Object, _mockMapper.Object);
        }

        [TestMethod]
        public async Task Handle_ValidId_Should_Return_True()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var command = new DeleteCategoryCommand { Id = categoryId };
            
            var existingCategory = new Category
            {
                Id = categoryId,
                IsDeleted = false
            };

            _mockRepository.Setup(r => r.GetById(categoryId)).ReturnsAsync(existingCategory);
            _mockRepository.Setup(r => r.Delete(categoryId)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _mockRepository.Verify(r => r.GetById(categoryId), Times.Once);
            _mockRepository.Verify(r => r.Delete(categoryId), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Handle_NonExistentCategory_Should_Throw_EntityNotFoundException()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var command = new DeleteCategoryCommand { Id = categoryId };

            _mockRepository.Setup(r => r.GetById(categoryId)).ReturnsAsync((Category)null);

            // Act & Assert
            var exception = await Assert.ThrowsExceptionAsync<EntityNotFoundException>(
                () => _handler.Handle(command, CancellationToken.None));
            
            exception.Should().NotBeNull();
            _mockRepository.Verify(r => r.GetById(categoryId), Times.Once);
            _mockRepository.Verify(r => r.Delete(It.IsAny<Guid>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        public async Task Handle_Should_Call_Repository_Delete()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var command = new DeleteCategoryCommand { Id = categoryId };
            var existingCategory = new Category { Id = categoryId };

            _mockRepository.Setup(r => r.GetById(categoryId)).ReturnsAsync(existingCategory);
            _mockRepository.Setup(r => r.Delete(categoryId)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockRepository.Verify(r => r.Delete(categoryId), Times.Once);
        }
    }
}