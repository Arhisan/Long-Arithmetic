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
        static void Main(string[] args)
        {
            StaticManualTest(8, 5);
            Read();
        }

        static void StaticManualTest(LongInt n1, LongInt n2)
        {
            WriteLine($"n1 = {n1}, n2 = {n2}");
            WriteLine($"n1 + n2 = {n1} + {n2} = {n1 + n2}");
            WriteLine($"n1 - n2 = {n1} - {n2} = {n1 - n2}");
            WriteLine($"n1 * n2 = {n1} * {n2} = {n1 * n2}");
            WriteLine($"n1 / n2 = {n1} / {n2} = {n1 / n2}");
            WriteLine($"n1 % n2 = {n1} % {n2} = {n1 % n2}");
        }
    }

    class LongInt
    {
        private BitArray Bits;

        public LongInt(long value=0) => SetValue(value);
        
        public void SetValue(long val)
        {
            const int mask = 1;
            string binarystr = "";
            while (val > 0)
            {
                binarystr = (val & mask) + binarystr;
                val = val >> 1;
            }
            Bits = new BitArray(binarystr.Length);
            for (int i = 0; i < binarystr.Length; i++)
            {
                Bits[i] = binarystr[i] == '1';
            }
        }

        public static LongInt operator +(LongInt a, LongInt b)
        {
            return a;
        }

        public static LongInt operator -(LongInt a, LongInt b)
        {
            return a;
        }
        public static LongInt operator *(LongInt a, LongInt b)
        {
            return a;
        }
        public static LongInt operator /(LongInt a, LongInt b)
        {
            return a;
        }
        public static LongInt operator %(LongInt a, LongInt b)
        {
            return a;
        }

        public static implicit operator LongInt(int v) => new LongInt(v);

        public override string ToString()
        {
            BigInteger sum = 0;

            for (int i = Bits.Length - 1, exp = 0; i >= 0; i--, exp++)
            {
                sum += Bits[i] ? BigInteger.Pow(2, exp) : 0;
            }
            return sum.ToString();
        }
    }
}
