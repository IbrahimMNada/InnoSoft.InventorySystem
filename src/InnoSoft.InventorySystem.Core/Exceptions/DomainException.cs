using System;

namespace InnoSoft.InventorySystem.Core.Exceptions
{
    public class DomainException : Exception
    {
        public int ErrorCode { get; set; }
        public IEnumerable<string> ExtraInput { get; set; }
        public DomainException(int errorCode, string message, IEnumerable<string> extraInput = default) : base(message)
        {
            ErrorCode = errorCode;
            ExtraInput = extraInput ?? new List<string>();
        }

        public DomainException(string message) : base(message) { }
    }
}
