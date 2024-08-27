using System;
using System.Collections.Generic;
using System.Text;

namespace Recorridos.Modelo
{
    public class CTipoUsuario
    {
        public int ID { get; set; }
        public String Nombre { get; set; }
        public int Nivel { get; set; }

        public virtual List<Usuario> Usuarios { get; set; }
    }
}
