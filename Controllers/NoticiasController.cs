using System;
using Microsoft.AspNetCore.Mvc;
using eplayers.Models;
using Microsoft.AspNetCore.Http;

namespace eplayers.Controllers
{
    public class NoticiasController : Controller
    {

        Noticias noticiaModel = new Noticias();


        public IActionResult Index()
        {
            ViewBag.Noticias = noticiaModel.Ler();
            return View();
        }

        public IActionResult Cadastrar(IFormCollection form)
        {
            Noticias novaNoticia = new Noticias();
            novaNoticia.IdNoticia = Int32.Parse( form["IdEquipe"] );
            novaNoticia.Titulo = form["Nome"];
            novaNoticia.Texto = form["Nome"];
            novaNoticia.Imagem = form["Imagem"];

            noticiaModel.Criar(novaNoticia);
            ViewBag.Equipes = noticiaModel.Ler();

            return LocalRedirect("~/Noticias");
            
        }

    }
}
