using BlogEdison.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEdison.DB.Classes
{
    public class TagPost : ClasseBase
    {
        public string IdTag { get; set; }
        public int IdPost { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual Post Post { get; set; } 
    }
}
