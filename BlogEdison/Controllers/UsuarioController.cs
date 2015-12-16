using BlogEdison.DB;
using BlogEdison.DB.Classes;
using BlogEdison.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEdison.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        //Lista os usuários
        public ActionResult Index(int? pagina)
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
            var dados = (from p in conexaoBanco.Usuarios
                         where p.Login != "ADM"
                         orderby p.Nome descending
                         select p);
            //retorna a quantidade de registros
            var qtdeRegistros = dados.Count();
            //calcula o número de páginas
            var numeroPaginas = Math.Ceiling((Decimal)qtdeRegistros / registrosPorPagina);
            //instancia o modelo
            var viewModel = new ListarUsuarioViewModel();
            //retorna somente os registros selecionados
            viewModel.Usuarios = dados.Skip(qtdeRegistrosPular).Take(registrosPorPagina).ToList();
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalPaginas = (int)numeroPaginas;

            return View(viewModel);
        }

        // GET: Usuario
        public ActionResult CadastrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var conexao = new ConexaoBanco();
                //var postDados = conexao.Posts.FirstOrDefault(x => x.Id == id);


                var dados = new Usuario();
                //Login será armazenado sempre em maiúsculo
                dados.Login = viewModel.Login.ToUpper();
                dados.Nome = viewModel.Nome;
                //Senha será armazenado sempre em minúsculo
                dados.Senha = viewModel.Senha.ToLower();

                conexao.Usuarios.Add(dados);

                try
                {
                    var testeLogin = (from x in conexao.Usuarios
                                      where x.Login == viewModel.Login.ToUpper()
                                      select x).Any(); //Any() retorna true/false

                    if (testeLogin)
                    {
                        throw new Exception(string.Format("O Login informado [ {0} ] já está cadastrado.", viewModel.Login));
                    }

                    conexao.SaveChanges();
                    //redireciona para o Index do controller atual
                    return RedirectToAction("Index");
                    //redireciona para o Index do controller Administracao
                    //return RedirectToAction("Index", "Administracao");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);

                }

            }
            return View(viewModel);
        }

        public ActionResult EditarUsuario(int id)
        {
            var conexao = new ConexaoBanco();

            try
            {
                //var postDados = conexao.Posts.FirstOrDefault(x => x.Id == id);
                var dados = (from x in conexao.Usuarios where x.Id == id select x).FirstOrDefault();

                if (dados == null)
                {
                    throw new Exception(string.Format("Usuário com código {0} não encontrado.", id));
                }

                var viewModel = new CadastrarUsuarioViewModel();
                viewModel.Id = dados.Id;
                viewModel.Login = dados.Login;
                viewModel.Nome = dados.Nome;
                viewModel.Senha = "";

                return View(viewModel);
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }

            return View();
        }

        [HttpPost]
        public ActionResult EditarUsuario(CadastrarUsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var conexao = new ConexaoBanco();

                try
                {
                    var testeLogin = (from x in conexao.Usuarios
                                      where x.Login == viewModel.Login.ToUpper() && x.Id != viewModel.Id
                                      select x).Any(); //Any() retorna true/false

                    if (testeLogin)
                    {
                        throw new Exception(string.Format("O Login informado [ {0} ] já está cadastrado.", viewModel.Login));
                    }

                    //var postDados = conexao.Posts.FirstOrDefault(x => x.Id == id);
                    var dados = (from x in conexao.Usuarios where x.Id == viewModel.Id select x).FirstOrDefault();

                    if (dados == null)
                    {
                        throw new Exception(string.Format("Usuário com código {0} não encontrado.", viewModel.Id));
                    }

                    dados.Id = viewModel.Id;
                    dados.Login = viewModel.Login.ToUpper();
                    dados.Nome = viewModel.Nome;
                    dados.Senha = viewModel.Senha.ToLower();

                    conexao.SaveChanges();
                    //redireciona para o Index do controller atual
                    return RedirectToAction("Index");
                    //redireciona para o Index do controller Administracao
                    //return RedirectToAction("Index", "Administracao");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);

                }
            }
            return View(viewModel);
        }

        public ActionResult _Paginacao()
        {
            return PartialView();
        }

        public ActionResult ExcluirUsuario(int id)
        {
            var conexao = new ConexaoBanco();

            //var postDados = conexao.Posts.FirstOrDefault(x => x.Id == viewModel.Id);
            var dados = (from x in conexao.Usuarios where x.Id == id select x).FirstOrDefault();

            if (dados == null)
            {
                throw new Exception(string.Format("Post com código {0} não existe.", id));
            }

            conexao.Usuarios.Remove(dados);

            try
            {
                conexao.SaveChanges();
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);

            }

            return RedirectToAction("Index", "Usuario");
        }

    }
}