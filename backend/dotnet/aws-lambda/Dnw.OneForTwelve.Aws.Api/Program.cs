var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.Map("/", (IHttpContextAccessor ctx) =>
{
    var architecture = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture;
    var dotnetVersion = Environment.Version.ToString();
    return $"Architecture: {architecture}, .NET Version: {dotnetVersion}, {ctx.HttpContext?.Request.Path}";
});

app.Run();