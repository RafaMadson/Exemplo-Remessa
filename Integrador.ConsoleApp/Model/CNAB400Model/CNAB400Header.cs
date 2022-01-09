using Integrador.ConsoleApp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ConsoleApp.Model
{
    public sealed class CNAB400Header
    {
        public CNAB400Header(string identificacao_registro, 
                      string identificacao_remessa,
                      string literal_remessa,
                      string codigo_servico_cobranca,
                      string literal_cobranca, 
                      string codigo_beneficiario,
                      string CNPJ_beneficiario,
                      string filler,
                      string numero_do_banco,
                      string banco,
                      string filer,
                      string numero_remessa, 
                      string filer_descricao,
                      string versao_sistema,
                      string numero_sequencial_arquivo


                      )
        {
            this.identificacao_registro    = identificacao_registro;
            this.identificacao_remessa     = identificacao_remessa;
            this.literal_remessa           = literal_remessa;
            this.codigo_servico_cobranca   = codigo_servico_cobranca;
            this.literal_cobranca          = literal_cobranca;
            this.codigo_beneficiario       = codigo_beneficiario;
            this.CNPJ_beneficiario         = CNPJ_beneficiario;
            this.filler                    = filler;
            this.numero_do_banco           = numero_do_banco;
            this.banco                     = banco;
            this.filer                     = filer;
            this.numero_remessa            = numero_remessa;
            this.filer_descricao           = filer_descricao;
            this.versao_sistema            = versao_sistema;
            this.numero_sequencial_arquivo = numero_sequencial_arquivo;
        }

        public CNAB400Header() {
            var repositorio = new RepositorioBeneficiarios();
            var beneficiarioTeste = repositorio.Beneficiario;
            headerPadrao = new CNAB400Header(
                identificacao_registro = "0",
                identificacao_remessa = "1",
                literal_remessa = "REMESSA",
                codigo_servico_cobranca = "01",
                literal_cobranca = "COBRANCA",
                codigo_beneficiario = beneficiarioTeste.Codigo,
                CNPJ_beneficiario = beneficiarioTeste.Cnpj,
                filler = "",
                numero_do_banco = beneficiarioTeste.Banco,
                banco = "EXIMIA",
                filer = "",
                numero_remessa = "1",
                filer_descricao = "",
                versao_sistema = "2.00",
                numero_sequencial_arquivo = "1"
                );
        }

        public StringBuilder fromStringBuilder()
        {
            StringBuilder build = new StringBuilder();
                                                                                 // |-----------|---------|--------------------------------------|
            build.Append(identificacao_registro.FormatCNAB(1));                  // | 001 a 001 | 001     | Identificação do registro *header*   |
            build.Append(identificacao_remessa.FormatCNAB(1));                   // | 002 a 002 | 001     | Identificação do arquivo de remessa  |
            build.Append(literal_remessa.FormatCNAB(7));                         // | 003 a 009 | 007     | Literal remessa                      |
            build.Append(codigo_servico_cobranca.FormatCNAB(2));                 // | 010 a 011 | 002     | Código do serviço de cobrança        |
            build.Append(literal_cobranca.FormatCNAB(15));                       // | 012 a 026 | 015     | Literal cobrança                     |
            build.Append(codigo_beneficiario.FormatCNAB(5, '0'));                // | 027 a 031 | 005     | Código do beneficário                |
            build.Append(CNPJ_beneficiario.FormatCNAB(14, '0'));                 // | 032 a 045 | 014     | CNPF do beneficário                  |
            build.Append(filler.FormatCNAB(31));                                 // | 046 a 076 | 031     | Filler                               |
            build.Append(numero_do_banco.FormatCNAB(3, '0'));                    // | 077 a 079 | 003     | Número do banco                      |
            build.Append(banco.FormatCNAB(15));                                  // | 080 a 094 | 015     | BANCO                                |
            build.Append(filler.FormatCNAB(15));                                 // | 095 a 102 | 008     | Filer                                |
            build.Append(numero_remessa.FormatCNAB(7, '0'));                     // | 111 a 117 | 007     | Número da remessa                    |
            build.Append(filer.FormatCNAB(273));                                 // | 118 a 390 | 273     | Filer                                |
            build.Append(versao_sistema.FormatCNAB(4, '0'));                     // | 391 a 394 | 004     | Versão do sistema                    |
            build.Append(numero_sequencial_arquivo.FormatCNAB(6, '0'));          // | 395 a 400 | 006     | Número sequêncial do arquivo         |

            return build;
        }

        public CNAB400Header headerPadrao;
        public string identificacao_registro { get; }
        public string identificacao_remessa { get; }
        public string literal_remessa { get; }
        public string codigo_servico_cobranca { get; }
        public string literal_cobranca { get; }
        public string codigo_beneficiario { get; }
        public string CNPJ_beneficiario { get; }
        public string filler { get; }
        public string numero_do_banco { get; }
        public string banco { get; }
        public string filer { get; }
        public string numero_remessa { get; }
        public string filer_descricao { get; }
        public string versao_sistema { get; }
        public string numero_sequencial_arquivo { get; }
    }
}
