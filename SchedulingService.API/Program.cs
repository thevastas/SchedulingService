using SchedulingService.API.Services;
using SchedulingService.API.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

MongoDB.Bson.Serialization.BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IScheduleService,ScheduleService>();

builder.Services.Configure<ScheduleDatabaseSettings>(
    builder.Configuration.GetSection("ScheduleDatabase"));

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
