using ClientesApi.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApi.Infra.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento para a entidade Cliente
    /// </summary>
    public class ClienteMap: IEntityTypeConfiguration<Cliente>
    {
        //Método para mapeamento da entidade
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //Mapeando nome da tabela
            builder.ToTable("Cliente");

            //Mapeando Chave Primária
            builder.HasKey(c => c.IdCliente);

            //Mapeando cada campo
            builder.Property(c => c.IdCliente).HasColumnName("IdCliente");
            builder.Property(c => c.Nome).HasColumnName("Nome").HasMaxLength(100).IsRequired();
            builder.Property(c => c.Email).HasColumnName("Email").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Cpf).HasColumnName("Cpf").HasMaxLength(14).IsRequired();
            builder.Property(c => c.DataNascimento).HasColumnName("DataNascimento").IsRequired();
            
        }
    }
}
