using System;
using System.Collections.Generic;
using System.Text;

namespace Recorridos.Modelo
{
    public class Parte
    {
        public int ID { get; set; }
        public String Nombre { get; set; }

        public virtual List<ZonaConceptoParte> ZonaConceptoPartes { get; set; }
    }

    public class ParteDTO
    {
        public int ID { get; set; }
        public String Nombre { get; set; }
        public int ZonaConceptoParteID { get; set; }
    }
}
