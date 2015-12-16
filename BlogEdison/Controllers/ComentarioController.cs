using BlogEdison.Models.Comentarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEdison.Controllers
{
    public class ComentarioController : Controller
    {
        // GET: Comentario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastrarComentario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarComentario( CadastrarComentarioViewModel viewModel)
        {
            return View();
        }

    }
}