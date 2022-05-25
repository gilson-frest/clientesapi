using ClientesApi.Infra.Data.Contexts;
using ClientesApi.Infra.Data.Interfaces;
using ClientesApi.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClientesApi.Services
{
    /// <summary>
    /// Classe para configuração do EntityFramework
    /// </summary>
    public class EntityFrameworkConfiguration
    {
        /// <summary>
        /// Método para registrar a configuração
        /// </summary>
        public static void Register(WebApplicationBuilder builder)
        {
            //capturar a connectionstring do banco de dados
            var connectionString = builder.Configuration.GetConnectionString("ClientesApi");

            //injeção de dependencia para a classe SqlServerContext no EntityFramework
            builder.Services.AddDbContext<SqlServerContext>
                (map => map.UseSqlServer(connectionString));

            //mapear cada classe do repositorio
            builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
        }
    }
}
