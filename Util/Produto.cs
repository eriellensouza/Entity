namespace Loja.Testes.ConsoleApp.Util
{
    public  class Produto
    {
        public int Id { get; internal set; }
        public string Nome { get; internal set; }
        public string Categoria { get; internal set; }
        public double Preco { get; internal set; }

        public override string ToString()
        {
            return $"Produto: {this.Id}, {this.Nome}, {this.Categoria}, {this.Preco}";
        }
    }
}