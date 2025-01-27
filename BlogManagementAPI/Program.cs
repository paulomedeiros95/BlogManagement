using BlogManagementInfra.BbContext;
using BlogManagementInfra.Data;
using BlogManagementInfra.DependencyInjection;
using BlogManagementService.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
