using Hotel.Command.CommandHandler;
using Hotel.Command.Consumer;
using Hotel.Command.Repository.BookedReservation;
using Hotel.Command.Repository.CanceledReservation;
using Hotel.Query.Consumer;
using Hotel.Query.Handler;
using Hotel.Query.Projector;
using Hotel.Query.Repository.HotelInfoRepository;
using Hotel.Query.Repository.ReservationRepository;
using Hotel.Service.MessageSender;
using MassTransit;
using MassTransit.AspNetCoreIntegration;

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
builder.Services.AddScoped<IHotelQueryHandler, HotelQueryHandler>();

builder.Services.AddMassTransit(x =>
    x.UsingRabbitMq((context, cfg) => {
        cfg.Host("rabbitmq", 5672, "guest", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("hotel-command-queue", e =>
        {
            e.ConfigureConsumer<HotelCommandConsumer>(context);
        });
        cfg.ReceiveEndpoint("hotel-event-queue", e =>
        {
            e.ConfigureConsumer<HotelEventConsumer>(context);
        });
        cfg.ConfigureEndpoints(context);
    }));


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
