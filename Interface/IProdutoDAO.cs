using Loja.Testes.ConsoleApp;
using Loja.Testes.ConsoleApp.Util;
using System.Collections.Generic;


namespace Alura.Loja.Testes.ConsoleApp.Interface
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
