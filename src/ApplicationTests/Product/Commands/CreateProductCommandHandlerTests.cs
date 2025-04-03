/*
using Application.Features.Product.Commands.CreateProduct;
using AutoMapper;
using Core.Entities.Write;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace ApplicationTests.Product.Commands
{
    public class CreateProductCommandHandlerTests : IDisposable
    {
        private readonly Mock<IWriteDbUnitOfWork> _mockWriteUow;
        private readonly Mock<IReadDbUnitOfWork> _mockReadUow;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<CreateProductCommandHandler>> _mockLogger;
        private readonly WriteDbContext _writeDbContext;
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            // Write DB için
            var writeOptions = new DbContextOptionsBuilder<WriteDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_WriteDB_" + Guid.NewGuid())
                .Options;
            _writeDbContext = new WriteDbContext(writeOptions);

            // Read DB için
            var readOptions = new DbContextOptionsBuilder<ReadDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_ReadDB_" + Guid.NewGuid())
                .Options;
            _readDbContext = new ReadDbContext(readOptions);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Application.MapperProfiles.ProductProfile>();
            });
            _mapper = new Mapper(configuration);

            // Mock'ları initialize et
            _mockWriteUow = new Mock<IWriteDbUnitOfWork>();

            _mockReadUow = new Mock<IReadDbUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<CreateProductCommandHandler>>();

            var mockCategoryRepo = new Mock<IWriteRepository<Category>>();

            mockCategoryRepo.Setup(r => r.GetByConditionAsync(
                It.IsAny<Expression<Func<Category, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Category { Id = Guid.NewGuid(), Name = "Test" });

            // Tüm repository'ler için mock setup

            _mockWriteUow.Setup(u => u.WriteRepository<Category>())
                 .Returns(mockCategoryRepo.Object);

            _mockWriteUow.Setup(u => u.WriteRepository<Category>())
                .Returns(new WriteRepository<Category>(_writeDbContext));

            _mockWriteUow.Setup(u => u.WriteRepository<Shop>())
                .Returns(new WriteRepository<Shop>(_writeDbContext));

            _mockWriteUow.Setup(u => u.WriteRepository<Core.Entities.Write.Product>())
                .Returns(new WriteRepository<Core.Entities.Write.Product>(_writeDbContext));

            // ReadRepository için fake implementasyon
            var fakeReadCategoryRepo = new Mock<ReadRepository<Category>>(_readDbContext);
            _mockReadUow.Setup(u => u.ReadRepository<Category>())
                .Returns(fakeReadCategoryRepo.Object);

            _handler = new CreateProductCommandHandler(
                _mockWriteUow.Object,
                _mockReadUow.Object,
                _mapper, 
                _mockLogger.Object,
                _mockMediator.Object);
        }


        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccess()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Surname = "User",
                Email = "test@example.com",
                Password = "Password123!",
                PhoneNumber = "5551234567",
                Country = "TR"
            };

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Electronics",
            };

            var shop = new Shop
            {
                Id = Guid.NewGuid(),
                Name = "Best Shop",
                Description = "Shop Desc",
                UserId = user.Id,
                User = user
            };

            // Arrange
            var categoryId = Guid.NewGuid();
            var shopId = Guid.NewGuid();

            // Mock Repository Setup
            var mockCategoryRepo = new Mock<IWriteRepository<Category>>();
            mockCategoryRepo.Setup(r => r.GetByConditionAsync(
                It.IsAny<Expression<Func<Category, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Category { Id = Guid.NewGuid(), Name = "Test" });

            _mockWriteUow.Setup(u => u.WriteRepository<Category>())
                        .Returns(mockCategoryRepo.Object);

            // Act
            var result = await _handler.Handle(
                new CreateProductCommand(categoryId, shopId, "Test", "Desc", 100, 10),
                CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ThrowsException()
        {
            // Arrange
            var invalidCommand = new CreateProductCommand(
                CategoryId: Guid.Empty,
                ShopId: Guid.Empty,
                Name: null!,
                Description: null!,
                Price: -1,
                Stock: -1
            );

            // Handler'ı oluştur
            var handler = new CreateProductCommandHandler(
                _mockWriteUow.Object,
                _mockReadUow.Object,
                _mockMapper.Object,
                _mockLogger.Object,
                _mockMediator.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() =>
                handler.Handle(invalidCommand, CancellationToken.None));
        }

        public void Dispose()
        {
            _writeDbContext?.Dispose();
            _readDbContext?.Dispose();
        }
    }
}

*/