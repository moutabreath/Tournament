using System.Configuration;
using Tournament.Common.Objects;
using Tournament.Interfaces;
using Tournament.Logic;
using Tournamnent.Repository.Mongo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITournamentBusinessLogic, TournamentBusinessLogic>();
builder.Services.AddScoped<ITournamentRepository, MongoRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<TournamentConfig>(builder.Configuration.GetSection(TournamentConfig.Position));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
