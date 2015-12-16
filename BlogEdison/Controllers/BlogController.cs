using BlogEdison.DB;
using BlogEdison.DB.Classes;
using BlogEdison.Models.Administracao;
using BlogEdison.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEdison.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        #region Index
        public ActionResult Index(int? pagina, string tag, string pesquisa)
        {

            //abre a conexão do banco
            var conexaoBanco = new ConexaoBanco();
            //Controle de paginação
            var registrosPorPagina = 3;
            //se é nulo assume 1
            var paginaCorreta = pagina.GetValueOrDefault(1);
            //indice da página
            var indiceDaPagina = paginaCorreta - 1;
            //indica a quantidade de registros que precisamos pular
            var qtdeRegistrosPular = (indiceDaPagina * registrosPorPagina);

            //prepara a consulta SQL
            var posts = (from p in conexaoBanco.Posts
                         where p.Visivel == true
                         orderby p.DataPublicacao descending
                         select p);

            if (!string.IsNullOrEmpty(tag))
            {
                //prepara a consulta SQL
                posts = (from p in conexaoBanco.Posts
                             where p.TagPosts.Any(x => x.IdTag.ToUpper() == tag.ToUpper())
                             orderby p.DataPublicacao descending
                             select p);
            }

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //prepara a consulta SQL
                posts = (from p in conexaoBanco.Posts
                             where p.Titulo.ToUpper().Contains(pesquisa.ToUpper())
                                || p.Resumo.ToUpper().Contains(pesquisa.ToUpper())
                                || p.Descricao.ToUpper().Contains(pesquisa.ToUpper())
                             orderby p.DataPublicacao descending
                             select p);
            }

            //retorna a quantidade de registros
            var qtdeRegistros = posts.Count();
            //calcula o número de páginas
            var numeroPaginas = Math.Ceiling((Decimal)qtdeRegistros / registrosPorPagina);
            //instancia o modelo
            var viewModel = new ListarPostsViewModel();
            //retorna somente os registros selecionados
            viewModel.Posts = posts.Skip(qtdeRegistrosPular).Take(registrosPorPagina).ToList();
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalPaginas = (int)numeroPaginas;
            viewModel.Tag = tag;
            viewModel.Pesquisa = pesquisa;

            viewModel.Tags = (from p in conexaoBanco.Tags
                        where conexaoBanco.TagPosts.Any(x => x.IdTag.ToUpper() == p.IdTag.ToUpper())
                        orderby p.IdTag
                        select p).ToList();

            return View(viewModel);
        }

        public ActionResult _Paginacao()
        {
            return PartialView();
        }
        #endregion

        //aula11122015
        #region Post
        public ActionResult Post(int id)
        {
            var conexao = new ConexaoBanco();

            var postDados = (from x in conexao.Posts
                             where x.Id == id
                             select new DetalhesPostViewModel
                             {
                                Id = x.Id,
                                Titulo = x.Titulo,
                                Autor = x.Autor,
                                DataPublicacao = x.DataPublicacao,
                                HoraPublicacao = x.DataPublicacao,
                                Descricao = x.Descricao,
                                Resumo = x.Resumo,
                                Visivel = x.Visivel,
                                Tags = x.TagPosts.Select(p => p.IdTag).ToList()

                              }).FirstOrDefault();


            return View(postDados);
        }
        #endregion
    }
}