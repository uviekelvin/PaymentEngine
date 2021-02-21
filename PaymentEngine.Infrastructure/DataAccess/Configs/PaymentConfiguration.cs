using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.Infrastructure.DataAccess.Configs
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payments>
    {
        public void Configure(EntityTypeBuilder<Payments> builder)
        {
            builder.OwnsOne(a => a.PaymentState).ToTable("PaymentState");
            builder.Property(a => a.SecurityCode).HasMaxLength(5);
            builder.Property(a => a.CreditCardNumber).HasMaxLength(25);
            builder.Property(a => a.Reference).HasMaxLength(32);
            builder.Property(a => a.CardHolder).HasMaxLength(100);
            builder.Property(a => a.Amount).HasColumnType("decimal(18, 4)");
            builder.HasIndex(a => a.Reference).IsUnique();
        }
    }
}
