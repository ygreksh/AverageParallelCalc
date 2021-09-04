using System;

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
            Console.WriteLine($"Сумма из {limit1}: {sum1}");
            Console.WriteLine($"Среднее из {limit1}: {avg1}");
            Console.WriteLine($"Сумма из {limit2}: {sum2}");
            Console.WriteLine($"Среднее из {limit2}: {avg2}");
        }
    }
}