using System;
using System.Collections.Generic;
using System.Linq;
using Alura.Loja.Testes.ConsoleApp.Interface;
using Alura.Loja.Testes.ConsoleApp.Contexto;
using Alura.Loja.Testes.ConsoleApp.Util;

namespace Alura.Loja.Testes.ConsoleApp.Conexao
{
    public class ProdutoDAOEntity : IProdutoDAO, IDisposable
    {
        private LojaContext contexto;

        public ProdutoDAOEntity()
        {
            this.contexto = new LojaContext();
        }
        public void Adicionar(Produto p)
        {
            contexto.Produtos.Add(p);
            contexto.SaveChanges();
        }

        public void Atualizar(Produto p)
        {
            contexto.Produtos.Update(p);
            contexto.SaveChanges();
        }

        public void Dispose()
        {
            contexto.Dispose();
        }

        public IList<Produto> Produtos()
        {
            return contexto.Produtos.ToList();
        }

        public void Remove(Produto p)
        {
            contexto.Produtos.Remove(p);
            contexto.SaveChanges();
        }

        public void SaveChanges()
        {
            //
        }
    }
}
