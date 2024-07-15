# fred-dk

FRED Development Kit for .NET.  Provides easy access to APIs on the St. Louis Federal Reserve Economic
Data (FRED) web site:

[https://fred.stlouisfed.org/](https://fred.stlouisfed.org/)
[https://fred.stlouisfed.org/docs/api/fred/](https://fred.stlouisfed.org/docs/api/fred/)

The current version provides partial support for the Categories API.  Services for the
Series and Releases APIs will be prioritzed based on their usefulness.

## Using

The FredDevelopmentKit library can be added to any .NET application.  A sample console application,
FredDKSample, is also provided.

This software is not associated with the St. Louis Fed or FRED web site.  You must obtain your own
API key to use the API, and it is your responsibility to follow all the terms of use:

[https://fred.stlouisfed.org/docs/api/api_key.html](https://fred.stlouisfed.org/docs/api/api_key.html)
[https://fred.stlouisfed.org/docs/api/terms_of_use.html](https://fred.stlouisfed.org/docs/api/terms_of_use.html)

The default value for the API key in appsettings.json is a placeholder.  You must provide your own key and it
is recommended that you use a secure method to store it, such as Azure Key Vault, or a user secret.  In User Secrets,
the key is "FredClient:ApiKey".