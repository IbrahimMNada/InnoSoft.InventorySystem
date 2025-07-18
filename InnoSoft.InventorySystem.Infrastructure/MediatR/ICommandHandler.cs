using InnoSoft.InventorySystem.Infrastructure.Commands;
using MediatR;

namespace InnoSoft.InventorySystem.Infrastructure
{
    public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : Command<TResult>
    {
    }
}
