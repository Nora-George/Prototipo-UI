using System;
using System.Collections.Generic;
using System.Text;

namespace Recorridos.Modelo
{
    public class ZonaConceptoParte
    {
        public int ID { get; set; }
        public int ZonaID { get; set; }
        public virtual Zona Zona { get; set; }
        public int ConceptoID { get; set; }
        public virtual Concepto Concepto { get; set; }
        public int ParteID { get; set; }
        public virtual Parte Parte { get; set; }

        //public virtual List<Recorrido> Recorridos { get; set; }
    }

    public class ZonaConceptoParteDTO
    {
        public int ID { get; set; }
        public int ZonaID { get; set; }
        public String NombreZona { get; set; }
        public int ConceptoID { get; set; }
        public String NombreConcepto { get; set; }
        public int ParteID { get; set; }
        public String NombreParte { get; set; }
    }
}
