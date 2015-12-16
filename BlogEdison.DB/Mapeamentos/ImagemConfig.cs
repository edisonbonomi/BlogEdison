using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEdison.DB.Classes;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEdison.DB.Mapeamentos
{
    public class ImagemConfig : EntityTypeConfiguration<Imagem>
    {
        public ImagemConfig()
        {
            ToTable("IMAGEM");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("IDMAGEM")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Extensao)
                .HasColumnName("EXTENSAO")
                .IsRequired();

            Property(x => x.Bytes)
                .HasColumnName("BYTES")
                .IsRequired();

            Property(x => x.IdPost)
                .HasColumnName("IDPOST")
                .IsRequired();

            HasRequired(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.IdPost);

        }
    }
}
