using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using CarInventory.Infrastructure;
using CarInventory.Application;
using CarInventory.Infrastructure.Middleware.ExceptionHandler;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using FluentValidation.AspNetCore;
using CarInventory.Api.Util.OpenApi;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using CarInventory.Api.Util.Enrichers;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add FluentValidation - This bypass the pipeline 
        //builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        // Add services to the container.
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
       

        // Serilog
        builder.Host.UseSerilog((context, loggerConfig) =>
            loggerConfig
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", context.HostingEnvironment.ApplicationName)
            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
            .Enrich.With<HttpContextEnricher>());

        // Add HttpClient service
        builder.Services.AddHttpClient();


        //Good practice to handle external services communication
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("all", builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
        });

        builder.Services.AddControllers();


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
 

        // Register the Swagger options configuration
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();

        //API Versioning
        builder.Services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddMvc()
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        // Enable Swagger for all environments for testing purposes
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();
            foreach (var desc in descriptions)
            {
                string url = $"/swagger/{desc.GroupName}/swagger.json";
                string name = desc.GroupName.ToUpperInvariant();

                options.SwaggerEndpoint(url, name);
            }
            options.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
        });

        //// Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI(options =>
        //    {
        //        IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();
        //        foreach (var desc in descriptions)
        //        {
        //            string url = $"/swagger/{desc.GroupName}/swagger.json";
        //            string name = desc.GroupName.ToUpperInvariant();

        //            options.SwaggerEndpoint(url, name);
        //        }
        //    });
        //}

        //ExceptionHandler
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();

        // Apply CORS policy
        app.UseCors("all");

        app.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate = "ASP.NET HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
            options.GetLevel = (httpContext, elapsed, ex) => Serilog.Events.LogEventLevel.Information;
        });

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                var response = new
                {
                    status = report.Status.ToString(),
                    results = report.Entries.Select(e => new
                    {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        description = e.Value.Description
                    })
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        });

        app.Run();
    }
}