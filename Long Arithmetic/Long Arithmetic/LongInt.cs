using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Long_Arithmetic
{
    public class LongInt
    {
        //The greatest number of "digit"
        private static int rank = 8;
        private List<int> digits = new List<int>{0};
        public LongInt(long value = 0) => SetValue(value);
        public void SetValue(long val)
        {
            long quotient;
            int i = 0;
            quotient = val;
            while (quotient != 0)

            {
                if (i < digits.Count)
                {
                    digits[i++] = (int) (quotient % rank);
                }
                else
                {
                    digits.Add((int)(quotient % rank));
                    i++;
                }
                quotient = quotient / rank;
            }
            /*
            if (i < digits.Count && digits.Count != 1)
            {
                digits.RemoveRange(i,digits.Count-i);
            }*/
            ShrinkToFit();
            /*
            Console.Write(val+": ");
            for (int j = digits.Count - 1; j >= 0; j--)
            {
                Console.Write(digits[j]);
            }
            Console.WriteLine();*/

        }

        //ADDITION
        public static LongInt operator +(LongInt a, LongInt b)
        {
            int l = a.digits.Count;
            int r = b.digits.Count;
            LongInt res = new LongInt();
            //Lenght of the result 
            int length = Math.Max(l, r) + 1;
            res.digits.Clear();
            int carry = 0;
            for (int i = 0; i < length; i++)
            {
                int adigit = i >= a.digits.Count ? 0 : a.digits[i];
                int bdigit = i >= b.digits.Count ? 0 : b.digits[i];
                res.digits.Add(bdigit + adigit + carry); // суммируем последние разряды чисел
                carry = (res.digits[i] / rank); // если есть разряд для переноса, переносим его в следующий разряд
                res.digits[i] %= rank; // если есть разряд для переноса он отсекается
            }
            return res;
        }
        public static LongInt operator -(LongInt a, LongInt b)
        {
            return a + b;
        }
        public static LongInt operator *(LongInt a, LongInt b)
        {
            return a + b;
        }
        public static LongInt operator /(LongInt a, LongInt b)
        {
            return a + b;
        }
        public static LongInt operator %(LongInt a, LongInt b)
        {
            return a + b;
        }

        //Convert int to LongIntBin implicitly
        public static implicit operator LongInt(int v) => new LongInt(v);

        public static bool operator >(LongInt a, LongInt b) => a.CompareTo(b) > 0;
        public static bool operator <(LongInt a, LongInt b) => a.CompareTo(b) < 0;
        public static bool operator >=(LongInt a, LongInt b) => a.CompareTo(b) >= 0;
        public static bool operator <=(LongInt a, LongInt b) => a.CompareTo(b) <= 0;
        public static bool operator ==(LongInt a, LongInt b) => a.CompareTo(b) == 0;
        public static bool operator !=(LongInt a, LongInt b) => a.CompareTo(b) != 0;


        //IComparable nonstatic implementation
        public int CompareTo(object obj)
        {
            LongInt snd = (LongInt)obj;
            if (digits.Count> snd.digits.Count)
            {
                return 1;
            }
            else if (digits.Count < snd.digits.Count)
            {
                return -1;
            }
            else
            {
                int maxlenght = Math.Max(digits.Count, snd.digits.Count);
                for (int i = maxlenght - 1; i >= 0; i--)
                {
                    int aval = i >= digits.Count ? 0 : digits[i];
                    int bval = i >= snd.digits.Count ? 0 : snd.digits[i];
                    if (aval > bval)
                        return 1;
                    else if (aval < bval)
                        return -1;
                }
                return 0;
            }

        }

        //CUT ALL THE GREATEST ZEROS
        private void ShrinkToFit()
        {
            for (int i = digits.Count - 1; i >= 0; i--)
            {
                if (digits[i] != 0   && i!=0)
                {
                    if (i < digits.Count - 1)
                    {
                        break;
                    }
                    digits.RemoveRange(i+1,digits.Count-i-1);
                    break;
                }
            }
        }

        //Transform number to "Human form"
        public override string ToString()
        {
            BigInteger sum = 0;
            for (int i = 0; i < digits.Count; i++)
            {
                sum += digits[i] * BigInteger.Pow(rank, i);
            }
            return sum.ToString();
        }
    }

    class LongIntBin : IComparable
    {
        private BitArray Bits;
        public LongIntBin(long value = 0) => SetValue(value);
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

        public static LongIntBin operator +(LongIntBin a, LongIntBin b)
        {
            int l = a.Bits.Length;
            int r = b.Bits.Length;
            LongIntBin res = new LongIntBin();
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
                    case 1: res.Bits[i] = true; carry = false; break;
                    case 2: res.Bits[i] = false; carry = true; break;
                    case 3: res.Bits[i] = true; carry = true; break;
                }
            }
            return res;
        }

        public static LongIntBin operator -(LongIntBin a, LongIntBin b)
        {
            int l = a.Bits.Length;
            int r = b.Bits.Length;
            LongIntBin res = new LongIntBin();
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

                int value = aval - bval - cval;
                switch (value)
                {
                    case 1: res.Bits[i] = true; carry = false; break;
                    case 0: res.Bits[i] = false; carry = false; break;
                    case -1: res.Bits[i] = true; carry = true; break;
                    case -2: res.Bits[i] = false; carry = true; break;
                }
            }
            return res;
        }

        public static LongIntBin operator *(LongIntBin a, LongIntBin b)
        {
            return
                Math.Max(a.Bits.Length, b.Bits.Length) > 256
                ? KaratsubaMult(a, b)
                : NaiveMult(a, b);
        }

        public static LongIntBin operator /(LongIntBin a, LongIntBin b)
        {
            return a;
        }

        public static LongIntBin operator %(LongIntBin a, LongIntBin b)
        {
            return a;
        }

        public static bool operator >(LongIntBin a, LongIntBin b) => a.CompareTo(b) > 0;
        public static bool operator <(LongIntBin a, LongIntBin b) => a.CompareTo(b) < 0;
        public static bool operator >=(LongIntBin a, LongIntBin b) => a.CompareTo(b) >= 0;
        public static bool operator <=(LongIntBin a, LongIntBin b) => a.CompareTo(b) <= 0;
        public static bool operator ==(LongIntBin a, LongIntBin b) => a.CompareTo(b) == 0;
        public static bool operator !=(LongIntBin a, LongIntBin b) => a.CompareTo(b) != 0;


        //Convert int to LongIntBin implicitly
        public static implicit operator LongIntBin(int v) => new LongIntBin(v);

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
        private static LongIntBin KaratsubaMult(LongIntBin a, LongIntBin b)
        {
            return 0;
        }

        //Naive Multiplication alg - O(n^2)
        private static LongIntBin NaiveMult(LongIntBin a, LongIntBin b)
        {
            int l = a.Bits.Length;
            int r = b.Bits.Length;
            LongIntBin res = new LongIntBin();
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
            LongIntBin snd = (LongIntBin)obj;
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
                    int bval = i >= snd.Bits.Length ? 0 : (snd.Bits[i] ? 1 : 0);
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