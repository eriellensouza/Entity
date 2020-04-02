using Alura.EntityFramework.Interface;
using Alura.EntityFramework.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Alura.EntityFramework
{
    internal class ProdutoDAO : IDisposable, IProdutoDAO
    {
        private SqlConnection conexao;
        private SqlParameter ParamId { get; set; }
        private SqlParameter ParamCategoria { get; set; }
        private SqlParameter ParamPreco { get; set; }
        private SqlParameter ParamNome { get; set; }

        private SqlCommand _createCommand;

        public ProdutoDAO()
        {
            this.conexao = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=LojaDB;Trusted_Connection=true;");
            this.conexao.Open();

            _createCommand = conexao.CreateCommand();
        }

        public void Adicionar(Produto p)
        {
            try
            {
                _createCommand.CommandText = InsertProdutos;

                ParamNome = new SqlParameter("nome", p.Nome);
                _createCommand.Parameters.Add(ParamNome);

                ParamCategoria = new SqlParameter("categoria", p.Categoria);
                _createCommand.Parameters.Add(ParamCategoria);

                ParamPreco = new SqlParameter("preco", p.PrecoUnitario);
                _createCommand.Parameters.Add(ParamPreco);

                ParamId = new SqlParameter("id", p.Id);
                _createCommand.Parameters.Add(ParamId);

                _createCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new SystemException(e.Message, e);
            }
        }

        public void Atualizar(Produto p)
        {
            try
            {
                _createCommand.CommandText = UpdateProdutos;

                ParamNome = new SqlParameter("nome", p.Nome);
                ParamCategoria = new SqlParameter("categoria", p.Categoria);
                ParamPreco = new SqlParameter("preco", p.PrecoUnitario);
                ParamId = new SqlParameter("id", p.Id);

                _createCommand.Parameters.Add(ParamNome);
                _createCommand.Parameters.Add(ParamCategoria);
                _createCommand.Parameters.Add(ParamPreco);
                _createCommand.Parameters.Add(ParamId);

                _createCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new SystemException(e.Message, e);
            }
        }

        public void Dispose()
        {
            this.conexao.Close();
        }

        public IList<Produto> Produtos()
        {
            var lista = new List<Produto>();

            _createCommand.CommandText = SelecionaProdutos;

            var resultado = _createCommand.ExecuteReader();
            while (resultado.Read())
            {
                Produto p = new Produto();
                p.Id = Convert.ToInt32(resultado["Id"]);
                p.Nome = Convert.ToString(resultado["Nome"]);
                p.Categoria = Convert.ToString(resultado["Categoria"]);
                p.PrecoUnitario = Convert.ToDouble(resultado["Preco"]);
                lista.Add(p);
            }
            resultado.Close();

            return lista;
        }

        public void Remove(Produto p)
        {
            try
            {
                _createCommand.CommandText = DeleteProdutos;

                ParamId = new SqlParameter("id", p.Id);
                _createCommand.Parameters.Add(ParamId);

                _createCommand.ExecuteNonQuery();

            }
            catch (SqlException e)
            {
                throw new SystemException(e.Message, e);
            }
        }

        public void SaveChanges()
        {
            _createCommand.ExecuteNonQuery();
        }

        #region Queries
        private string InsertProdutos = "INSERT INTO Produtos (Nome, Categoria, Preco) VALUES (@nome, @categoria, @preco)";
        private string UpdateProdutos = "UPDATE Produtos SET Nome = @nome, Categoria = @categoria, Preco = @preco WHERE Id = @id";
        private string DeleteProdutos = "DELETE FROM Produtos WHERE Id = @id";
        private string SelecionaProdutos = "SELECT * FROM Produtos";
        #endregion
    }
}
