using MediatR;
using Wallet.Registration.Api.Controller.Mutation;
using Wallet.Registration.Api.Controller.Query;
using Wallet.Registration.CrossCutting.Configuration;
using Wallet.Registration.Domain.Command.v1.SignUp;
using Wallet.Registration.Domain.Interfaces.v1.MongoDb;
using Wallet.Registration.Infrastructure.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Origins",
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:50001").AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SignUpCommand).Assembly));

builder.Services.AddGraphQLServer()
        .AddQueryType<RegistrationQuery>()
        .AddMutationType<RegistrationMutation>();

builder.Services.AddGraphQL();
var appSettings = builder.Configuration.Get<AppSettings>();
builder.Services.AddSingleton(appSettings);
builder.Services.AddSingleton<IMongoDbRepository>(provider =>
    new MongoDbRepository(
        "SignIn",
        "UserRegistry",
        "User",
        provider.GetRequiredService<AppSettings>()
    ));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("Origins");

app.MapGraphQL();

app.Run();