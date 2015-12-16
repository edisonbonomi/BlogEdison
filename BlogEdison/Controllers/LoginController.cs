using BlogEdison.DB;
using BlogEdison.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogEdison.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        //Só permitirá acesso para esta Action dentro do controler
        [AllowAnonymous]
        public ActionResult Index(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel viewModel, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var conexao = new ConexaoBanco();
            var usuario = (from p in conexao.Usuarios
                           where p.Login.ToUpper() == viewModel.Login.ToUpper()
                           && p.Senha == viewModel.Senha
                           select p).FirstOrDefault();

            if (usuario == null)
            {
                //dessa forma exibiria o erro embaixo do campo login
                //ModelState.AddModelError("Login", "Usuário e/ou senha estão incorretos");
                ModelState.AddModelError("", "Usuário e/ou senha estão incorretos");
                return View(viewModel);
            }

            FormsAuthentication.SetAuthCookie(usuario.Login, viewModel.Lembrar);

            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Index", "Blog");
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }

    }
}