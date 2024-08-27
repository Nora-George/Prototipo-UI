using System;
using System.Collections.Generic;
using System.Text;

namespace Recorridos.Modelo
{
    public class Recorrido
    {
        public int ID { get; set; }
        public int ZonaConceptoParteID { get; set; }
        //public virtual ZonaConceptoParte ZonaConceptoParte { get; set; }
        public int UsuarioID { get; set; }
        //public virtual Usuario Usuario { get; set; }
        public int PisoID { get; set; }
        //public virtual Piso Piso { get; set; }
        public double Fecha { get; set; }
        public String observaciones { get; set; }
        public String Estado { get; set; }
    }
    public class RecorridoDTO
    {
        public int ID { get; set; }
        public int ZonaConceptoParteID { get; set; }
        public int ZonaID { get; set; }
        public String NombreZona { get; set; }
        public int ConceptoID { get; set; }
        public String NombreConcepto { get; set; }
        public int ParteID { get; set; }
        public String NombreParte { get; set; }
        public int UsuarioID { get; set; }
        public String NombreUsuario { get; set; }
        public String CorreoElectronico { get; set; }
        public int PisoID { get; set; }
        public String NombrePiso { get; set; }
        public double Fecha { get; set; }
        public String Estado { get; set; }
        public String Observaciones { get; set; }

        public String FechaString { get; set; }
    }

    public class RecorridoZonaFecha
    {
        public int ZonaID { get; set; }
        public double FechaInicial { get; set; }
        public double FechaFinal { get; set; }
    }
}
