using Loja.Testes.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class MyController
    {
        private readonly LojaContext _context;

        public MyController(LojaContext context)
        {
            _context = context;
        }

    }
}
