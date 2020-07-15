using System;
using System.Collections.Generic;
using System.IO;

namespace eplayers.Models
{
    public class Equipe : EPlayersBase , IEquipe
    {
        public int IdEquipe { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        private const string PATH = "Database/equipe.csv";

        /// <summary>
        /// metodo construtor
        /// </summary>
        public Equipe(){
            CreateFolderAndFile(PATH);
        }

        /// <summary>
        /// metodo para alterar uma equipe
        /// </summary>
        /// <param name="e"></param>
        public void Alterar(Equipe e)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == e.IdEquipe.ToString());
            linhas.Add( PrepararLinha(e) );
            RewriteCSV(PATH, linhas);
        }

        /// <summary>
        /// metodo para criar uma nova equipe 
        /// </summary>
        /// <param name="e"></param>
        public void Criar(Equipe e)
        {
            string[] linha = { PrepararLinha(e) };
            File.AppendAllLines(PATH, linha);
        }
        private string PrepararLinha(Equipe e){
            return $"{e.IdEquipe};{e.Nome};{e.Imagem}";
        }

        /// <summary>
        /// metodo para ler e escrever no csv
        /// </summary>
        /// <returns></returns>
        public List<Equipe> Ler()
        {
            List<Equipe> equipes = new List<Equipe>();
            string[] linhas = File.ReadAllLines(PATH);
            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");
                Equipe equipe = new Equipe();
                equipe.IdEquipe = Int32.Parse(linha[0]);
                equipe.Nome = linha[1];
                equipe.Imagem = linha[2];

                equipes.Add(equipe);
            }
            return equipes;
        }
        
        /// <summary>
        /// metodo para excluit uma equipe do csv
        /// </summary>
        /// <param name="id">id da equipe</param>
        public void Remover(int id)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());
            RewriteCSV(PATH, linhas);
        }
    }
}