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
            lock (Program.itemQueue)
            {
                if (Program.itemQueue.Count == 0)
                {
                    goto Consuming;
                }
                else
                {
                    Program.itemQueue.Dequeue();
                    goto Consuming;
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
            lock (Program.itemQueue)
            {
                if (Program.itemQueue.Count >= 100)
                {
                    goto Producing;
                }
                else
                {
                    Random rnd = new Random();
                    int s = rnd.Next(0, 100);

                    Program.itemQueue.Enqueue(s);
                    goto Producing;
                }
            }
        }
    }
}
class Program
{
    static public Queue<int> itemQueue = new Queue<int>();

    static void printQueue()
    {
    Printing:
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
            Thread.Sleep(500);
            goto Printing;
        }
    }

    static public void Main()
    {
        Producer producer1 = new Producer();
        Producer producer2 = new Producer();
        Producer producer3 = new Producer();

        Consumer consumer1 = new Consumer();
        Consumer consumer2 = new Consumer();

        Thread printThread = new Thread(printQueue);
        printThread.Start();

        while (true)
        {
            ConsoleKey key = Console.ReadKey().Key;
            if (key == ConsoleKey.Q)
            {
                Environment.Exit(0);
            }
        }
    }
}