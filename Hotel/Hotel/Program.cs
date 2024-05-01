using Hotel.Repository.BookedReservation;
using Hotel.Repository.CanceledReservation;
using Hotel.Service.CommandHandler;
using Hotel.Service.MessageSender;

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
