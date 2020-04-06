namespace Alura.EntityFramework.Util
{
    public class Endereco
    {
        public int Cep { get; internal set; }
        public string Logradouro { get; internal set; }
        public int Numero { get; internal set; }
        public int Complemento { get; internal set; }
        public string Bairro { get; internal set; }
        public string Cidade { get; internal set; }
        public string Estado { get; internal set; }
        public string Pais { get; internal set; }
        public Cliente Cliente { get; set; }
    }
}