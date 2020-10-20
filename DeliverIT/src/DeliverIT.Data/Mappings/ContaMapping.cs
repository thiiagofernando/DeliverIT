using DeliverIT.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverIT.Data.Mappings
{
    public class ContaMapping : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).IsRequired();

            builder.Property(p => p.DiasEmAtraso).IsRequired();

            builder.Property(p => p.ValorOriginal)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ValorCorrigido)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.DataVencimento).IsRequired();
            
            builder.Property(p => p.DataPagamento).IsRequired();
        }
    }
}
