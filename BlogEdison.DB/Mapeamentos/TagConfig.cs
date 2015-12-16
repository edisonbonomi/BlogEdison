using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEdison.DB.Classes;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEdison.DB.Mapeamentos
{
    public class TagConfig : EntityTypeConfiguration<Tag>
    {
        public TagConfig()
        {
            ToTable("TAG");

            HasKey(x => x.IdTag);

            Property(x => x.IdTag)
                .HasColumnName("IDTAG")
                .HasMaxLength(20)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
