using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ClientesApi.Infra.Data.Contexts
{
    public class SqlServerMigration : IDesignTimeDbContextFactory<SqlServerContext>
    {
        public SqlServerContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            var connectionstring = root.GetSection("ConnectionStrings")
                .GetSection("ClientesApi").Value;

            var builder = new DbContextOptionsBuilder<SqlServerContext>();
            builder.UseSqlServer(connectionstring);

            return new SqlServerContext(builder.Options);
        }
    }
}
