using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("Posicao", schema: "dom");

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id)
            .UseIdentityColumn()
            .HasColumnName("id_posicao");
        
        builder.Property(p => p.Cpf)
            .HasColumnName("cod_cpf")
            .HasColumnType("VARCHAR")
            .HasMaxLength(11)
            .IsRequired();
        
        builder.Property(p => p.Asset)
            .HasColumnName("cod_ativo")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(10)
            .IsRequired();
        
        builder.Property(p => p.Value)
            .HasColumnName("val_posicao")
            .HasColumnType("DECIMAL")
            .HasPrecision(15, 2)
            .HasDefaultValue(0)
            .IsRequired();   
        
        builder.Property(p => p.Date)
            .HasColumnName("dat_posicao")
            .HasColumnType("DATE")
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();   
        
        builder.Property(p => p.CreatedOn)
            .HasColumnName("dat_criacao")
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.Property(p => p.UpdatedOn)
            .HasColumnName("dat_atualizacao")
            .HasColumnType("DATETIME");
        
        builder.HasIndex(p => new { p.Cpf, p.Asset, p.Date }).IsUnique();          
    }
}