using SchedulingService.API.Services;
using SchedulingService.API.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

MongoDB.Bson.Serialization.BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IScheduleService,ScheduleService>();

builder.Services.Configure<ScheduleDatabaseSettings>(
    builder.Configuration.GetSection("ScheduleDatabase"));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
