using Alura.Loja.Testes.ConsoleApp;
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
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var produtos = contexto.Produtos.ToList();
                foreach (var p in produtos)
                {
                    Console.WriteLine(p);
                }

                Console.WriteLine("=================");
                foreach (var e in contexto.ChangeTracker.Entries())
                {
                    Console.WriteLine(e.State);
                }

                var p1 = produtos.Last();
                p1.Nome = "007 - O Espiao Que Me Amava";

                Console.WriteLine("=================");
                foreach (var e in contexto.ChangeTracker.Entries())
                {
                    Console.WriteLine(e.State);
                }

                contexto.SaveChanges();

                //Console.WriteLine("=================");
                //produtos = contexto.Produtos.ToList();
                //foreach (var p in produtos)
                //{
                //    Console.WriteLine(p);
                //}
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
