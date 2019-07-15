using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaBuferSinSincronizacion
{
    // Esta clase crea los subprocesos productor y consumidor
    class Program
    {
        // Crea los subprocesos productor y consumidor, y los inicia
        static void Main(string[] args)
        {
            // Crea objeto compartido utilizado por lo subprocesos
            //BuferSinSincronizacion compartido = new BuferSinSincronizacion();
            BuferSincronizado compartido = new BuferSincronizado();

            // Objeto aleatorio usado por cada proceso
            Random aleatorio = new Random();

            // Imprime los encabezados de las columnas y el estado inicial del búfer
            Console.WriteLine($"{"Operación",-35}{"Búfer",-9}{"Cuenta ocupado"}\n");
            compartido.MostrarEstado("Estado inicial");

            // Crea objetos Productor y Consumidor
            Productor productor = new Productor(compartido, aleatorio);
            Consumidor consumidor = new Consumidor(compartido, aleatorio);

            // Crea subprocesos para productor y consumidor, y establece delegados
            // para cada subproceso
            Thread subprocesoProductor = new Thread(new ThreadStart(productor.Producir));
            subprocesoProductor.Name = "Productor";

            Thread subprocesoConsumidor = new Thread(new ThreadStart(consumidor.Consumir));
            subprocesoConsumidor.Name = "Consumidor";

            // Inicia cada subproceso
            subprocesoProductor.Start();
            subprocesoConsumidor.Start();

            Console.ReadLine();
        }
    }
}
