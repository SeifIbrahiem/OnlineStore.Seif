
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Seif.Api.Extensions;
using OnlineStore.Seif.Api.Middlewares;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstractions;
using Shared.ErrorsModels;

namespace OnlineStore.Seif.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.RegisterAllServices(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            await app.ConfigureMiddlewares();

            app.Run();
        }
    }
}
