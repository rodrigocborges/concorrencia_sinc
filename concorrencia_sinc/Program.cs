using System;
using System.Collections.Generic;
using System.Threading;

namespace concorrencia_sinc
{
    class Program
    {
        /*
         Rodrigo Borges e José Braga
         */

        //É possível definir um cliente ou mais de um para os threads realizarem as operações
        private static List<Client> clients = new List<Client>() {
            new Client(1, "Rodrigo", 2000)
        };
        static void Main(string[] args)
        {
            string inputThreads = Util.ConsoleInOut("Número de threads: ");
            int nThreads = Convert.ToInt32(inputThreads);
            if (nThreads <= 0)
                nThreads = 1;

            Thread[] threads = new Thread[nThreads];

            for(int i = 0; i < nThreads; ++i)
            {
                Client client = clients[new Random().Next(clients.Count)];
                int operationToDo = new Random().Next(0, 10);
                if (operationToDo % 2 == 0)
                {
                    threads[i] = new Thread(() => client.Loot(Util.RandomVal(1.0, 1000.0)));
                    threads[i].Start();
                }
                else if (operationToDo == 1)
                {
                    threads[i] = new Thread(() => client.Deposit(Util.RandomVal(1.0, 9999.9)));
                    threads[i].Start();
                }
                else
                {
                    threads[i] = new Thread(() => client.ShowMoney());
                    threads[i].Start();
                }

                //Responsável por fazer a thread completar antes de finalizar a aplicação
                threads[i].Join();
            }
            Console.ReadKey();
        }
    }
}
