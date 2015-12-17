using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEdison.DB.Classes;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEdison.DB.Mapeamentos
{
    public class ComentarioConfig : EntityTypeConfiguration<Comentario>
    {
        public ComentarioConfig()
        {
            ToTable("COMENTARIO");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("IDCOMENTARIO")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Descricao)
                .HasColumnName("DESCRICAO")
                .IsRequired();

            Property(x => x.AdmPost)
                .HasColumnName("ADMPOST")
                .IsRequired();

            Property(x => x.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(100);

            Property(x => x.PaginaWeb)
                .HasColumnName("PAGINAWEB")
                .HasMaxLength(100);

            Property(x => x.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.IdPost)
                .HasColumnName("IDPOST")
                .IsRequired();

            Property(x => x.DataHora)
                .HasColumnName("DATAHORA")
                .IsRequired();

            HasRequired(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.IdPost);

        }
    }
}
