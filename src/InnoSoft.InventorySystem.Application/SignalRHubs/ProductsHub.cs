using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.SignalRHubs
{
    [Authorize]
    public class ProductsHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            var name = Context.User?.Identity?.Name;
            Console.WriteLine($"Connected: {name} ({userId})");
            return base.OnConnectedAsync();
        }
    }
}