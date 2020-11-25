using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace concorrencia_sinc
{
    public class Client
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public double Money { get; private set; }

        private Mutex mutex = new Mutex(false, "Sync");

        //Lista responsável por armazenar os IDs dos threads que solicitam o acesso a operação em questão
        private List<int> currentThreads = new List<int>();

        public Client(int id, string name, double money)
        {
            ID = id;
            Name = name;
            Money = money;
        }

        public void Loot(double amount)
        {
            try
            {
                Util.ConsoleMessage(string.Format("Thread ({0}) quer sacar dinheiro! (ID Cliente: {1})", Thread.CurrentThread.ManagedThreadId, ID), ConsoleColor.Magenta);
                currentThreads.Add(Thread.CurrentThread.ManagedThreadId);
                mutex.WaitOne();
                Util.ConsoleMessage(string.Format("Thread ({0}) agora pode sacar dinheiro! Quantidade solicitada: {1}", Thread.CurrentThread.ManagedThreadId, amount), ConsoleColor.Green);
                if ((Money - amount) >= 0)
                {
                    Money -= amount;
                }
                else
                {
                    Util.ConsoleMessage("Erro: Operação cancelada devido o saldo insuficiente!", ConsoleColor.Red);
                }
            }
            finally
            {
                Util.ConsoleMessage(string.Format("Thread ({0}) finalizou o saque!", Thread.CurrentThread.ManagedThreadId), ConsoleColor.Blue);
                currentThreads.Remove(Thread.CurrentThread.ManagedThreadId);
                mutex.ReleaseMutex();
            }

        }

        public void Deposit(double amount)
        {
            try
            {
                Util.ConsoleMessage(string.Format("Thread ({0}) quer depositar dinheiro! (ID Cliente: {1})", Thread.CurrentThread.ManagedThreadId, ID), ConsoleColor.Magenta);
                currentThreads.Add(Thread.CurrentThread.ManagedThreadId);
                mutex.WaitOne();
                Util.ConsoleMessage(string.Format("Thread ({0}) agora pode depositar dinheiro! Quantidade depositada: {1}", Thread.CurrentThread.ManagedThreadId, amount), ConsoleColor.Green);
                Money += amount;
            }
            finally
            {
                Util.ConsoleMessage(string.Format("Thread ({0}) finalizou o depósito!", Thread.CurrentThread.ManagedThreadId), ConsoleColor.Blue);
                currentThreads.Remove(Thread.CurrentThread.ManagedThreadId);
                mutex.ReleaseMutex();
            }
        }

        public void ShowMoney() 
        {
            try
            {
                Util.ConsoleMessage(string.Format("Thread ({0}) quer mostrar o saldo! (ID Cliente: {1})", Thread.CurrentThread.ManagedThreadId, ID), ConsoleColor.Magenta);
                currentThreads.Add(Thread.CurrentThread.ManagedThreadId);
                mutex.WaitOne();
                Util.ConsoleMessage(string.Format("Thread ({0}) agora pode mostrar o saldo!", Thread.CurrentThread.ManagedThreadId), ConsoleColor.Green);
                Console.WriteLine("###################################################################");
                Console.WriteLine(string.Format("[{0}] - {1}", ID, Name));
                Console.WriteLine("------------------");
                Console.WriteLine("Saldo: " + Money);
                Console.WriteLine("###################################################################");
                Console.WriteLine();
            }
            finally
            {
                Util.ConsoleMessage(string.Format("Thread ({0}) finalizou a exibição do saldo!", Thread.CurrentThread.ManagedThreadId), ConsoleColor.Blue);
                currentThreads.Remove(Thread.CurrentThread.ManagedThreadId);
                mutex.ReleaseMutex();
            }
        }

        public static void ShowClientsInfo(List<Client> clients)
        {
            foreach(Client client in clients)
            {
                Console.WriteLine("###################################################################");
                Console.WriteLine(string.Format("[{0}] - {1}", client.ID, client.Name));
                Console.WriteLine("------------------");
                Console.WriteLine("Saldo: " + client.Money);
                Console.WriteLine("###################################################################");
                Console.WriteLine();
            }
        }
    }
}
