using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using eplayers.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace eplayers.Controllers
{
    public class EquipeController : Controller
    {

        /// <summary>
        /// Cria nova equipe
        /// </summary>
        /// <returns></returns>
        Equipe equipeModel = new Equipe();


        /// <summary>
        /// Metodo construtor de equipe
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.Equipes = equipeModel.Ler();
            return View();
        }

        /// <summary>
        /// Metodo para cadastrar equipes
        /// </summary>
        /// <param name="form"></param>
        /// <returns>equipe cadstrada</returns>
        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe novaEquipe = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse( form["IdEquipe"] );
            novaEquipe.Nome = form["Nome"];
            
            // Upload Início
            var file    = form.Files[0];
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

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
                novaEquipe.Imagem   = file.FileName;
            }
            else
            {
                novaEquipe.Imagem   = "padrao.png";
            }
            // Upload Final

            equipeModel.Criar(novaEquipe);
            ViewBag.Equipes = equipeModel.Ler();

            return LocalRedirect("~/Equipe");
            
        }


        /// <summary>
        /// metodo para apagar equipe
        /// </summary>
        /// <param name="id">id da equipe</param>
        /// <returns>equipe apagada</returns>
        [Route("Equpe/{id}")]
        public IActionResult Excluir(int id)
        {
            equipeModel.Remover(id);
            return LocalRedirect("~/Equipe");

        }

    }
}