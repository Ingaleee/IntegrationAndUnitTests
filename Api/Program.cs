using FluentValidation;
using OzonGrpc.Infrastructure;
using OzonGrpc.Api.Grpc;
using OzonGrpc.Api.Interceptors;
using OzonGrpc.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddGrpc(c =>
{
    c.Interceptors.Add<LoggingInterceptor>();
    c.Interceptors.Add<ErrorHandlingInterceptor>();
    c.Interceptors.Add<ValidationInterceptor>();
}).AddJsonTranscoding();

builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGrpcService<ProductServiceGrpc>();

app.Run();
