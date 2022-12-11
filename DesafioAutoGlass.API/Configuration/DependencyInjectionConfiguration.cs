using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.API.Middleware;
using DesafioAutoGlass.APPLICATION.Command.InserirProdutoCommand;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.INFRASTRUCTURE.Context;
using DesafioAutoGlass.INFRASTRUCTURE.Persistense.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DesafioAutoGlass.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DesafioAutoGlassContext>(options => options.UseSqlServer(configuration.GetConnectionString("conexao")));
            services.AddMediatR(typeof(InserirProdutoCommand));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<GlobalExceptionHandlerMiddleware>();
        }
    }
}