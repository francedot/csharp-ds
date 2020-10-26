namespace CSharp.DS.Algo.Math
{
    public partial class Math
    {
        /*
            Implement pow(x, n), which calculates x raised to the power n (i.e. x^n).
         */
        public double MyPowBF(double x, int n)
        {
            // To handle the case where N=int.MinValue
            long N = n;
            if (N < 0) // Handle fraction
            {
                x = 1 / x;
                N *= -1;
            }

            var result = 1.0;
            for (var i = 0L; i < N; i++)
                result *= x;

            return result;
        }

        public double MyPowOpt(double x, int n)
        {
            // To handle the case where N=int.MinValue
            long N = n;
            if (N < 0) // Handle fraction
            {
                x = 1 / x;
                N *= -1;
            }

            // The basic idea is to decompose the exponent into powers of 2,
            // so that you can keep halving the problem.
            // E.g. N = 9 = 2^3 + 2^0 = 1001 in binary. Then:
            //      x ^ 9 = x ^ (2 ^ 3) * x ^ (2 ^ 0)
            // The i-th index correspond to the exponent

            // We can see that every time we encounter a 1
            // in the binary representation of N, we need to multiply
            // the answer with x^(2^i) where i is the ith bit of the exponent.
            // Thus, we can keep a running total of repeatedly squaring x
            // (x, x^2, x^4, x^8, etc) and multiply it by the answer when we see a 1.

            // X = 3, N = 9, 3^9
            double result = 1;
            while (N > 0) // 9 = 1001 = 2^3 + 2^0 // 8 // 2 // 1
            {
                if ((N & 1) == 1)
                    result *= x; // 3 //  3*6561

                N >>= 1; // 100 = 8 // 10 = 2 // 1
                x *= x; // 9 // 81 // 6561 
            }

            return result;
        }

        public static int Atoi(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return 0;

            int sign = 1, @base = 0, i = 0;
            while (s[i] == ' ')
                i++;

            if (s[i] == '-' || s[i] == '+')
            {
                sign = s[i] == '-' ? -1 : 1;
                i++;
            }

            while (i < s.Length && s[i] >= '0' && s[i] <= '9')
            {
                if (@base > int.MaxValue / 10 || (@base == int.MaxValue / 10 && s[i] - '0' > 7))
                    return sign == 1 ? int.MaxValue : int.MinValue;
                
                @base = 10 * @base + (s[i] - '0');
                i++;
            }

            return @base * sign;
        }

        public bool IsPalindrome(int x)
        {
            if (x < 0 || (x != 0 && x % 10 == 0)) return false;
            int rev = 0;
            while (x > rev)
            {
                rev = rev * 10 + x % 10;
                x = x / 10;
            }
            return (x == rev || x == rev / 10);

            //if (x < 0)
            //    return false;

            //// Reversing number without the need to handle the overflow
            //int reversed = 0;
            //while (x != 0)
            //{
            //    var remainder = x % 10; // reversed integer is stored in variable // 112 
            //    reversed = reversed * 10 + remainder; // multiply reversed by 10 then add the remainder so it gets stored at next decimal place.
            //    x /= 10;  // the last digit is removed from num after division by 10.
            //}

            //// No overflow bc Since we divided the number by 10, and multiplied the reversed number by 10, when the original number is less than the reversed number, it means we've processed half of the number digits

            //// palindrome if original and reversed are equal
            //return x == reversed || x == reversed / 10; // if odd
        }

        public int BinaryToDecimal(string n)
        {
            string num = n;
            int decValue = 0;

            // Initializing base value to 1,
            // i.e 2^0
            int base1 = 1;

            int len = num.Length;
            for (var i = len - 1; i >= 0; i--)
            {
                if (num[i] == '1')
                    decValue += base1;
                base1 *= 2;
            }

            return decValue;
        }
    }
}
