using ChartsServer.Hubs;
using ChartsServer.model;
using ChartsServer.Subscription;
using ChartsServer.Subscription.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(
  policy => policy.AllowCredentials().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(x => true)));
builder.Services.AddSignalR();



builder.Services.AddSingleton<DatabaseSubscription<Sales>>();
builder.Services.AddSingleton<DatabaseSubscription<Employee>>();
var app = builder.Build();

app.UseDatabaseSubscription<DatabaseSubscription<Sales>>("Sales");
app.UseDatabaseSubscription<DatabaseSubscription<Employee>>("Employee");
app.UseCors();


app.MapHub<SalesHub>("/saleshub");


app.Run();
