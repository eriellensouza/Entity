using Loja.Testes.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.Loja.Testes.ConsoleApp
{
    public static class Help
    {
        public static void GravarUsandoEntity()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.PrecoUnitario = 19.89;

            using (var contexto = new ProdutoDAOEntity())
            {
                contexto.Adicionar(p);
                contexto.SaveChanges();
            }
        }

        public static void RecuperaProdutos()
        {
            using (var repo = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = repo.Produtos();
                Console.WriteLine("Foram encontrados {0} produto(s)", produtos.Count);

                foreach (var item in produtos)
                {
                    Console.WriteLine(item.Nome);
                }
            }
        }

        public static void ExcluirProdutos()
        {
            using (var repo = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = repo.Produtos();

                foreach (var item in produtos)
                {
                    repo.Remove(item);
                }
                repo.SaveChanges();
            }
        }

        public static void AtualizarProduto()
        {
            // inclui um produto
            GravarUsandoEntity();
            RecuperaProdutos();

            // atualiza o produto
            using (var repo = new ProdutoDAOEntity())
            {
                Produto primeiro = repo.Produtos().First();
                primeiro.Nome = "Cassino Royale - Editado";
                repo.Atualizar(primeiro);
                repo.SaveChanges();
            }
            RecuperaProdutos();
        }

        //private static void GravarUsandoAdoNet()
        //{
        //    Produto p = new Produto();
        //    p.Nome = "Harry Potter e a Ordem da Fênix";
        //    p.Categoria = "Livros";
        //    p.Preco = 19.89;

        //    using (var repo = new ProdutoDAO())
        //    {
        //        repo.Adicionar(p);
        //    }
        //}

        //using (var contexto = new LojaContext())
        //{
        //    var produtos = contexto.Produtos.ToList();
        //    foreach (var p in produtos)
        //    {
        //        Console.WriteLine(p);
        //    }

        //    var p1 = produtos.First();
        //    p1.Nome = "Harry Potter";

        //    contexto.SaveChanges();

        //    Console.WriteLine("=================");
        //    produtos = contexto.Produtos.ToList();
        //    foreach (var p in produtos)
        //    {
        //        Console.WriteLine(p);
        //    }
        //}
    }
}
