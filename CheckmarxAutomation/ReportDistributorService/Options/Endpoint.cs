namespace ReportDistributorService.Options
{
    public struct Endpoint
    {
        public Endpoint(string url, string username, string password, string certificateName, string requestingSystem)
        {
            this.Url = url;
            this.Username = username;
            this.Password = password;
            this.CertificateName = certificateName;
            this.RequestingSystem = requestingSystem;
        }

        public string Url { get; }

        public string Username { get; }

        public string Password { get; }

        public string CertificateName { get; }

        public string RequestingSystem { get; }
    }
}
