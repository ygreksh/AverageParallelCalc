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
            for (int i = 0; i < limit1; i++)
            {
                array1[i] = random1.Next(limit1);
            }

            for (int i = 0; i < limit1; i++)
            {
                sum1 += array1[i];
            }
            //
            for (int i = 0; i < limit2; i++)
            {
                array2[i] = random2.Next(limit2);
            }

            for (int i = 0; i < limit2; i++)
            {
                sum2 += array2[i];
            }
            avg1 = sum1 / limit1;
            avg2 = sum2 / limit2;
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
                
                for (int i = 0; i < limit1; i++)
                {
                    array1[i] = random1.Next(limit1);
                }

                for (int i = 0; i < limit1; i++)
                {
                    sum1 += array1[i];
                }
                stopwatch2.Stop();
                avg1 = sum1 / limit1;
                Console.WriteLine("Паралельное вычисление");
                Console.WriteLine($"Среднее из {limit1}: {avg1}. Вычмслено за {stopwatch2.ElapsedMilliseconds}");
            });
            //массив array2 [100М]
            ThreadPool.QueueUserWorkItem(_ =>
            {
                stopwatch3.Start();
                for (int i = 0; i < limit2; i++)
                {
                    array2[i] = random2.Next(limit2);
                }

                for (int i = 0; i < limit2; i++)
                {
                    sum2 += array2[i];
                } 
                stopwatch3.Stop();
                avg2 = sum2 / limit2;
                Console.WriteLine("Паралельное вычисление");
                Console.WriteLine($"Среднее из {limit2}: {avg2}. Вычмслено за {stopwatch3.ElapsedMilliseconds}");
            });
            /*
            stopwatch2.Stop();
            Console.WriteLine("Паралельное вычисление");
            Console.WriteLine($"Среднее из {limit1}: {avg1}");
            Console.WriteLine($"Среднее из {limit2}: {avg2}. Вычислено за {stopwatch2.ElapsedMilliseconds}");
            */
            Console.Read();
        }
    }
}