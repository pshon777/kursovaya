using Microsoft.EntityFrameworkCore;
using RecruitmentAgency;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ResruitmentAgancyDbContext>(options =>
{
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ResruitmentAgancyDB;Trusted_Connection=True;");
});

// Add services to the container.

builder.Services.AddControllers()//;
    .AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
