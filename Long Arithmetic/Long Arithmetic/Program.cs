using System;
using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using static System.Console;
namespace Long_Arithmetic
{
    class Program
    {
        private static LongIntBin ooo = 0;
        static void Main(string[] args)
        {
            StaticManualTest(8, 5);
            // StaticManualTest(5, 8);

            //  StaticManualTest(100, 6);
            //   StaticManualTest(3, 3);

            //  for (int i = 1; i < 100; i *= 10)
            //  {
            //      PerformanceTest(i);
            //  }
            //  WriteLine(ooo);
            Read();
        }
        
        static void StaticManualTest(LongInt n1, LongInt n2)
        {
            WriteLine("----------");
            WriteLine($"n1 = {n1}, n2 = {n2}");
            WriteLine($"{n1} + {n2} = {n1 + n2}");
            WriteLine($"{n1} - {n2} = {n1 - n2}");
            WriteLine($"{n1} * {n2} = {n1 * n2}");
            WriteLine($"{n1} / {n2} = {n1 / n2}");
            WriteLine($"{n1} % {n2} = {n1 % n2}");
            WriteLine($"{n1} > {n2} = {n1 > n2}");
            WriteLine($"{n1} < {n2} = {n1 < n2}");
            WriteLine($"{n1} >= {n2} = {n1 >= n2}");
            WriteLine($"{n1} <= {n2} = {n1 <= n2}");
            WriteLine($"{n1} == {n2} = {n1 == n2}");
            WriteLine($"{n1} != {n2} = {n1 != n2}");

            LongInt num = 1;
            for (int i = 0; i < 200; i++)
            {
                num *= int.MaxValue;
            }
            WriteLine($"Big Number {num}");
        }
        
        static void StaticManualTestBin(LongIntBin n1, LongIntBin n2)
        {
            WriteLine("----------");
            WriteLine($"n1 = {n1}, n2 = {n2}");
            WriteLine($"{n1} + {n2} = {n1 + n2}");
            WriteLine($"{n1} - {n2} = {n1 - n2}");
            WriteLine($"{n1} * {n2} = {n1 * n2}");
            WriteLine($"{n1} / {n2} = {n1 / n2}");
            WriteLine($"{n1} % {n2} = {n1 % n2}");
            WriteLine($"{n1} > {n2} = {n1 > n2}");
            WriteLine($"{n1} < {n2} = {n1 < n2}");
            WriteLine($"{n1} >= {n2} = {n1 >= n2}");
            WriteLine($"{n1} <= {n2} = {n1 <= n2}");
            WriteLine($"{n1} == {n2} = {n1 == n2}");
            WriteLine($"{n1} != {n2} = {n1 != n2}");

            LongIntBin num = 1;
            for (int i = 0; i < 200; i++)
            {
                num *= int.MaxValue;
            }
            WriteLine($"Big Number {num}");
        }

        static void PerformanceTest(int n)
        {
            WriteLine($"---------   n = {n}");
            DateTime start = DateTime.Now;
            DateTime current = DateTime.Now;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    LongIntBin a = i;
                    LongIntBin b = j;
                    LongIntBin c = a + b;
                }
            }
            Console.WriteLine($"Add time {DateTime.Now - current}");
            current = DateTime.Now;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    LongIntBin a = i;
                    LongIntBin b = j;
                    LongIntBin c = a * b;
                }
            }
            Console.WriteLine($"Mult time {DateTime.Now - current}");
            current = DateTime.Now;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    LongIntBin a = i;
                    LongIntBin b = j;
                    LongIntBin c = a - b;
                    
                }
            }
            Console.WriteLine($"Substr time {DateTime.Now - current}");
            current = DateTime.Now;
            WriteLine($"Total {current - start}");
        }
    } 
}
