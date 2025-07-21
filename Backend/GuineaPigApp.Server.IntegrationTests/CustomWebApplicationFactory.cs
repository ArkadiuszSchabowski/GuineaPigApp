using Microsoft.AspNetCore.Mvc.Testing;

namespace GuineaPigApp.Server.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                var integrationConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Test.json")
                    .Build();

                configBuilder.AddConfiguration(integrationConfig);
            });
        }
    }
}
