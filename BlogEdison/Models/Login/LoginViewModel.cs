using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogEdison.Models.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo login é obrigatório.")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name = "Lembrar")]
        public bool Lembrar { get; set; }

    }
}