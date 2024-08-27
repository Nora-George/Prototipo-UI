using System;
using System.Collections.Generic;
using System.Text;

namespace Recorridos.Modelo
{
    public class Piso
    {
        public int ID { get; set; }
        public String Nombre { get; set; }

        public virtual List<Recorrido> Recorridos { get; set; }
    }
    public class PisoDTO
    {
        public int ID { get; set; }
        public String Nombre { get; set; }
    }
}
