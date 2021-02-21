using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PaymentEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PaymentEngine.Infrastructure.DataAccess
{
    public class PaymentEngineContext : DbContext
    {
        public PaymentEngineContext(DbContextOptions<PaymentEngineContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Payments> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);     
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
