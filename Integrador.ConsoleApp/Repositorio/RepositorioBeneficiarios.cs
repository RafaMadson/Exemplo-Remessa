using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ConsoleApp
{
    public sealed class RepositorioBeneficiarios
    {
        public Beneficiario Beneficiario { get; set; }

        
        public RepositorioBeneficiarios()
        {
            Beneficiario = new Beneficiario("987", "8276", "21121", "51450629000174");
        }

        public Beneficiario RecuperarBeneficiario()
            => new Beneficiario("987", "8276", "21121", "51450629000174");
    }
}
