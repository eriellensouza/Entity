using Alura.Loja.Testes.ConsoleApp.Util;
using Loja.Testes.ConsoleApp.Contexto;
using Loja.Testes.ConsoleApp.Util;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loja.Testes.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new LojaContext())
            {
                //Providor de serviços
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();

                //Pergar um serviço especifico de log
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

                //Para esse loggerFactory quero que seja pego log do entity e colocado aqui
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var produtos = contexto.Produtos.ToList();

                ExibeEntries(contexto.ChangeTracker.Entries());

                var p1 = produtos.Last();
                p1.Nome = "007 - O Espiao Que Me Amava";

                var novoProduto = new Produto()
                {
                    Nome = "Desinfetante",
                    Categoria = "Limpeza",
                    Preco = 2.99
                };
                contexto.Produtos.Add(novoProduto);

                contexto.Produtos.Remove(p1);

                ExibeEntries(contexto.ChangeTracker.Entries());

                contexto.SaveChanges();

                ExibeEntries(contexto.ChangeTracker.Entries());

                var entry = contexto.Entry(novoProduto);
                Console.WriteLine("\n\n" + entry.Entity.ToString() + " - " + entry.State);


                //Console.WriteLine("=================");
                //produtos = contexto.Produtos.ToList();
                //foreach (var p in produtos)
                //{
                //    Console.WriteLine(p);
                //}
            }
            }
        private static void ExibeEntries(IEnumerable<EntityEntry> entries)
        {
            Console.WriteLine("===================");
            foreach (var e in entries)
            {
                Console.WriteLine(e.Entity.ToString() + " - " + e.State);
            }
        }
    }
}

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
