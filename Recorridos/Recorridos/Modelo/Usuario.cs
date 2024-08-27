using System;
using System.Collections.Generic;
using System.Text;

namespace Recorridos.Modelo
{
    public class Usuario
    {
        public int ID { get; set; }
        public String Nombre { get; set; }
        public String CorreoElectronico { get; set; }
        public String Contraseña { get; set; }
        public bool Estatus { get; set; }

        public int CTipoUsuarioID { get; set; }
        public virtual CTipoUsuario CTipoUsuario { get; set; }
    }

    public class UsuarioDTO
    {
        public int ID { get; set; }
        public String Nombre { get; set; }
        public String CorreoElectronico { get; set; }
        public String Contraseña { get; set; }
        public bool Estatus { get; set; }
        public int NivelTipoUsuario { get; set; }
        public String TipoUsuario { get; set; }
    }

    public class InicioSesion
    {
        public String CorreoElectronico { get; set; }
        public String Contraseña { get; set; }
    }
}
