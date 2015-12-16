using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogEdison.Models.Usuario
{
    public class CadastrarUsuarioViewModel
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Login")]
        [Required(ErrorMessage = "O campo Login é obrigatório.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "A quantidade de caracteres no campo Login deve ser entre {2} e {1}.")]
        public string Login { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo Nome deve ser entre {2} e {1}.")]
        public string Nome { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A quantidade de caracteres no campo Senha deve ser entre {2} e {1}.")]
        public string Senha { get; set; }

        [DisplayName("Confirmar senha")]
        [Required(ErrorMessage = "O campo confirmar senha é obrigatório.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A quantidade de caracteres no campo confirmar senha deve ser entre {2} e {1}.")]
        [Compare("Senha", ErrorMessage ="As senhas digitadas não conferem.")]
        public string ConfirmarSenha { get; set; }

    }
}