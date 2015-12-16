using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEdison.DB.Classes;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEdison.DB.Mapeamentos
{
    public class TagPostConfig : EntityTypeConfiguration<TagPost>
    {
        public TagPostConfig()
        {
            ToTable("TAGPOST");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("IDTAGPOST")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.IdPost)
                .HasColumnName("IDPOST")
                .IsRequired();

            Property(x => x.IdTag)
                .HasColumnName("IDTAG")
                .IsRequired();

            HasRequired(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.IdPost);

            HasRequired(x => x.Tag)
                .WithMany()
                .HasForeignKey(x => x.IdTag);

        }
    }
}
