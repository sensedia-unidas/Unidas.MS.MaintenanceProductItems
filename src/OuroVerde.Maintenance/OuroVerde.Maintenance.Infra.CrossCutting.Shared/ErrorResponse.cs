namespace Unidas.MS.Maintenance.Infra.CrossCutting.Shared
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Code = Guid.NewGuid().ToString();
            Message = "Inspected error.";
            Date = DateTime.Now;
        }

        public string Code { get; private set; }
        public DateTime Date { get; private set; }
        public string Message { get; private set; }
        public string Detail { get; private set; }
    }
}