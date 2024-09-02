using Microsoft.Extensions.Options;
using MongoDB.Driver;
using student_api_mongodb.models;
using student_api_mongodb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<IStudentStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(StudentStroreDatabaseSettings)));

builder.Services.AddSingleton<IStudentStoreDatabaseSettings>(sp => 
    sp.GetRequiredService<IOptions<StudentStroreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("StudentStoreDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddControllers();
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
