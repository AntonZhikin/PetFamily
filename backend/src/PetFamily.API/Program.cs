using PetFamily.Application.Voluunters;
using PetFamily.Application.Voluunters.CreateVoluunters;
using PetFamily.Infrastructure;
using PetFamily.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

builder.Services.AddScoped<ApplicationDbContext>();

builder.Services.AddScoped<CreateVolunteerHandler>();
//builder.Services.AddScoped<IVolunteerRepository, VolunteersRepository>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
