using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEdison.DB.Classes;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEdison.DB.Mapeamentos
{
    public class DownloadConfig : EntityTypeConfiguration<Download>
    {
        public DownloadConfig()
        {
            ToTable("DOWNLOAD");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("IDDOWNLOAD")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Ip)
                .HasColumnName("IP")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.DataHora)
                .HasColumnName("DATAHORA")
                .IsRequired();

            Property(x => x.IdArquivo)
                .HasColumnName("IDARQUIVO")
                .IsRequired();

            HasRequired(x => x.Arquivo)
                .WithMany()
                .HasForeignKey(x => x.IdArquivo);

        }
    }
}
