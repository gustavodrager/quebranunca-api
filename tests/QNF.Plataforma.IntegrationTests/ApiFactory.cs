using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QNF.Plataforma.API;
using QNF.Plataforma.Infrastructure.Data;

public class ApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<PlataformaDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<PlataformaDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb" + Guid.NewGuid());
            });
        });
    }
}
