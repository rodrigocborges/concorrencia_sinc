using System;
using System.Collections.Generic;
using System.Threading;

namespace concorrencia_sinc
{
    class Program
    {
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
                int operationToDo = new Random().Next(0, 3);
                if (operationToDo == 0)
                {
                    threads[i] = new Thread(() => client.Loot(new Random().Next(1, 350)));
                    threads[i].Start();
                }
                else if (operationToDo == 1)
                {
                    threads[i] = new Thread(() => client.Deposit(new Random().Next(1, 9999)));
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
