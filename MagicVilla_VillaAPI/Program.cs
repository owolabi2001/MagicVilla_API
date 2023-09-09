

using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.loggingh;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
//    .WriteTo.File("log/villalogs.txt",rollingInterval:RollingInterval.Day).CreateLogger();
////builder.Logging.AddSerilog(logger);
//builder.Host.UseSerilog();
builder.Services.AddControllers(//options =>
//{
   // options.ReturnHttpNotAcceptable=true;}
   ).AddXmlDataContractSerializerFormatters(); // this help ensure that an acceptable response type can also be xml and not just json
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ILogging, Logging>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});


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
