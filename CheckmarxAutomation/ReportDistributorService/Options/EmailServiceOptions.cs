namespace ReportDistributorService.Options
{
    public struct EmailServiceOptions
    {
        public EmailServiceOptions(Endpoint endpoint)
        {
            this.Endpoint = endpoint;
        }

        public Endpoint Endpoint { get; }
    }
}
