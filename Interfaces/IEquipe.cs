using System.Collections.Generic;
using eplayers.Models;

namespace eplayers.Models
{
    public interface IEquipe
    {
        void Criar(Equipe e);
        List<Equipe> Ler();

        void Alterar(Equipe e);

        void Remover(int id);
    }
    
}