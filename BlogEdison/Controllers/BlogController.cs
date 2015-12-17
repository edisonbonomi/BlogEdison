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

            viewModel.Posts = (from p in posts
                               orderby p.DataPublicacao descending
                               select new DetalhesPostViewModel
                               {
                                   DataPublicacao = p.DataPublicacao,
                                   Autor = p.Autor,
                                   Descricao = p.Descricao,
                                   Id = p.Id,
                                   Resumo = p.Resumo,
                                   Titulo = p.Titulo,
                                   Visivel = p.Visivel,
                                   QtdeComentarios = p.Comentarios.Count
                               }).Skip(qtdeRegistrosPular).Take(registrosPorPagina).ToList();

            //retorna somente os registros selecionados
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
        public ActionResult Post(int id, int? pagina)
        {
            var conexao = new ConexaoBanco();

            var postDados = (from x in conexao.Posts
                             where x.Id == id
                             select x).FirstOrDefault();
            if (postDados == null)
            {
                throw new Exception(string.Format("Post código {0} não encontrado.", id));
            }

            var viewModel = new DetalhesPostViewModel();

            preencherViewModel(postDados, viewModel, pagina);

            return View(viewModel);
        }

        private static void preencherViewModel(Post postDados, DetalhesPostViewModel viewModel, int? pagina)
        {
            viewModel.Id = postDados.Id;
            viewModel.Titulo = postDados.Titulo;
            viewModel.Autor = postDados.Autor;
            viewModel.DataPublicacao = postDados.DataPublicacao;
            viewModel.HoraPublicacao = postDados.DataPublicacao;
            viewModel.Descricao = postDados.Descricao;
            viewModel.Resumo = postDados.Resumo;
            viewModel.Visivel = postDados.Visivel;
            viewModel.QtdeComentarios = postDados.Comentarios.Count;
            viewModel.Tags = postDados.TagPosts.Select(p => p.IdTag).ToList();
            var paginaCorreta = pagina.GetValueOrDefault(1);
            var registrosPorPagina = 3;
            var indiceDaPagina = paginaCorreta - 1;
            var qtdeRegistrosPular = (indiceDaPagina * registrosPorPagina);
            var qtdeRegistros = postDados.Comentarios.Count;
            var qtdePaginas = Math.Ceiling((Decimal)qtdeRegistros / registrosPorPagina);
            viewModel.Comentarios = (from p in postDados.Comentarios
                                     orderby p.DataHora descending
                                     select p).Skip(qtdeRegistrosPular).Take(registrosPorPagina).ToList();
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalPaginas = (int)qtdePaginas;
        }
        #endregion

        [HttpPost]
        public ActionResult Post(DetalhesPostViewModel viewModel)
        {
            var conexao = new ConexaoBanco();
            var post = (from p in conexao.Posts
                        where p.Id == viewModel.Id
                        select p).FirstOrDefault();

            if (ModelState.IsValid)
            {

                if (post == null)
                {
                    throw new Exception(string.Format("Post código {0} não encontrado.", viewModel.Id));
                }

                var comentario = new Comentario();

                comentario.IdPost = viewModel.Id;
                comentario.AdmPost = HttpContext.User.Identity.IsAuthenticated;
                comentario.Nome = viewModel.ComentarioNome;
                comentario.Descricao = viewModel.ComentarioDescricao;
                comentario.Email = viewModel.ComentarioEmail;
                comentario.PaginaWeb = viewModel.ComentarioPaginaWeb;
                comentario.DataHora = DateTime.Now;

                try
                {
                    conexao.Comentarios.Add(comentario);
                    conexao.SaveChanges();

                    /**********************************************************
                    return RedirectToAction("Post", new
                    {
                        ano = post.DataPublicacao.Year,
                        mes = post.DataPublicacao.Month,
                        dia = post.DataPublicacao.Day,
                        titulo = post.Titulo,
                        id = post.Id
                    });
                    ***********************************************************/
                    /**********************************************************
                    Modificado para incluir a Hashtag comentario no final da
                    Url e posicionar o foco nos comentários no final da página
                    ***********************************************************/
                    return Redirect(Url.Action("Post", new
                    {
                        ano = viewModel.DataPublicacao.Year,
                        mes = viewModel.DataPublicacao.Month,
                        dia = viewModel.DataPublicacao.Day,
                        titulo = viewModel.Titulo,
                        id = viewModel.Id
                    })+"#comentarios");

                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }
            }

            preencherViewModel(post, viewModel, null);

            return View(viewModel);
        }
    }
}