using System;

namespace Integrador.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gerador CNAB banco Eximia");

            var repositorioBoletos = new RepositorioBoletos();
            var repositorioBeneficiario = new RepositorioBeneficiarios();
            var beneficiario = repositorioBeneficiario.Beneficiario;
            var boletos = repositorioBoletos.RecuperarTodos();

            new GeraCNAB400().Gerar(beneficiario, boletos);
        }
    }
}