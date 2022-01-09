using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Integrador.ConsoleApp.Extensions;
using Integrador.ConsoleApp.Model;
using Integrador.ConsoleApp.Model.CNAB400Model;

namespace Integrador.ConsoleApp
{
    public class GeraCNAB400
    {
        public string ArquivoCNAB { get; private set; }

        public Beneficiario Beneficiario { get; private set; }

        public StringBuilder Header { get;  private set; }
        public StringBuilder Trailer { get; private set; }
        public IEnumerable<Boleto> Boletos { get; private set;}

        public GeraCNAB400()
        {
            
        }

        public string RecuperarCodigoDoMes(in int month)
        {
            return month switch
            {
                1 => "1",
                2 => "2",
                3 => "3",
                4 => "4",
                5 => "5",
                6 => "6",
                7 => "7",
                8 => "8",
                9 => "9",
                10 => "O",
                11 => "N",
                12 => "D",
                _ => ""
            };
        }
        
        public void Gerar(Beneficiario beneficiario, IEnumerable<Boleto> boletos)
        {
            if (beneficiario == null)
                throw new ArgumentException("Beneficiário não informado");

            if (boletos == null || boletos.Count() <= 0)
                throw new ArgumentException("Nenhum boleto informado");

            Beneficiario = beneficiario;
            Boletos = boletos;

            ArquivoCNAB = Beneficiario.Codigo.FormatCNAB(5, '0');


            ArquivoCNAB += RecuperarCodigoDoMes(DateTime.Now.Month);


            ArquivoCNAB += DateTime.Now.Day + ".CRM";
            
            if (File.Exists(ArquivoCNAB))
                File.Delete(ArquivoCNAB);

            GerarHeader();
            GerarDetalhe();
            GerarTrailer();
        }
        void GerarHeader()
        {
            CNAB400Header header = new CNAB400Header();
            this.Header = header.fromStringBuilder();
            File.AppendAllText(ArquivoCNAB, this.Header.ToString());
        }


        void GerarDetalhe()
        {
            CNAB400Detalhe detalhes = new CNAB400Detalhe();
            File.AppendAllText(ArquivoCNAB, detalhes.fromStringBuilder(Boletos).ToString());
        }
        void GerarTrailer()
        {
            CNAB400Trailer trailer = new CNAB400Trailer();
            this.Trailer = trailer.fromStringBuilder();
            File.AppendAllText(ArquivoCNAB, this.Trailer.ToString());

        }
    }
}