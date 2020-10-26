namespace CSharp.DS.Algo.Bit_Manipulations
{
    public partial class BitsManipulation
    {
        public int CountSetBits(uint n)
        {
            // Also known as Hamming weight
            // In C#, shifts are arithmetic shifts (in contrast to logical shifts).
            // In a right arithmetic shift, the sign bit is shifted in on the left,
            //  so the sign of the number is preserved. A

            // BF Checking all bits: O(N) = O(32)
            //int count = 0;
            //while (n != 0)
            //{
            //    if (n % 2 == 1)
            //        count++;

            //    n >>= 1;
            //}

            //int count = 0;
            //int mask = 1;
            //for (int i = 0; i < 32; i++)
            //{
            //    if ((n & mask) != 0)
            //    {
            //        count++;
            //    }
            //    // n = 10100001
            //    // mask = 00000101
            //    mask <<= 1;
            //}

            // O(# of 1s)
            int count = 0;
            while (n != 0)
            {
                count++;
                // n   = 110100 != 0
                // n-1 = 110011
                // &
                // =     110000
                n = n & (n - 1);
            }

            return count;
        }
    }
}
