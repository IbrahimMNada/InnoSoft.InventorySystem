namespace InnoSoft.InventorySystem.Core.Exceptions
{
    public class InvalidInputException : DomainException
    {
        public InvalidInputException() : base("Invalid input") { }
        public InvalidInputException(string inputName) : base($"Invalid input: {inputName}")
        {
            InputName = inputName;
        }

        public string InputName { get; }
    }
}
