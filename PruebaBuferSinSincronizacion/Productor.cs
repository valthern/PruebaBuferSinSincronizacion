using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaBuferSinSincronizacion
{
    // El método Producir de la clase Productor controla un subproceso que
    // almacena valores de 1 al 10 en ubicacionCompartida
    public class Productor
    {
        private IBufer ubicacionCompartida;
        private Random tiempoInactividadAleatorio;

        public Productor(IBufer compartido,Random aleatorio)
        {
            ubicacionCompartida = compartido;
            tiempoInactividadAleatorio = aleatorio;
        }

        // Almacena valores del 1 al 10 en el objeto ubicacionCompartida
        public void Producir()
        {
            // Duerme durante intervalo aleatorio de hasta 3000 milisegundos
            // Después establece la propiedad Bufer de ubicacionCompartida
            for (int cuenta = 1; cuenta <= 10; cuenta++)
            {
                Thread.Sleep(tiempoInactividadAleatorio.Next(1, 3001));
                ubicacionCompartida.Bufer = cuenta;
            }

            Console.WriteLine("{0} terminó de producir.\nTerminado {0}", Thread.CurrentThread.Name);
        }
    }
}
