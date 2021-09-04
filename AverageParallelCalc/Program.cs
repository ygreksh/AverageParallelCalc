using System;
using System.Diagnostics;
using System.Threading;

namespace AverageParallelCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            int limit1 = 10000000;
            int limit2 = 100000000;
            int[] array1 = new int[limit1];
            int[] array2 = new int[limit2];
            Random random1 = new Random();
            Random random2 = new Random();
            long sum1 = 0;
            long sum2 = 0;
            long avg1;
            long avg2;
            Stopwatch stopwatch1 = new Stopwatch();
            Stopwatch stopwatch2 = new Stopwatch();
            Stopwatch stopwatch3 = new Stopwatch();
            // последовательное вычисление
            stopwatch1.Start();
            avg1 = CalcArrayAverage(limit1);
            //
            avg2 = CalcArrayAverage(limit2);
            stopwatch1.Stop();
            Console.WriteLine("Последовательное вычисление");
            Console.WriteLine($"Среднее из {limit1}: {avg1}");
            Console.WriteLine($"Среднее из {limit2}: {avg2}. Вычислено за {stopwatch1.ElapsedMilliseconds}");
            stopwatch1.Reset();
            
            //параллельное вычисление
            sum1 = 0;
            sum2 = 0;
            //массив array2 [10М]
            ThreadPool.QueueUserWorkItem(_ =>
            {
                stopwatch2.Start();
                avg1 = CalcArrayAverage(limit1);
                stopwatch2.Stop();
                Console.WriteLine("Паралельное вычисление");
                Console.WriteLine($"Среднее из {limit1}: {avg1}. Вычмслено за {stopwatch2.ElapsedMilliseconds}");
            });
            //массив array2 [100М]
            ThreadPool.QueueUserWorkItem(_ =>
            {
                stopwatch3.Start();
                avg2 = CalcArrayAverage(limit2); 
                stopwatch3.Stop();
                Console.WriteLine("Паралельное вычисление");
                Console.WriteLine($"Среднее из {limit2}: {avg2}. Вычмслено за {stopwatch3.ElapsedMilliseconds}");
            });
            Console.ReadLine();
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