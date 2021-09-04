using System;
using System.Diagnostics;
using System.Threading;

namespace AverageParallelCalc
{
    class Program
    {
        static AutoResetEvent waitHandler1 = new AutoResetEvent(true);
        static AutoResetEvent waitHandler2 = new AutoResetEvent(true);
        static WaitHandle[] waitHandles = new[] {waitHandler1,waitHandler2 };
        
        static void Main(string[] args)
        {
            int limit1 = 10000000;
            int limit2 = 100000000;
            long avg1;
            long avg2;
            Stopwatch stopwatch1 = new Stopwatch();
            Stopwatch stopwatch2 = new Stopwatch();
            
            // последовательное вычисление
            stopwatch1.Start();
            avg1 = CalcArrayAverage(limit1);
            avg2 = CalcArrayAverage(limit2);
            stopwatch1.Stop();
            Console.WriteLine("Последовательное вычисление");
            Console.WriteLine($"Среднее из {limit1}: {avg1}");
            Console.WriteLine($"Среднее из {limit2}: {avg2}. Вычислено за {stopwatch1.ElapsedMilliseconds} мс");
            
            //параллельное вычисление
            Console.WriteLine("Паралельное вычисление");
            //массив array2 [10М]
            ThreadPool.QueueUserWorkItem(_ =>
            {
                stopwatch2.Start();
                waitHandler1.WaitOne();
                avg1 = CalcArrayAverage(limit1);
                Console.WriteLine($"Среднее из {limit1}: {avg1}");
                waitHandler1.Set();
            });
            //массив array2 [100М]
            ThreadPool.QueueUserWorkItem(_ =>
            {
                waitHandler2.WaitOne();
                avg2 = CalcArrayAverage(limit2); 
                Console.WriteLine($"Среднее из {limit2}: {avg2}");
                waitHandler2.Set();
            });
            AutoResetEvent.WaitAll(waitHandles);
            stopwatch2.Stop();
            Console.WriteLine($"Вычислено за {stopwatch2.ElapsedMilliseconds} мс");
            Console.WriteLine("Финиш!");
        }

        public static long CalcArrayAverage(int size)
        {
            long sum = 0;
            int[] array = new int[size]; 
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(size);
                sum += array[i];
            }

            return sum / size;
        }
    }
}