using ApiMetodosPagamento.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMetodosPagamento.Data
{
    public class ApiMetodosPagamentoContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public ApiMetodosPagamentoContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
