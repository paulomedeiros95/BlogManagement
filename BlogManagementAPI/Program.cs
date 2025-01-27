using BlogManagementDomain.MappingProfiles;
using BlogManagementInfra.Data;
using BlogManagementInfra.DependencyInjection;
using BlogManagementService.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

#region Configure DataBase
builder.Services.ConfigureDbContext(builder.Configuration);
#endregion

#region Repositories DI
builder.Services.AddRepositories();
#endregion

#region Services DI
builder.Services.AddServices();
#endregion

#region App Builder
var app = builder.Build();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                exception = e.Value.Exception?.Message,
                duration = e.Value.Duration.ToString()
            })
        };

        await context.Response.WriteAsJsonAsync(response);
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
#endregion
