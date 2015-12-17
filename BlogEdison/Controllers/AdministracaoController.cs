using BlogEdison.DB;
using BlogEdison.DB.Classes;
using BlogEdison.Models.Administracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEdison.Controllers
{
    [Authorize]
    public class AdministracaoController : Controller

    {
        // GET: Administracao
        public ActionResult Index()
        {

            return View();
        }

        #region Cadastrar Post
        public ActionResult CadastrarPost()
        {
            var viewModel = new CadastrarPostViewModel();
            viewModel.DataPublicacao = DateTime.Now;
            viewModel.HoraPublicacao = DateTime.Now;
            viewModel.Autor = "Edison Eduardo Bonomi";
            viewModel.Visivel = true;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarPost(CadastrarPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var conexao = new ConexaoBanco();
                var postDados = new Post();

                /* uma forma de converter
                string dataHora;
                dataHora = viewModel.DataPublicacao.ToString("dd/MM/yyyy") + " " + viewModel.HoraPublicacao.ToString("hh:mm:ss");
                postDados.DataPublicacao = Convert.ToDateTime(dataHora);
                */
                postDados.Titulo = viewModel.Titulo;
                postDados.Autor = viewModel.Autor;
                postDados.DataPublicacao = new DateTime(viewModel.DataPublicacao.Year,
                                                        viewModel.DataPublicacao.Month,
                                                        viewModel.DataPublicacao.Day,
                                                        viewModel.HoraPublicacao.Hour,
                                                        viewModel.HoraPublicacao.Minute,
                                                        viewModel.HoraPublicacao.Second);
                postDados.Descricao = viewModel.Descricao;
                postDados.Resumo = viewModel.Resumo;
                postDados.Visivel = viewModel.Visivel;
                postDados.TagPosts = new List<TagPost>();

                //se tem tags no post
                if (viewModel.Tags != null)
                {
                    foreach(var item in viewModel.Tags)
                    {
                        var tagExiste = (from p in conexao.Tags
                                         where p.IdTag.ToLower() == item.ToLower()
                                         select p).Any();

                        if(!tagExiste)
                        {
                            var tagClass = new Tag();
                            tagClass.IdTag = item;
                            conexao.Tags.Add(tagClass);
                        }

                        var tagPost = new TagPost();
                        tagPost.IdTag = item;
                        postDados.TagPosts.Add(tagPost); 
                    }
                        
                }

                conexao.Posts.Add(postDados);

                try
                {
                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);

                }

            }
            return View(viewModel);
        }
        #endregion

        #region Editar Post
        public ActionResult EditarPost(int id)
        {
            var conexao = new ConexaoBanco();

            //var postDados = conexao.Posts.FirstOrDefault(x => x.Id == id);
            var postDados = (from x in conexao.Posts where x.Id == id select x).FirstOrDefault();

            if (postDados == null)
            {
                throw new Exception(string.Format("Post com código {0} não encontrado.", id));
            }

            var viewModel = new CadastrarPostViewModel();
            viewModel.Id = postDados.Id;
            viewModel.Titulo = postDados.Titulo;
            viewModel.Autor = postDados.Autor;
            viewModel.DataPublicacao = postDados.DataPublicacao;
            viewModel.HoraPublicacao = postDados.DataPublicacao;
            viewModel.Descricao = postDados.Descricao;
            viewModel.Resumo = postDados.Resumo;
            viewModel.Visivel = postDados.Visivel;
            viewModel.Tags = (from p in postDados.TagPosts
                              select p.IdTag).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditarPost(CadastrarPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var conexao = new ConexaoBanco();

                //var postDados = conexao.Posts.FirstOrDefault(x => x.Id == viewModel.Id);
                var postDados = (from x in conexao.Posts where x.Id == viewModel.Id select x).FirstOrDefault();

                postDados.Titulo = viewModel.Titulo;
                postDados.Autor = viewModel.Autor;
                postDados.DataPublicacao = new DateTime(viewModel.DataPublicacao.Year,
                                                        viewModel.DataPublicacao.Month,
                                                        viewModel.DataPublicacao.Day,
                                                        viewModel.HoraPublicacao.Hour,
                                                        viewModel.HoraPublicacao.Minute,
                                                        viewModel.HoraPublicacao.Second);
                postDados.Descricao = viewModel.Descricao;
                postDados.Resumo = viewModel.Resumo;
                postDados.Visivel = viewModel.Visivel;
                postDados.TagPosts = new List<TagPost>();

                //busca as tags atuais
                var tagsAtuais = postDados.TagPosts.ToList();
                
                //exclui todas as tags do post
                foreach( var item in tagsAtuais)
                {
                    conexao.TagPosts.Remove(item); 
                }
                
                //se tem tags no post
                if (viewModel.Tags != null)
                {
                    foreach (var item in viewModel.Tags)
                    {
                        var tagExiste = (from p in conexao.Tags
                                         where p.IdTag.ToLower() == item.ToLower()
                                         select p).Any();

                        if (!tagExiste)
                        {
                            var tagClass = new Tag();
                            tagClass.IdTag = item;
                            conexao.Tags.Add(tagClass);
                        }

                        var tagPost = new TagPost();
                        tagPost.IdTag = item;
                        postDados.TagPosts.Add(tagPost);
                    }

                }


                try
                {
                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                    
                }

            }
            return View(viewModel);
        }
        #endregion

        #region Excluir Post
        public ActionResult ExcluirPost(int id)
        {
            var conexao = new ConexaoBanco();

            //var postDados = conexao.Posts.FirstOrDefault(x => x.Id == viewModel.Id);
            var postDados = (from x in conexao.Posts where x.Id == id select x).FirstOrDefault();

            if (postDados == null)
            {
                throw new Exception(string.Format("Post com código {0} não existe.", id));
            }

            conexao.Posts.Remove(postDados);

            try
            {
                conexao.SaveChanges();
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);

            }

            return RedirectToAction("Index", "Blog");
        }
        #endregion

        #region ExcluirComentario
        public ActionResult ExcluirComentario(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var comentario = (from p in conexaoBanco.Comentarios
                              where p.Id == id
                              select p).FirstOrDefault();
            if (comentario == null)
            {
                throw new Exception(string.Format("Comentário código {0} não foi encontrado.", id));
            }
            conexaoBanco.Comentarios.Remove(comentario);
            conexaoBanco.SaveChanges();

            var post = (from p in conexaoBanco.Posts
                        where p.Id == comentario.IdPost
                        select p).First();
            return Redirect(Url.Action("Post", "Blog", new
            {
                ano = post.DataPublicacao.Year,
                mes = post.DataPublicacao.Month,
                dia = post.DataPublicacao.Day,
                titulo = post.Titulo,
                id = post.Id
            }) + "#comentarios");
        }
        #endregion
    }
    }