using SOLID.Principles.Demo.Services;
using SOLID.S.SingleResponsibility.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<ILogger<OrderService>, Logger<OrderService>>();
builder.Services.AddScoped<IPaymentProcess, CreditCardProcessor>();
builder.Services.AddScoped<IPaymentProcess, BankTransferProcessor>();
builder.Services.AddScoped<ILiskovSubstitutionService, LiskovSubstitutionService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
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
