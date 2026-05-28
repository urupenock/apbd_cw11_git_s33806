using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Services; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddOpenApi();


builder.Services.AddDbContext<HospitalContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IHospitalService, HospitalService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
}

app.UseAuthorization();

app.MapControllers();

app.Run();