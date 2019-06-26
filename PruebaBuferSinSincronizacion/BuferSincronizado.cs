using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaBuferSinSincronizacion
{
    public class BuferSincronizado : IBufer
    {
        // Búber compartido por los subprocesos productor y consumidor
        private int bufer = -1;

        // cuentaBuferOcupado mantiene la cuenta de los búferes ocupados
        private int cuentaBuferOcupado = 0;

        public int Bufer
        {
            get
            {
                // Obtiene el bloqueo sobre este objeto
                Monitor.Enter(this);

                // Si no hay datos para leer coloca el subproceso
                // invocado en el estado WaitSleepJoin
                if (cuentaBuferOcupado == 0)
                {
                    Console.WriteLine(Thread.CurrentThread.Name + " trata de leer.");
                    MostrarEstado("Búfer vacío. " + Thread.CurrentThread.Name + " espera.");
                    // Entra al estado WaitSleeJoin
                    Monitor.Wait(this);
                }

                // Indica que el productor puede almacenar otro valor 
                // ya que el consumidor está a punto de extraer un valor del búfer
                --cuentaBuferOcupado;

                MostrarEstado(Thread.CurrentThread.Name + " lee " + bufer);


                // Indica al subproceso en espera (si lo hay) que debe prepararse
                // para ejecutarse (estado Running)
                Monitor.Pulse(this);

                // Obtiene copia de búfer antes de liberar el bloqueo.
                // Es posible que al productor se le pudiera
                // asignar el procesador inmediatamente después de que
                // se libere el monitor y antes de que se ejecute la
                // instrucción de retorno. En este caso, el productor
                // asignaría un nuevo valor al búfer antes de que
                // la instrucción de retorno devuelva el valor al
                // consumidor. Por ende, el consumidor recibirá el
                // nuevo valor. Al hacer una copia del búfer y devolver la copia
                // se asegura que el consumidor reciba el valor apropiado.
                int copiaBufer = bufer;

                // Libera el bloqueo sobre este objeto
                Monitor.Exit(this);

                return copiaBufer;
            }
            set
            {
                // Adquiere el bloqueo para este objeto
                Monitor.Enter(this);

                // Si no hay ubicaciones vacías coloca el subproceso
                // invocador en el estado WaitSleepJoin
                if (cuentaBuferOcupado == 1)
                {
                    Console.WriteLine(Thread.CurrentThread.Name + " trata de escribir.");
                    MostrarEstado("Búfer lleno. " + Thread.CurrentThread.Name + " espera.");
                    // Entra al estado WaitSleepJoin
                    Monitor.Wait(this);
                }

                // Establece nuevo valor de búfer
                bufer = value;

                // Indica que el consumidor puede extraer otro valor
                // ya que el productor acaba de almacenar un valor en el búfer
                ++cuentaBuferOcupado;

                MostrarEstado(Thread.CurrentThread.Name + " escribe " + bufer);

                // Indica al subproceso en espera (si lo hay) que
                // se prepare para ejecutarse (estado Running)
                Monitor.Pulse(this);

                // Libera el bloqueo sobre este objeto
                Monitor.Exit(this);
            }
        }

        // Muestra la operación actual y el estado del búfer
        public void MostrarEstado(string operacion) => Console.WriteLine($"{operacion,-35}{bufer,-9}{cuentaBuferOcupado}");
    }
}
