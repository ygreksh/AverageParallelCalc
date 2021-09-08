using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AverageParallelCalc
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int limit1 = 10000000;
            int limit2 = 100000000;
            long avg1serial;
            long avg2serial;
            long avg1parallel;
            long avg2parallel;
            Stopwatch stopwatch1 = new Stopwatch();
            Stopwatch stopwatch2 = new Stopwatch();
            
            //Генерация массивов
            int[] array1 = GenerateRandomArray(limit1);
            int[] array2 = GenerateRandomArray(limit2);
            
            // последовательное вычисление
            stopwatch1.Start();
            avg1serial = CalcArrayAverage(array1);
            avg2serial = CalcArrayAverage(array2);
            stopwatch1.Stop();
            
            //параллельное вычисление
            stopwatch2.Start();
            avg1parallel = CalcArrayAverageParallel(array1);
            avg2parallel = CalcArrayAverageParallel(array2);
            stopwatch2.Stop();
            
            Console.WriteLine("Последовательное вычисление");
            Console.WriteLine($"Среднее из {limit1}: {avg1serial}");
            Console.WriteLine($"Среднее из {limit2}: {avg2serial}. Вычислено за {stopwatch1.ElapsedMilliseconds} мс");
            
            Console.WriteLine("Паралельное вычисление");
            Console.WriteLine($"Среднее из {limit1}: {avg1parallel}");
            Console.WriteLine($"Среднее из {limit2}: {avg2parallel}. Вычислено за {stopwatch2.ElapsedMilliseconds} мс");

            Console.WriteLine("Финиш!");
        }

        public static long CalcArrayAverage(int[] array)
        {
            long sum = 0;
            long size = array.Length;
            for (int i = 0; i < size; i++)
            {
                sum += array[i];
            }

            return sum / size;
        }
        public static long CalcArrayAverageParallel(int[] array)
        {
            long sum = 0;
            object sum_locker = new object();
            //long sum1 = 0;
            //long sum2 = 0;
            long size = array.Length;
            Parallel.For(0, size, i =>
            {
                lock (sum_locker)
                {
                    sum += array[i];
                }
                //Console.WriteLine($"array[{i}] = {array[i]}, sum = {sum}");
            }) ;
            /*
            Parallel.For(size/2-1, size, i =>
            {
                sum2 += array[i];
            }) ;
            */
            //Console.WriteLine($"Сумма из {size}: {sum1+sum2}");
            return sum / size;
        }
        

        public static int[] GenerateRandomArray(int size)
        {
            int[] array = new int[size]; 
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(size);
            }

            return array;
        }
    }
}