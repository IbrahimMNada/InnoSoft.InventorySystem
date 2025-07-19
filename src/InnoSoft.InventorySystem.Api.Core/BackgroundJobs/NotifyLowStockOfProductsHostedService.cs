using AutoMapper;
using Azure.Core;
using InnoSoft.InventorySystem.Application.Features.Products.Dtos;
using InnoSoft.InventorySystem.Application.SignalRHubs;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InnoSoft.InventorySystem.Api.Core.BackgroundJobs
{
    public class NotifyLowStockOfProductsHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NotifyLowStockOfProductsHostedService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5);

        public NotifyLowStockOfProductsHostedService(
            IServiceProvider serviceProvider,
            ILogger<NotifyLowStockOfProductsHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("NotifyLowStockOfProductsHostedService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CheckAndNotifyLowStockProducts(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while checking low stock products.");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("NotifyLowStockOfProductsHostedService stopped.");
        }

        private async Task CheckAndNotifyLowStockProducts(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IRepository<Product>>();
            var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<ProductsHub>>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

            var thirtyMinutesAgo = DateTime.UtcNow.AddMinutes(-30);

            var lowStockProducts = await repository.Queryable()
                .Include(p => p.Translations)
                .Where(p =>
                    p.Quantity <= p.AlertThresholdQuantity &&
                    (p.LastLowStockWarningDate == null || p.LastLowStockWarningDate <= thirtyMinutesAgo) &&
                    !p.IsDeleted)
                .ToListAsync(cancellationToken);

            if (lowStockProducts.Any())
            {
                _logger.LogInformation($"Found {lowStockProducts.Count} products with low stock.");

                foreach (var product in lowStockProducts)
                {
                    product.LastLowStockWarningDate = DateTime.UtcNow;
                    await hubContext.Clients.All.SendAsync("LowStockAlert", new { request = mapper.Map<ProductNotificationDto>(product) }, cancellationToken);
                }


                await repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            else
            {
                _logger.LogDebug("No low stock products found.");
            }
        }
    }
}
