using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

using var loggerFactory = LoggerFactory.Create(config =>
{
    config.SetMinimumLevel(LogLevel.Warning);
    config.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<ILogger>();

builder.Services.AddSingleton(typeof(ILogger), logger);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = (context) =>
    {
        logger.LogError(new EventId(), "One or more fields consist of invalid model data."
            + " If you are using the goal seek endpoint, ensure input, \n targetResult and"
            + " maximumIterations fields are strictly numeric");
        return new BadRequestObjectResult(context.ModelState);
    };
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");


// Only localhost requests are allowed
app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
    options.AllowAnyHeader();
});

app.Run();
