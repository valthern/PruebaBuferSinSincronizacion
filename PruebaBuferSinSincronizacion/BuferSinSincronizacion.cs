using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaBuferSinSincronizacion
{
    // Esta clase representa a un solo valor int compartido
    public class BuferSinSincronizacion : IBufer
    {
        // bufer compartido por los subprocesos productor y consumidor
        private int bufer = -1;

        // Propiedad Bufer
        public int Bufer
        {
            get
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} lee {bufer}");
                return bufer;
            }
            set
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} escribe {value}");
                bufer = value;
            }
        }
    }
}
