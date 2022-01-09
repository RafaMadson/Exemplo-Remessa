using System;
using System.Collections.Generic;
using System.IO;
using Integrador.ConsoleApp;
using Integrador.ConsoleApp.Model;
using Integrador.ConsoleApp.Model.CNAB400Model;
using Xunit;

namespace Integrador.Testes
{
    public class UnitTest1
    {
        [Fact]
        public void DadoBeneficiarioValidoEListaDeBoletos_QuandoGerarArquivoCNAB400_DevoEscreverArquivoFormatado()
        {
            // Ambiente
            var hoje = DateTime.Now;
            var repositorio = new RepositorioBeneficiarios();
            var repositorioBoletos = new RepositorioBoletos();
            var beneficiario = repositorio.Beneficiario;
            IEnumerable<Boleto> boletos = repositorioBoletos.RecuperarTodos();
            var gerador = new GeraCNAB400();

            // Ação
            gerador.Gerar(beneficiario, boletos);

            // Assertiva
            bool arquivoCriado = File.Exists(gerador.ArquivoCNAB);
            Assert.True( arquivoCriado );
        }

        [Fact]
        public void DadoMesValido_QuandoGerarCodigoDoMes_DevoRetornarCodigoCorreto()
        {
            var mes = 10;
            var gerador = new GeraCNAB400();

            var codigo = gerador.RecuperarCodigoDoMes(mes);
            
            Assert.Equal("O", codigo);
        }

        [Fact]
        public void ValidaGeracaoCNAB400Trailer()
        {
            // Ambiente
            CNAB400Trailer trailer = new CNAB400Trailer();

            // Ação
            var Trailer = trailer.fromStringBuilder();

            // Assertiva
            Assert.Equal("\r\n9198721121                                                                                                                                                                                                                                                                                                                                                                                               000001",
                         Trailer.ToString());

        }

        [Fact]
        public void ValidaGeracaoCNAB400Header()
        {
            // Ambiente
            CNAB400Header header = new CNAB400Header();

            // Ação
            var headerBuilder = header.fromStringBuilder();

            // Assertiva
            Assert.Equal("01REMESSA01       COBRANCA2112151450629000174                               987         EXIMIA               0000001                                                                                                                                                                                                                                                                                 0200000001",
                         headerBuilder.ToString());

        }


    }
}