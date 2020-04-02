using Alura.EntityFramework.Util;
using System.Collections.Generic;


namespace Alura.EntityFramework.Interface
{
    interface IProdutoDAO
    {
        void Adicionar(Produto p);
        void Atualizar(Produto p);
        void Remove(Produto p);
        IList<Produto> Produtos();
        void SaveChanges();

    }
}
