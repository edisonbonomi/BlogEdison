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
        //atributos dos comentarios
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo nome deve ser entre {2} e {1}!")]
        public string ComentarioNome { get; set; }

        [DisplayName("E-mail")]
        [StringLength(100, ErrorMessage = "O campo e-mail deve possuir no máximo {1} caracteres!")]
        [EmailAddress(ErrorMessage ="E-mail inválido")]
        public string ComentarioEmail { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo descrição é obrigatório.")]
        public string ComentarioDescrição { get; set; }


    }
}