namespace Lista3_Crud.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException() : base()
        {
        }

        public DomainException(string? message) : base(message)
        {
        }

        public DomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public static void ThrowWhen(bool error, string? message)
        {
            if (error)
                throw new DomainException(message);
        }
    }
}