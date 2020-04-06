using Alura.EntityFramework.Conexao;
using Alura.EntityFramework.Contexto;
using Alura.EntityFramework.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework
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

                var cliente = contexto.Clientes.AsEnumerable().FirstOrDefault();

                Console.WriteLine("Endereço de entrega:\n" + cliente.EnderecoDeEntrega);
            }
        }

        private static void ExibeProdutosDaPromocao()
        {
            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var promocao = contexto
                    .Promocoes
                    .Include("Produtos.Produto")
                    .FirstOrDefault();


                foreach (var item in promocao.Produtos)
                {
                    Console.WriteLine(item.Produto);
                }
            }
        }

        private static void JoinEntidadesRelacionadas()
        {
            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var promocao = contexto
                    .Promocoes
                    .Include(p => p.Produtos)
                    .ThenInclude(pp => pp.Produto)
                    .FirstOrDefault();

                foreach (var item in promocao.Produtos)
                {
                    Console.WriteLine(item.Produto);
                }
            }
        }

        private static void IncluirPromocao()
        {
            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var promocao = new Promocao();
                promocao.Descricao = "Queima Total Janeiro 2021";
                promocao.DataInicio = new DateTime(2021, 1, 1);
                promocao.DataFim = new DateTime(2021, 1, 31);

                var produtos = contexto
                    .Produtos
                    .AsEnumerable()
                    .Where(p => p.Categoria == "Bebidas alcóolicas")
                    .ToList();

                foreach (var item in produtos)
                {
                    promocao.IncluiProduto(item);
                }

                contexto.Promocoes.Add(promocao);

                ExibeEntries(contexto.ChangeTracker.Entries());
                contexto.SaveChanges();

            }
        }

        private static void UmParaUm()
        {
            var fulaninho = new Cliente
            {
                Nome = "Euzinha",
                EnderecoDeEntrega = new Endereco()
                {
                    Cep = 30190051,
                    Logradouro = "Rua dos Goitacazes",
                    Numero = 470,
                    Complemento = 1301,
                    Bairro = "Centro",
                    Cidade = "Belo Horizonte",
                    Estado = "MG",
                    Pais = "Brasil"
                }
            };

            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                //Adicionar
                contexto.Clientes.Add(fulaninho);
                contexto.SaveChanges();

                ExibeEntries(contexto.ChangeTracker.Entries());
                contexto.SaveChanges();
                ExibeEntries(contexto.ChangeTracker.Entries());
            }
        }
        private static void MuitosParaMuitos()
        { 
            var p1 = new Produto()
            { Nome= "Laranja", Categoria = "Hortifruti", PrecoUnitario = 0.50, Unidade = "Kilo"};
            var p2 = new Produto()
            { Nome = "Gin", Categoria = "Bebidas alcóolicas", PrecoUnitario = 80.0, Unidade = "Unidade" };
            var p3 = new Produto()
            { Nome = "Taça", Categoria = "Utensílios Domesticos", PrecoUnitario = 5.0, Unidade = "Unidade" };

            var promocaoPascoa = new Promocao
            {
                Descricao = "Pascoa Feliz!",
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddMonths(3)
            };

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
        private static void MuitosParaUm()
        {
            //compra 6 pães franceses

            var paoFrances = new Produto();
            paoFrances.Nome = "Pão Francês";
            paoFrances.PrecoUnitario = 0.40;
            paoFrances.Unidade = "Unidade";
            paoFrances.Categoria = "Padaria";

            var compra = new Compra();
            compra.Quantidade = 6;
            compra.Produto = paoFrances;
            compra.Preco = paoFrances.PrecoUnitario * compra.Quantidade;


            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                contexto.Compras.Add(compra);
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