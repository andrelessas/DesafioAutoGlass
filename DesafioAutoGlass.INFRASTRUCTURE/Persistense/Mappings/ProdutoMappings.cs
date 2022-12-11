using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioAutoGlass.INFRASTRUCTURE.Persistense.Mappings
{
    public class ProdutoMappings : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).IsRequired();
            builder.Property(d => d.Descricao).IsRequired();
            builder.HasOne(x => x.Fornecedor)
                .WithMany(x => x.Produto)
                .HasForeignKey(x => x.IdFornecedor);
        }
    }
}