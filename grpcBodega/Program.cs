using grpcBodega;
using grpcBodega.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Register the DbContext with the DI container
builder.Services.AddDbContext<ContextoLibreria>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<BookServiceImpl>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
