using System;
using Microsoft.AspNetCore.Mvc;
using eplayers.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace eplayers.Controllers
{
    public class NoticiasController : Controller
    {
        
        Noticias noticiaModel = new Noticias();

        /// <summary>
        /// metodo construtor de noticias
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.Noticias = noticiaModel.Ler();
            return View();
        }

        /// <summary>
        /// metodo de cadastrar noticias
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public IActionResult Cadastrar(IFormCollection form)
        {
            Noticias novaNoticia = new Noticias();
            novaNoticia.IdNoticia = Int32.Parse( form["IdNoticia"] );
            novaNoticia.Titulo = form["Titulo"];
            novaNoticia.Texto = form["Texto"];
           
           // Upload Início
            var file    = form.Files[0];
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Noticias");

            if(file != null)
            {
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))  
                {  
                    file.CopyTo(stream);  
                }
                novaNoticia.Imagem   = file.FileName;
            }
            else
            {
                novaNoticia.Imagem   = "padrao.png";
            }
            // Upload Final

            noticiaModel.Criar(novaNoticia);
            ViewBag.Equipes = noticiaModel.Ler();

            return LocalRedirect("~/Noticias");
            
        }

        /// <summary>
        /// metodo para apagar noticia
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Notica/{id}")]
        public IActionResult Excluir(int id)
        {
            noticiaModel.Remover(id);
            return LocalRedirect("~/Noticias");

        }

    }
}
