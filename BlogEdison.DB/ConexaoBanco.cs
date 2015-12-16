﻿using BlogEdison.DB.Classes;
using BlogEdison.DB.Infra;
using BlogEdison.DB.Mapeamentos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEdison.DB
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ConexaoBanco : DbContext
    {
        #region Construtor da classe
        public ConexaoBanco() : base("ConexaoMySQL")
        {
            Database.Log = (p => Debug.WriteLine(p));
        }
        #endregion

        #region Criacao do DbSet
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Download> Downloads { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagPost> TagPosts{ get; set; }
        public DbSet<Visita> Visitas { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<ConexaoBanco>(new DropCreateDatabaseIfModelChanges<ConexaoBanco>());
            Database.SetInitializer<ConexaoBanco>(new MeuCriadorDeBanco());

            modelBuilder.Configurations.Add(new UsuarioConfig());
            modelBuilder.Configurations.Add(new PostConfig());
            modelBuilder.Configurations.Add(new ArquivoConfig());
            modelBuilder.Configurations.Add(new ComentarioConfig());
            modelBuilder.Configurations.Add(new DownloadConfig());
            modelBuilder.Configurations.Add(new ImagemConfig());
            modelBuilder.Configurations.Add(new TagConfig());
            modelBuilder.Configurations.Add(new TagPostConfig());
            modelBuilder.Configurations.Add(new VisitaConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
