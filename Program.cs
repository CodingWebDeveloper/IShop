using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using ReactQuery_Server;
using ReactQuery_Server.Data;
using ReactQuery_Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:3000");
        });
});
// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddDbContext<ShopDbContext>(
//    options => options.UseNpgsql().UseSqlServer());
builder
    .Services
    .AddDbContext<ShopDbContext>
    (options => options
    .UseNpgsql("name=ConnectionStrings:DefaultConnection",
    x => x.MigrationsHistoryTable("__efmigrationshistory", "public"))
    .UseSnakeCaseNamingConvention()
    .ReplaceService<IHistoryRepository, LoweredCaseMigrationHistoryRepository>());

builder.Services.AddScoped<IProductsService, ProductsServices>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
var app = builder.Build();

app.UseCors("CORSPolicy");
using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ShopDbContext>();
    dbContext.Database.Migrate();
}


    // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
