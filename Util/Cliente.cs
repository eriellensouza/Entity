﻿namespace Alura.EntityFramework.Util
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Endereco EnderecoDeEntrega { get; set; }
    }
}