using Integrador.ConsoleApp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ConsoleApp.Model.CNAB400Model
{
    public  class CNAB400Trailer
    {
        public CNAB400Trailer(string identificacao_registro,
                      string identificacao_remessa,
                      string numero_eximia,
                      string filer,
                      string codigo_beneficiario,
                      string numero_sequencial_registro
                      )
        {
            this.identificacao_registro = identificacao_registro;
            this.identificacao_remessa = identificacao_remessa;
            this.numero_eximia = numero_eximia;
            this.filer = filer;
            this.codigo_beneficiario = codigo_beneficiario;
            this.numero_sequencial_registro = numero_sequencial_registro;
        }

        public CNAB400Trailer()
        {
            var repositorio = new RepositorioBeneficiarios();
            var beneficiarioTeste = repositorio.Beneficiario;
            trailerPadrao = new CNAB400Trailer(
                identificacao_registro = "9",
                identificacao_remessa = "1",
                numero_eximia = beneficiarioTeste.Banco,
                codigo_beneficiario = beneficiarioTeste.Codigo,
                filer = "",
                numero_sequencial_registro = "1"               
                );
        }
        public StringBuilder fromStringBuilder()
        {
            StringBuilder build = new StringBuilder();                      // | Posição   | Tamanho | Descrição                            |
            build.AppendLine();                                             // |-----------|---------|--------------------------------------|
            build.Append(identificacao_registro.FormatCNAB(1));             // | 001 a 001 | 001     | Identificação do registro *trailer*  |
            build.Append(identificacao_remessa.FormatCNAB(1));              // | 002 a 002 | 001     | Identificação do arquivo de mressa   |
            build.Append(numero_eximia.FormatCNAB(3));                      // | 003 a 005 | 003     | Numero Eximia                        |
            build.Append(codigo_beneficiario.FormatCNAB(5));                // | 006 a 010 | 005     | Codigo do beneficiario               |
            build.Append(filer.FormatCNAB(383));                            // | 011 a 394 | 384     | Filer                                |
            build.Append(numero_sequencial_registro.FormatCNAB(6, '0'));    // | 395 a 400 | 006     | Numero sequencial do registro        |            
            return build;
        }

        public CNAB400Trailer trailerPadrao;
        public string identificacao_registro { get; }
        public string identificacao_remessa { get; }
        public string numero_eximia { get; }
        public string codigo_beneficiario { get; }
        public string filer { get; } 
        public string numero_sequencial_registro  { get; }

    }
}
