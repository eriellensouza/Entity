using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Alura.Loja.Testes.ConsoleApp.Util
{
    public class SqlLoggerProvider : ILoggerProvider
    {
        public static ILoggerProvider Create()
        {
            return new SqlLoggerProvider();
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (categoryName == typeof(IRelationalCommandBuilderFactory).FullName)
            {
                return new SqlLogger();
            }
            return new NullLogger();
        }

        public void Dispose()
        {
            // Method intentionally left empty.
        }

    }
}
