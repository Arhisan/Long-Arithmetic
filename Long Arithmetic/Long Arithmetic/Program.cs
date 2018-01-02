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
            StaticManualTest(100, 6);

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
                binarystr += (val & mask); //+ binarystr;
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
            return 
                Math.Max(a.Bits.Length, b.Bits.Length) > 256
                ? KaratsubaMult(a, b) 
                : NaiveMult(a, b);
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
            for (int i = 0; i < Bits.Length; i++)
            {
                sum += Bits[i] ? BigInteger.Pow(2, i) : 0;
            }
            return sum.ToString();
        }

        //O(n^log(3) ~ n^1.58)
        private static LongInt KaratsubaMult(LongInt a, LongInt b)
        {
            return 0;
        }
    

        private static LongInt NaiveMult(LongInt a, LongInt b)
        {
            int l = a.Bits.Length;
            int r = b.Bits.Length;
            LongInt res = new LongInt();
            res.Bits = new BitArray(l + r);
            bool carry = false;

            for (int i = 0; i < l; i++)
            {
                carry = false;
                for (int j = 0; j < r; j++)
                {
                    bool oldval = res.Bits[i + j];
                    bool result = a.Bits[i] & b.Bits[j];
                    res.Bits[i + j] ^= carry ^ result;
                    carry = (oldval | carry) & (oldval | result) &
                            (carry | result); 
                }
                res.Bits[i + r] ^= carry;
            }
            return res;
        }
    }
}
