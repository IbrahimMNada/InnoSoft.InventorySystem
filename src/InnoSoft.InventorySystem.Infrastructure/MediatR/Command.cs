using MediatR;
using System;

namespace InnoSoft.InventorySystem.Infrastructure.Commands
{
    public abstract class Command<TResult> : IRequest<TResult>, ICommand
    {
        public Guid CommandId { get; } = Guid.NewGuid();
    }
}
