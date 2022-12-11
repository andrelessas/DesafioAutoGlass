using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.APPLICATION.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace DesafioAutoGlass.API.Configuration
{
    public static class ServicesConfiguration
    {
        public static void AddServicesConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()
                .Where(p => !p.IsDynamic));

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<InserirProdutoCommandValidation>();    
        }
    }
}