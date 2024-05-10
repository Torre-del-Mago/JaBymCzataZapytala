using Hotel.Command.CommandHandler;
using Hotel.Command.Repository;
using Hotel.Command.Repository.BookedReservation;
using Hotel.Command.Repository.CanceledReservation;
using Hotel.Query.Projector;
using Hotel.Query.Repository.HotelInfoRepository;
using Hotel.Query.Repository.ReservationRepository;
using Hotel.Service.MessageSender;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookedReservationRepository, BookedReservationRepository>();
builder.Services.AddScoped<ICanceledReservationRepository, CanceledReservationRepository>();
builder.Services.AddScoped<IHotelCommandHandler, HotelCommandHandler>();
builder.Services.AddScoped<IMessageSender, MessageSender>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IHotelInfoRepository, HotelInfoRepository>();
builder.Services.AddScoped<IHotelEventProjector, HotelEventProjector>();


// Add DbContext to the container
builder.Services.AddDbContext<HotelContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbPath")));


//hotel-command-queue

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
