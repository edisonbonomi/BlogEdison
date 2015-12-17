using BlogEdison.DB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogEdison.Models.Blog
{
	public class DetalhesPostViewModel
	{
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public DateTime DataPublicacao { get; set; }
        public DateTime HoraPublicacao { get; set; }
        public string Descricao { get; set; }
        public string Resumo { get; set; }
        public bool Visivel { get; set; }
        public List<string> Tags { get; set; }
        public int QtdeComentarios { get; set; }

        //atributos dos comentarios
        [DisplayName("Nome")]
        [StringLength(100, ErrorMessage = " O campo Nome deve possuir no máximo {1} carateres")]
        [Required(ErrorMessage = "* O campo Nome é obrigatório.")]
        public string ComentarioNome { get; set; }

        [DisplayName("E-mail")]
        [StringLength(100, ErrorMessage = " O campo E-mail deve possuir no máximo {1} carateres")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string ComentarioEmail { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public string ComentarioDescricao { get; set; }

        [DisplayName("Página Web")]
        [StringLength(100, ErrorMessage = "O campo Página Web  deve possuir no máximo {1} carateres")]
        public string ComentarioPaginaWeb { get; set; }

        /*listar comentarios*/
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public IList<Comentario> Comentarios { get; set; }

    }
}