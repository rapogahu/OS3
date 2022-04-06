using System;
using System.Collections.Concurrent;

class Consumer
{
    Thread thread;

    public Consumer()
    {
        thread = new Thread(Consume);
        thread.Start();
    }

    void Consume()
    {
    Consuming:
        {
            if (Program.itemQueue.Count == 0)
            {
                goto Consuming;
            }
            else
            {
                lock (Program.itemQueue)
                {
                    Program.itemQueue.Dequeue();
                }
            }
        }
    }
}

class Producer
{
    Thread thread;

    public Producer()
    {
        thread = new Thread(Produce);
        thread.Start();
    }
    void Produce()
    {
    Producing:
        {
            if (Program.itemQueue.Count >= 100)
            {
                goto Producing;
            }
            else
            {
                lock (Program.itemQueue)
                {
                    Random rnd = new Random();
                    int s = rnd.Next(0, 100);

                    Program.itemQueue.Enqueue(s);
                }
            }
        } 
    }
}
class Program
{
    static public Queue<int> itemQueue = new Queue<int>();

    static public void Main()
    {
        Producer producer1 = new Producer();
        Producer producer2 = new Producer();
        Producer producer3 = new Producer();

        Consumer consumer1 = new Consumer();
        Consumer consumer2 = new Consumer();

        while (true)
        {
            lock (itemQueue)
            {
                Console.Clear();
                foreach (int i in itemQueue)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
                Console.WriteLine("\n\n q - остановить производителей");
            }

            ConsoleKey key = Console.ReadKey().Key;
            if (key == ConsoleKey.Q)
            {
                Environment.Exit(0);
            }
        }
    }
}