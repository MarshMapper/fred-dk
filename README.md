# fred-dk
FRED Development Kit for .NET.  Provides easy access to APIs on the St. Louis Federal Reserve Economic
Data (FRED) web site:

[https://fred.stlouisfed.org/](https://fred.stlouisfed.org/)   
[https://fred.stlouisfed.org/docs/api/fred/](https://fred.stlouisfed.org/docs/api/fred/)

The current version provides support for all endpoints on the Categories, Series and Releases APIs
except release/tables endpoint that is a special case.  

## Using
The FredDevelopmentKit library can be added to any .NET 8.0+ application.  To use from a Console application,
you must first create a HostBuilder:  

        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);

            // call extension method to add Fred services
            builder.AddFredServices();

            // services can then be requested and used 
            var categoryService = services.GetService<IFredCategoryService>();

            // call methods on the service ...
        }

From an ASP.NET application, the following method should be called with a WebApplicationBuilder

            builder.Services.AddFredWebServices(builder.Configuration);

Sample console (FredDKSample) and ASP.NET (FredDKAspNetSample) applications are also included.

This software is not associated with the St. Louis Fed or FRED web site.  You must obtain your own
API key to use the API, and it is your responsibility to follow all the terms of use:

[https://fred.stlouisfed.org/docs/api/api_key.html](https://fred.stlouisfed.org/docs/api/api_key.html)   
[https://fred.stlouisfed.org/docs/api/terms_of_use.html](https://fred.stlouisfed.org/docs/api/terms_of_use.html)

The default value for the API key in appsettings.json is a placeholder.  You must provide your own key and it
is recommended that you use a secure method to store it, such as Azure Key Vault, or a user secret.  In User Secrets,
the key is "FredClient:ApiKey".