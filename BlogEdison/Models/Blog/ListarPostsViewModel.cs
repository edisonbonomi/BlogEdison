using BlogEdison.DB.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogEdison.Models.Blog
{
    public class ListarPostsViewModel
    {
        public List<Post> Posts { get; set; }
        public List<Tag> Tags { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public string Tag { get; set; }
        public string Pesquisa { get; set; }
    }
}