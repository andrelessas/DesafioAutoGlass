using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace DesafioAutoGlass.API.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services
                .AddSwaggerGen(x =>
                {
                    x.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Desafio Auto Glass",
                        Version = "1.0",
                        Description = @"<br><b>Api de gerenciamento de produtos.</b>
                            <br><br><b>Informações sobre a criação da API:</b>
                            <p>Nas orientações do desafio, foi informado que a entidade Produto possui os campos: 
                            <br>•	Código do produto (sequencial e não nulo)
                            <br>•	Descrição do produto (não nulo)
                            <br>•	Situação do produto (Ativo ou Inativo)
                            <br>•	Data de fabricação
                            <br>•	Data de validade
                            <br>•	Código do fornecedor
                            <br>•	Descrição do fornecedor
                            <br>•	CNPJ do fornecedor</p>
                            <p>Na modelagem do banco, eu criei a tabela Produto e Fornecedor, criei um relacionamento entre essas tabelas. 
                                Dessa forma, não será necessário informar dados repetidos no cadastro de produto, como CNPJ do fornecedor e o nome do fornecedor.
                            </p>",
                        Contact = new OpenApiContact()
                        {
                            Name = "André Lessas",
                            Email = "andrelessasp@gmail.com",
                            Url = new Uri("https://github.com/andrelessas")
                        }
                    });

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory,xmlFile);
                    x.IncludeXmlComments(xmlPath);
                });
        }
    }
}