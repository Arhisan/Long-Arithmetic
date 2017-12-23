using System;
using System.Collections;

namespace Long_Arithmetic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.Read();
        }

    }

    class LongInt
    {
        private BitArray Bits;

        public LongInt(int order = 1)
        {
            Bits = new BitArray(order);
        }

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

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
