using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TrendSense.WebApi
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                var apiVersion = description.ApiVersion.ToString();
                options.SwaggerDoc(description.GroupName,
                    new OpenApiInfo
                    {
                        Version = apiVersion,
                        Title = $"TrendSense API{apiVersion}",
                        Description = "API for monitoring exchange",
                        Contact = new OpenApiContact
                        {
                            Name = "Reyme",
                            Email = string.Empty
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Product License"
                        }
                    });
            }
        }
    }
}
