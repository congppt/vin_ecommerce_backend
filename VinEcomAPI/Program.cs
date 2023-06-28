using VinEcomAPI;
using VinEcomService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectInfrastructure(builder.Configuration);
builder.Services.InjectWebAPIService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseRequestLocalization();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
