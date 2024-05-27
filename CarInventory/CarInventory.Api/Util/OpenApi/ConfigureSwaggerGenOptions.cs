using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace CarInventory.Api.Util.OpenApi
{
    public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (ApiVersionDescription apiVersionDescription in _provider.ApiVersionDescriptions)
            {
                var openApiInfo = new OpenApiInfo
                {
                    Title = $"CarInventory.Api v{apiVersionDescription.ApiVersion}",
                    Version = apiVersionDescription.ApiVersion.ToString(),
                    Description = "API for managing car inventory, including creating, updating, retrieving, and deleting.",
                    Contact = new OpenApiContact
                    {
                        Name = "API Support",
                        Email = "support@example.com",
                        Url = new Uri("https://example.com/support")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                };
                options.SwaggerDoc(apiVersionDescription.GroupName, openApiInfo);
            }
            // Optional: Include XML comments (if configured in the csproj file)
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        }
    }
}
