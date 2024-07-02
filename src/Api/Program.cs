using MediatR;
using Wallet.Registration.Api.Controller.Mutation;
using Wallet.Registration.Api.Controller.Query;
using Wallet.Registration.Domain.Command.v1.SignUp;

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
        .AddQueryType<RegisrationQuery>()
        .AddMutationType<RegistrationMutation>();

builder.Services.AddGraphQL();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("Origins");

app.MapGraphQL();

app.Run();