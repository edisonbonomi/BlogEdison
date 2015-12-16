using BlogEdison.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEdison.DB.Classes
{
    public class Download : ClasseBase
    {
        public string Ip { get; set; }
        public DateTime DataHora { get; set; }
        public int IdArquivo { get; set; }

        public virtual Arquivo Arquivo { get; set; }
    }
}
