using System;
using System.Collections.Generic;
using System.Text;

namespace Recorridos.Modelo
{
    public class Zona
    {
        public int ID { get; set; }
        public String Nombre { get; set; }

        public virtual List<ZonaConceptoParte> ZonaConceptoPartes { get; set; }
    }
    public class ZonaDTO
    {
        public int ID { get; set; }
        public String Nombre { get; set; }
    }
}
