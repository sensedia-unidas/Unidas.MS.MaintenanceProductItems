namespace OuroVerde.Maintenance.Domain.Core.Domain
{
    public class DomainException : Exception
    {
        public string ErrorCode { get; set; }

        public DomainException()
        {

        }

        public DomainException(string message)
        : base(message)
        {

        }

        public DomainException(string message, Exception innerException)
        : base(message, innerException)
        {

        }
    }
}