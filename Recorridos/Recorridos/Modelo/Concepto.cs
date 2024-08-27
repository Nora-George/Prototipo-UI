using System;
using System.Collections.Generic;
using System.Text;

namespace Recorridos.Modelo
{
    public class Concepto
    {
        public int ID { get; set; }
        public String Nombre { get; set; }

        public virtual List<ZonaConceptoParte> ZonaConceptoPartes { get; set; }
    }

    public class ConceptoDTO
    {
        public int ID { get; set; }
        public String Nombre { get; set; }
    }
}
