namespace FredDeveloperKit.Configuration
{
    public class FredClientOptions
    {
        // the name of the property used to store the below options in the appsettings.json file
        public const string FredClient = "FredClient";
        public string ApiKey { get; set; } = String.Empty;
    }
}
