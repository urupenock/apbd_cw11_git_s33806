using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Services; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

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
    
  
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Hospital API V1");
    });
}

app.UseAuthorization();
app.MapControllers();
app.MapGet("/", async context => context.Response.Redirect("/swagger"));

app.Run();