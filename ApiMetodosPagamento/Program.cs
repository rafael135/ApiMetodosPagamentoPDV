
using ApiMetodosPagamento.Data;
using ApiMetodosPagamento.Data.DAL;
using ApiMetodosPagamento.Services;
using ApiMetodosPagamento.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ApiMetodosPagamento
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration["ConnectionStrings:PostgreConnection"];

            // Add services to the container.

            builder.Services.AddDbContext<ApiMetodosPagamentoContext>(opts =>
            {
                opts.UseNpgsql(connectionString);
                opts.UseLazyLoadingProxies();
            });

            #region DALs
            builder.Services.AddTransient<PaymentDAL>();

            #endregion

            #region Services
            builder.Services.AddTransient<PaymentService>();
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            builder.Services.AddEndpointsApiExplorer();
            // Adiciono o Swagger para facilitar a documentação da API
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetodosPagamentoAPI", Version = "v1" }); // Defino um documento para documentação de API a ser criado
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // Monto o nome do arquivo
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); // Monto o diretório até o arquivo
                c.IncludeXmlComments(xmlPath); // Inclui comentários XML para documentação da API
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
