using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEdison.DB.Classes;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEdison.DB.Mapeamentos
{
    public class PostConfig : EntityTypeConfiguration<Post>
    {
        public PostConfig()
        {
            ToTable("POST");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("IDPOST")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Autor)
                .HasColumnName("AUTOR")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.DataPublicacao)
                .HasColumnName("DATAPUBLICACAO")
                .IsRequired();

            Property(x => x.Descricao)
                .HasColumnName("DESCRICAO")
                .IsRequired();

            Property(x => x.Resumo)
                .HasColumnName("RESUMO")
                .HasMaxLength(1000)
                .IsRequired();

            Property(x => x.Titulo)
                .HasColumnName("TITULO")
                .HasMaxLength(100);

            Property(x => x.Visivel)
                .HasColumnName("VISIVEL")
                .IsRequired();

            //public virtual IList<Comentario> Comentarios { get; set; }
            HasMany(x => x.Comentarios)
                .WithOptional()
                .HasForeignKey(x => x.IdPost);

            //public virtual IList<Arquivo> Arquivos { get; set; }
            HasMany(x => x.Arquivos)
                .WithOptional()
                .HasForeignKey(x => x.IdPost);

            //public virtual IList<Imagem> Imagens { get; set; }
            HasMany(x => x.Imagens)
                .WithOptional()
                .HasForeignKey(x => x.IdPost);

            //public virtual IList<Visita> Visitas { get; set; }
            HasMany(x => x.Visitas)
                .WithOptional()
                .HasForeignKey(x => x.IdPost);

            //public virtual IList<TagPost> TagPosts { get; set; }
            HasMany(x => x.TagPosts)
                .WithOptional()
                .HasForeignKey(x => x.IdPost);


        }
    }
}
