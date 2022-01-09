using Integrador.ConsoleApp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ConsoleApp.Model
{
    internal class CNAB400Detalhe
    {
        public StringBuilder fromStringBuilder(IEnumerable<Boleto> Boletos)
        {
            StringBuilder build = new StringBuilder();

            int sequencia = 2;
            foreach (var boleto in Boletos)
            {                                                                     // | Posição   | Tamanho | Descrição                            |
                build.AppendLine();                                               // |-----------|---------|--------------------------------------|
                build.Append("1".FormatCNAB(1));                                  // | 001 a 001 | 001     | Identificação do registro detalhe    | 
                build.Append("A".FormatCNAB(1));                                  // | 002 a 002 | 001     | Tipo de cobrança                     | 
                build.Append("A".FormatCNAB(1));                                  // | 003 a 003 | 001     | Tipo de carteira                     | 
                build.Append("A".FormatCNAB(1));                                  // | 004 a 004 | 001     | Tipo de impressão                    | 
                build.Append("".FormatCNAB(12));                                  // | 005 a 016 | 012     | Filer                                | 
                build.Append("A".FormatCNAB(1));                                  // | 017 a 017 | 001     | Tipo de moeda                        | 
                build.Append("A".FormatCNAB(1));                                  // | 018 a 018 | 001     | Tipo de Desconto                     | 
                build.Append("A".FormatCNAB(1));                                  // | 019 a 019 | 001     | Tipo de Juros                        | 
                build.Append("".FormatCNAB(28));                                  // | 020 a 047 | 028     | Filer                                | 
                build.Append(("AA2" + boleto.NossoNumero).FormatCNAB(9));         // | 048 a 056 | 009     | Nosso Numero                         | 
                build.Append("".FormatCNAB(6));                                   // | 057 a 062 | 006     | Filer                                | 
                build.Append(DateTime.Now.ToString("yyyyMMdd").FormatCNAB(8));    // | 063 a 070 | 008     | Data instrução                       | 
                build.Append("".FormatCNAB(1));                                   // | 071 a 071 | 001     | Vazio                                | 
                build.Append("N".FormatCNAB(1));                                  // | 072 a 072 | 001     | Postagem                             | 
                build.Append("".FormatCNAB(1));                                   // | 073 a 073 | 001     | Filer                                | 
                build.Append("B".FormatCNAB(1));                                  // | 074 a 074 | 001     | Emissão boleto                       | 
                build.Append("".FormatCNAB(2));                                   // | 075 a 076 | 002     | Vazio                                | 
                build.Append("".FormatCNAB(2));                                   // | 077 a 078 | 002     | Vazio                                | 
                build.Append("".FormatCNAB(4));                                   // | 079 a 082 | 004     | Filer                                | 
                build.Append(boleto.Desconto.Valor.FormatCNAB(10, '0'));          // | 083 a 092 | 010     | Valor de desconto                    | 
                build.Append("".FormatCNAB(4, '0'));                              // | 093 a 096 | 004     | % multa pagamento em atraso          | 
                build.Append("".FormatCNAB(12));                                  // | 097 a 108 | 012     | Filer                                | 
                build.Append("01".FormatCNAB(2));                                 // | 109 a 110 | 002     | Instrução                            | 
                build.Append(boleto.NumeroDocumento.FormatCNAB(10));              // | 111 a 120 | 010     | Seu número                           | 
                build.Append(boleto.Vencimento.ToString("ddMMyy").FormatCNAB(6)); // | 121 a 126 | 006     | Data de vencimento                   | 
                build.Append(boleto.Valor.FormatCNAB(13, '0'));                   // | 127 a 139 | 013     | Valor                                | 
                build.Append("".FormatCNAB(9));                                   // | 140 a 148 | 009     | Filer                                | 
                build.Append("O".FormatCNAB(1));                                  // | 149 a 149 | 001     | Espécie                              | 
                build.Append("S".FormatCNAB(1));                                  // | 150 a 150 | 001     | Aceite                               | 
                build.Append(DateTime.Now.ToString("ddMMyy").FormatCNAB(6));      // | 151 a 156 | 006     | Data de emissão                      | 
                build.Append("".FormatCNAB(2, '0'));                              // | 157 a 158 | 002     | Protesto                             | 
                build.Append("".FormatCNAB(2, '0'));                              // | 159 a 160 | 002     | Numero de dias protesto              | 
                build.Append(boleto.Juros.Calcular(boleto.Desconto.Valor, 1)
                                         .Value.Valor.FormatCNAB(12));            // | 161 a 173 | 012     | Valor de juros por dia de atraso     | 
                build.Append(boleto.Desconto.ValidoAte
                                         .ToString("ddMMyy").FormatCNAB(6, '0')); // | 174 a 179 | 006     | Data limite de desconto              | 
                build.Append("".FormatCNAB(13, '0'));                             // | 180 a 192 | 013     | Zeros                                | 
                build.Append("".FormatCNAB(13));                                  // | 193 a 205 | 013     | Filer                                | 
                build.Append("".FormatCNAB(13, '0'));                             // | 206 a 218 | 013     | Zeros                                | 
                build.Append(boleto.Pagador.Documento.Tipo ==
                             ETipoDocumento.CPF ? "0" : "2".FormatCNAB(1));       // | 219 a 219 | 001     | Tipo de pessoa do pagador PF ou PJ   | 
                build.Append("".FormatCNAB(1));                                   // | 220 a 220 | 001     | Filer                                | 
                build.Append(boleto.Pagador.Documento.Valor.FormatCNAB(14, '0')); // | 221 a 234 | 014     | CNPJ ou CPF do pagador               | 
                build.Append(boleto.Pagador.Nome.FormatCNAB(40));                 // | 235 a 274 | 040     | Nome do pagador                      | 
                build.Append(boleto.Pagador.Endereco.Rua.FormatCNAB(40));         // | 275 a 314 | 040     | Endereço do pagador                  | 
                build.Append("".FormatCNAB(5, '0'));                              // | 315 a 319 | 005     | Zeros                                | 
                build.Append("".FormatCNAB(6));                                   // | 320 a 235 | 006     | Filer                                | 
                build.Append("".FormatCNAB(1, '0'));                              // | 326 a 326 | 001     | Filer                                | 
                build.Append(boleto.Pagador.Endereco.Cep.FormatCNAB(8, '0'));     // | 327 a 224 | 008     | CEP do pagador                       | 
                build.Append("".FormatCNAB(5, '0'));                              // | 335 a 339 | 005     | Zeros                                | 
                build.Append("".FormatCNAB(14, '0'));                             // | 340 a 353 | 014     | Zeros                                | 
                build.Append("".FormatCNAB(41));                                  // | 354 a 394 | 041     | Deixar em branco                     | 
                build.Append((sequencia++).ToString().FormatCNAB(6, '0'));        // | 395 a 400 | 006     | Numero sequêncial do registro        | 
            }
            return build;
        }
    }
}
