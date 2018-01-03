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
            StaticManualTest(3, 3);
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
        }
    }

    class LongInt: IComparable
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
            int l = a.Bits.Length;
            int r = b.Bits.Length;
            LongInt res = new LongInt();
            //Lenght of the result BitArray
            int n = Math.Max(l, r) + 1;
            res.Bits = new BitArray(n);
            bool carry = false;

            for (int i = 0; i < n; i++)
            {
                //Automatical 0-filling and out-of-range prevention
                int aval = (i < l ? (a.Bits[i] ? 1 : 0) : 0);
                int bval = (i < r ? (b.Bits[i] ? 1 : 0) : 0);
                int cval = (carry ? 1 : 0);
                
                int value = aval + bval + cval;
                switch (value)
                { 
                    case 0: res.Bits[i] = false; carry = false; break;
                    case 1: res.Bits[i] = true;  carry = false; break;
                    case 2: res.Bits[i] = false; carry = true; break;
                    case 3: res.Bits[i] = true;  carry = true; break;
                }
            }
            return res;
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

        public static bool operator >(LongInt a, LongInt b) => a.CompareTo(b) > 0;
        public static bool operator <(LongInt a, LongInt b) => a.CompareTo(b) < 0;
        public static bool operator >=(LongInt a, LongInt b) => a.CompareTo(b) >= 0;
        public static bool operator <=(LongInt a, LongInt b) => a.CompareTo(b) <= 0;
        public static bool operator ==(LongInt a, LongInt b) => a.CompareTo(b) == 0;
        public static bool operator !=(LongInt a, LongInt b) => a.CompareTo(b) != 0;


        //Convert int to LongInt implicitly
        public static implicit operator LongInt(int v) => new LongInt(v);

        //Transform number to "Human form"
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

        //Naive Multiplication alg - O(n^2)
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

        //IComparable nonstatic implementation
        public int CompareTo(object obj)
        {
            LongInt snd = (LongInt)obj;
            if (Bits.Length > snd.Bits.Length)
            {
                return 1;
            }
            else if (Bits.Length < snd.Bits.Length)
            {
                return -1;
            }
            else
            {
                int maxlenght = Math.Max(Bits.Length, snd.Bits.Length);
                for (int i = maxlenght - 1; i >= 0; i--)
                {
                    int aval = i >= Bits.Length ? 0 : (Bits[i] ? 1 : 0);
                    int bval = i >= snd.Bits.Length ? 0: (snd.Bits[i] ? 1 : 0);
                    if (aval > bval)
                        return 1;
                    else if (aval < bval)
                        return -1;
                }
                return 0;
            }

        }
    }
}
