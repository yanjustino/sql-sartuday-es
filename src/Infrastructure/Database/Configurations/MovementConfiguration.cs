using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class MovementConfiguration : IEntityTypeConfiguration<Movement>
{
    public void Configure(EntityTypeBuilder<Movement> builder)
    {
        builder.ToTable("Movimento", schema: "dom");
        builder.HasKey(k => k.Id);
        builder.Ignore(p => p.CodePosition);
        
        builder.Property(p => p.Id)
            .UseIdentityColumn()
            .HasColumnName("id_movimento");
        
        builder.Property(p => p.PositionId)
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
        
        builder.Property(p => p.Quantity)
            .HasColumnName("qtd_ativo")
            .HasColumnType("INTEGER")
            .HasDefaultValue(1)
            .IsRequired();   
        
        builder.Property(p => p.Price)
            .HasColumnName("val_ativo")
            .HasColumnType("DECIMAL")
            .HasPrecision(15,2)
            .HasDefaultValue(0)
            .IsRequired();    
        
        builder.Property(p => p.PriceCurve)
            .HasColumnName("val_ativo_curva")
            .HasColumnType("DECIMAL")
            .HasPrecision(15,2)
            .HasDefaultValue(0)
            .IsRequired();         
        
        builder.Property(p => p.Type)
            .HasColumnName("tip_ativo")
            .HasColumnType("CHAR")
            .HasMaxLength(1)
            .IsRequired();         
        
        builder.Property(p => p.Date)
            .HasColumnName("dat_movimento")
            .HasColumnType("DATE")
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();   
        
        builder.Property(p => p.CreatedOn)
            .HasColumnName("dat_criacao")
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.Ignore(p => p.Value);
        
        builder.Property(p => p.UpdatedOn)
            .HasColumnName("dat_atualizacao")
            .HasColumnType("DATETIME")
            .IsRequired();  
        
        builder
            .HasOne<Position>(s => s.Position)
            .WithMany(g => g.Movements)
            .HasForeignKey(s => s.PositionId); 
    }
}