using Alura.EntityFramework.Conexao;
using Alura.EntityFramework.Contexto;
using Alura.EntityFramework.Util;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace EntityFramework
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fulaninho = new Cliente();
            fulaninho.Nome = "Euzinha";
            //fulaninho.EnderecoDeEntrega = ?


        }
        private static void MuitosParaMuitos()
        { 
            var p1 = new Produto()
            { Nome= "Laranja", Categoria = "Hortifruti", PrecoUnitario = 0.50, Unidade = "Kilo"};
            var p2 = new Produto()
            { Nome = "Gin", Categoria = "Bebidas alcóolicas", PrecoUnitario = 80.0, Unidade = "Unidade" };
            var p3 = new Produto()
            { Nome = "Taça", Categoria = "Utensílios Domesticos", PrecoUnitario = 5.0, Unidade = "Unidade" };

            var promocaoPascoa = new Promocao();

            promocaoPascoa.Descricao = "Pascoa Feliz!";
            promocaoPascoa.DataInicio = DateTime.Now;
            promocaoPascoa.DataFim = DateTime.Now.AddMonths(3);

            promocaoPascoa.IncluiProduto(p1);
            promocaoPascoa.IncluiProduto(p2);
            promocaoPascoa.IncluiProduto(p3);

            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                //Adicionar
                contexto.Add(promocaoPascoa);

                //Deletar
                //var promocao = contexto.Promocoes.Find(2);
                //contexto.Promocoes.Remove(promocao);

                ExibeEntries(contexto.ChangeTracker.Entries());
                contexto.SaveChanges();
                ExibeEntries(contexto.ChangeTracker.Entries());
            }
        }

        private static void ExibeEntries(IEnumerable<EntityEntry> entries)
        {
            foreach (var e in entries)
            {
                Console.WriteLine
                (
                    e.Entity.ToString() + "\n" + e.State 
                );
            }
            
        }

        
    }
}

//static void Main(string[] args)
//{
//    //compra 6 pães franceses

//    var paoFrances = new Produto();
//    paoFrances.Nome = "Pão Francês";
//    paoFrances.PrecoUnitario = 0.40;
//    paoFrances.Unidade = "Unidade";
//    paoFrances.Categoria = "Padaria";

//    var compra = new Compra();
//    compra.Quantidade = 6;
//    compra.Produto = paoFrances;
//    compra.Preco = paoFrances.PrecoUnitario * compra.Quantidade;


//    using (var contexto = new LojaContext())
//    {
//        var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
//        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
//        loggerFactory.AddProvider(SqlLoggerProvider.Create());

//        contexto.Compras.Add(compra);
//        ExibeEntries(contexto.ChangeTracker.Entries());
//        contexto.SaveChanges();
//        ExibeEntries(contexto.ChangeTracker.Entries());
//    }
//}

//private static void ExibeEntries(IEnumerable<EntityEntry> entries)
//{
//    foreach (var e in entries)
//    {
//        Console.WriteLine(e.Entity.ToString() + " - " + e.State);
//    }

//}


//static List<Produto> produtos = new List<Produto>();
//static void Main(string[] args)
//{

//    using (var contexto = new LojaContext())
//    {
//        produtos = contexto.Produtos.ToList();

//        MostaDadosBD(contexto);

//        var p1 = produtos.LastOrDefault();
//        p1.Nome = "O Homem Invisivél";

//        Console.WriteLine("========================================");

//        foreach (var e in contexto.ChangeTracker.Entries())
//        {
//            Console.WriteLine(e.State);
//        }

//        contexto.SaveChanges();

//        MostaDadosBD(contexto);
//    }
//}

//static void MostaDadosBD(LojaContext context)
//{
//    foreach (var n in produtos)
//    {
//        Console.WriteLine(n.ToString());
//    }
//}


//using (var contexto = new LojaContext())
//{
//    var insere = new ProdutoDAOEntity();
//    var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
//    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
//    loggerFactory.AddProvider(SqlLoggerProvider.Create());

//    var novoProduto = new Produto()
//    {
//        Nome = "Harry Potter e a Ordem da Fênix",
//        Categoria = "Livros",
//        Preco = 19.89
//    };
//    insere.Adicionar(novoProduto);

//    var novoProduto2 = new Produto()
//    { 
//        Nome = "Marley",
//        Categoria = "Livros",
//        Preco = 19.99
//    };
//    insere.Adicionar(novoProduto2);

//    var produtos = contexto.Produtos.ToList();

//    foreach (var p in produtos)
//    {
//        Console.WriteLine(p);
//    }

//    Console.WriteLine("=================");
//    foreach (var e in contexto.ChangeTracker.Entries())
//    {
//        Console.WriteLine(e.State);
//    }



//    var p1 = produtos.Last();
//    p1.Nome = "007 - O Espiao Que Me Amava";

//    Console.WriteLine("=================");
//    foreach (var e in contexto.ChangeTracker.Entries())
//    {
//        Console.WriteLine(e.State);
//    }

//    contexto.SaveChanges();

//    Console.WriteLine("=================");
//    produtos = contexto.Produtos.ToList();
//    foreach (var p in produtos)
//    {
//        Console.WriteLine(p);
//    }

//    Console.ReadLine();
//}