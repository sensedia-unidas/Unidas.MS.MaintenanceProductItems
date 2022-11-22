using Microsoft.EntityFrameworkCore;
using OuroVerde.Maintenance.Domain.Model;

namespace OuroVerde.Maintenance.Infra.Data.Context
{
    public class SensediaContext : DbContext
    {
        protected SensediaContext()
        {
        }

        public SensediaContext(DbContextOptions<SensediaContext> options)
            :base(options)
        {

        }

        public DbSet<ItensIntegrationLog> Itens_IntegrationLog { get; set; }
    }
}

