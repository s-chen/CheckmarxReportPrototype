namespace ReportDistributorService.Exception
{
    using System;

    public class EmailServiceException : Exception
    {
        public EmailServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public EmailServiceException(string message)
            : base(message)
        {
        }
    }
}
