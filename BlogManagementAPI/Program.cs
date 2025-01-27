using BlogManagementInfra.BbContext;
using BlogManagementInfra.DependencyInjection;
using BlogManagementService.DependencyInjection;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#region Repositories DI
builder.Services.AddRepositories();
#endregion

#region Services DI
builder.Services.AddServices();
#endregion

#region App Builder
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); 
}

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
