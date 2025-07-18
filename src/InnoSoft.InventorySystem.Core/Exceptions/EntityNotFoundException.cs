using System;

namespace InnoSoft.InventorySystem.Core.Exceptions
{
    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(Type entityType, Guid entityId) : base($"entityNotFound")
        {

        }
    }
}
