using AutoMapper;
using CruiseControl.Application.Asaas.RabbitMQServiceAsaas;
using CruiseControl.Application.Commands.AddCommand;
using CruiseControl.Application.Commands.DeleteCommand;
using CruiseControl.Application.Commands.ReserveCommand;
using CruiseControl.Application.Consumers;
using CruiseControl.Application.DTO_s;
using CruiseControl.Application.Email;
using CruiseControl.Application.GoogleCalendar;
using CruiseControl.Application.GoogleCalendar.RabbitMQServiceCalendar;
using CruiseControl.Application.Options;
using CruiseControl.Application.Queries.GetAllCarsQuery;
using CruiseControl.Application.Queries.GetAllCustomersQuery;
using CruiseControl.Application.Queries.GetAvailableCarsByCategoryQuery;
using CruiseControl.Application.Queries.GetAvailableCarsQuery;
using CruiseControl.Application.Queries.GetCarByIdQuery;
using CruiseControl.Application.Queries.GetReservationByCustomerIdQuery;
using CruiseControl.Application.Queries.GetReservationsByCarIdQuery;
using CruiseControl.Application.Validations.AddValidator;
using CruiseControl.Core.Entities;
using CruiseControl.Core.Repositories;
using CruiseControl.Core.Services;
using CruiseControl.Infrastructure.MessageService;
using CruiseControl.Infrastructure.Payments;
using CruiseControl.Infrastructure.Persistence;
using CruiseControl.Infrastructure.Persistence.Repositories;
using FluentAssertions.Common;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string chaveSecreta = "eeccd01b-e77c-44f9-bb9f-a519b3105dce";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CruiseControl - API", Version = "v1" });

    var securitySchems = new OpenApiSecurityScheme
    {
        Name = "JWT Autenticação",
        Description = "Entre com o JWT Bearer Token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    x.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchems);
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securitySchems, new string[] { } }
            });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "CruiseControl",
        ValidAudience = "CruiseControl",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta))
    };
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.IgnoreNullValues = true;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddScoped<ICarCategoryRepository, CarCategoryRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>(); 
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IMessageService, MessageService>();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(AddCarCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(AddCustomerCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(AddNotificationCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(AddPaymentCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(AddReservationCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(AddScheduleCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(DeleteCarCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(DeleteCarCommand)));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(ReserveCarByCategoryCommand)));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddTransient<IRequestHandler<GetCarByIdQuery, CarDTO>, GetCarByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAvailableCarsQuery, IEnumerable<CarDTO>>, GetAvailableCarsQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetReservationsByCustomerIdQuery, IEnumerable<ReservationDTO>>, GetReservationsByCustomerIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetReservationsByCarIdQuery, IEnumerable<ReservationDTO>>, GetReservationsByCarIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllCarsQuery, IEnumerable<CarDTO>>, GetAllCarsQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDTO>>, GetAllCustomersQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAvailableCarsByCategoryQuery, IEnumerable<CarDTO>>, GetAvailableCarsByCategoryQueryHandler>();


builder.Services.AddHttpClient();

builder.Services.Configure<WebMailOptions>(builder.Configuration.GetSection("WebMailOptions"));
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMqOptions"));
builder.Services.Configure<RabbitMQService>(builder.Configuration.GetSection("RabbitMQConfig"));
builder.Services.Configure<RabbitMQAsaas>(builder.Configuration.GetSection("AsaasConfig"));

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddTransient<IValidator<AddCarCommand>, AddCarCommandValidator>();
builder.Services.AddTransient<IValidator<AddCustomerCommand>, AddCustomerCommandValidator>();

builder.Services.AddHostedService<PaymentApprovedConsumer>();
builder.Services.AddHostedService<SendEmailConsumerService>();


var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Car, CarDTO>();
});

builder.Services.AddSingleton<IMapper>(new Mapper(mapperConfig));

builder.Services.AddLogging();

builder.Services.AddSingleton<GoogleCalendarService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CruiseControl.API v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
