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

        private bool activeMutex = true;
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
                if (activeMutex)
                {
                    Util.ConsoleMessage(string.Format("Thread ({0}) quer sacar dinheiro! (ID Cliente: {1})", Thread.CurrentThread.ManagedThreadId, ID), ConsoleColor.Magenta);
                    currentThreads.Add(Thread.CurrentThread.ManagedThreadId);
                    mutex.WaitOne();
                    Util.ConsoleMessage(string.Format("Thread ({0}) agora pode sacar dinheiro! Quantidade solicitada: R$ {1}", Thread.CurrentThread.ManagedThreadId, amount.ToString("N2")), ConsoleColor.Green);
                }
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
                if (activeMutex)
                {
                    Util.ConsoleMessage(string.Format("Thread ({0}) finalizou o saque!", Thread.CurrentThread.ManagedThreadId), ConsoleColor.Blue);
                    currentThreads.Remove(Thread.CurrentThread.ManagedThreadId);
                    mutex.ReleaseMutex();
                }
            }

        }

        public void Deposit(double amount)
        {
            try
            {
                if (activeMutex)
                {
                    Util.ConsoleMessage(string.Format("Thread ({0}) quer depositar dinheiro! (ID Cliente: {1})", Thread.CurrentThread.ManagedThreadId, ID), ConsoleColor.Magenta);
                    currentThreads.Add(Thread.CurrentThread.ManagedThreadId);
                    mutex.WaitOne();
                    Util.ConsoleMessage(string.Format("Thread ({0}) agora pode depositar dinheiro! Quantidade depositada: R$ {1}", Thread.CurrentThread.ManagedThreadId, amount.ToString("N2")), ConsoleColor.Green);
                }
                Money += amount;
            }
            finally
            {
                if (activeMutex)
                {
                    Util.ConsoleMessage(string.Format("Thread ({0}) finalizou o depósito!", Thread.CurrentThread.ManagedThreadId), ConsoleColor.Blue);
                    currentThreads.Remove(Thread.CurrentThread.ManagedThreadId);
                    mutex.ReleaseMutex();
                }
            }
        }

        public void ShowMoney() 
        {
            try
            {
                if (activeMutex)
                {
                    Util.ConsoleMessage(string.Format("Thread ({0}) quer mostrar o saldo! (ID Cliente: {1})", Thread.CurrentThread.ManagedThreadId, ID), ConsoleColor.Magenta);
                    currentThreads.Add(Thread.CurrentThread.ManagedThreadId);
                    mutex.WaitOne();
                    Util.ConsoleMessage(string.Format("Thread ({0}) agora pode mostrar o saldo!", Thread.CurrentThread.ManagedThreadId), ConsoleColor.Green);
                }
                Console.WriteLine("###################################################################");
                Console.WriteLine(string.Format("[{0}] {1}", ID, Name));
                Console.WriteLine("------------------");
                Console.WriteLine("Saldo: R$ " + Money.ToString("N2"));
                Console.WriteLine("###################################################################");
                Console.WriteLine();
            }
            finally
            {
                if (activeMutex)
                {
                    Util.ConsoleMessage(string.Format("Thread ({0}) finalizou a exibição do saldo!", Thread.CurrentThread.ManagedThreadId), ConsoleColor.Blue);
                    currentThreads.Remove(Thread.CurrentThread.ManagedThreadId);
                    mutex.ReleaseMutex();
                }
            }
        }
    }
}
