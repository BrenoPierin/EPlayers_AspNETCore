using System.Collections.Generic;
using eplayers.Models;

namespace eplayers.Interfaces
{
    public interface INoticias
    {
        void Criar(Noticias n);
        List<Noticias> Ler();

        void Alterar(Noticias n);

        void Remover(int id);
    }
}