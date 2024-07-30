using FluentValidation;
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
builder.Services.AddValidatorsFromAssemblyContaining<SignUpCommandValidator>();
builder.Services.AddSingleton<IUserRegistryRepository>(provider =>
    new UserRegistryRepository(
        "SignIn",
        "WalletRegistration",
        "UserRegistry",
        provider.GetRequiredService<AppSettings>()
    ));
builder.Services.AddSingleton<IUserSaltRepository>(provider =>
    new UserSaltRepository(
        "SignIn",
        "WalletRegistration",
        "UserSalt",
        provider.GetRequiredService<AppSettings>()
    ));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("Origins");

app.MapGraphQL();

app.Run();