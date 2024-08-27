using System;
using System.Collections.Generic;
using System.Text;

namespace Recorridos
{
    public class Epoch
    {
        public double convertirEpoch(DateTime fecha)
        {
            DateTime fechaInicial = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            double epoch = Math.Round((fecha.ToUniversalTime() - fechaInicial).TotalSeconds, 0);
            return epoch;
        }

        public DateTime convertirFecha(double epoch)
        {
            DateTime fechaInicial = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            fechaInicial = fechaInicial.AddSeconds(epoch).ToLocalTime();

            return fechaInicial;
        }
    }
}
