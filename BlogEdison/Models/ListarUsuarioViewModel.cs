using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogEdison.Models.Usuarios
{
    public class ListarUsuarioViewModel
    {
        public List<Usuario> Usuarios { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
    }
}


