using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaBuferSinSincronizacion
{
    // El método Consumir de la clase Consumidor controla un subproceso que
    // itera 10 veces y lee un valor de ubicacionCompartida
    public class Consumidor
    {
        private IBufer ubicacionCompartida;
        private Random tiempoInactividadAleatorio;

        public Consumidor(IBufer compartido,Random aleatorio)
        {
            ubicacionCompartida = compartido;
            tiempoInactividadAleatorio = aleatorio;
        }

        // Lee el valor de ubicacionCompartida diez veces
        public void Consumir()
        {
            int suma = 0;

            // Duerme durante intervalo aleatorio de hasta 3000 milisegundos, después
            // suma el valor de la propiedad Bufer de ubicacionCompartida a suma
            for (int cuenta = 1; cuenta <= 10; cuenta++)
            {
                Thread.Sleep(tiempoInactividadAleatorio.Next(1, 3001));
                suma += ubicacionCompartida.Bufer;
            }

            Console.WriteLine("{0} leyó valores para un total de: {1}.\nTerminado {0}.", Thread.CurrentThread.Name, suma);
        }
    }
}
