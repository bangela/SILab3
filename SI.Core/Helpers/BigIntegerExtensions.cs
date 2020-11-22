using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace SI.Core.Helpers
{
    public static class BigIntegerExtensions
    {
        public static BigInteger ModInverse(this BigInteger a, BigInteger m)
        {
            if (m == 1) return 0;
            BigInteger m0 = m;
            (BigInteger x, BigInteger y) = (1, 0);

            while (a > 1)
            {
                BigInteger q = a / m;
                (a, m) = (m, a % m);
                (x, y) = (y, x - q * y);
            }
            return x < 0 ? x + m0 : x;
        }

        public static BigInteger ModPow(this BigInteger value, BigInteger exponent, BigInteger mod)
        {
            return BigInteger.ModPow(value, exponent, mod);
        }

        public static bool IsProbablePrime(this BigInteger source, int certainty)
        {
            var test = new DJD.Security.BigInteger(source.ToByteArray());
            return test.isProbablePrime(certainty);
            if (source == 2 || source == 3)
                return true;
            if (source < 2 || source % 2 == 0)
                return false;

            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[source.ToByteArray().LongLength];
            BigInteger a;

            for (int i = 0; i < certainty; i++)
            {
                do
                {
                    rng.GetBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= source - 2);

                BigInteger x = BigInteger.ModPow(a, d, source);
                if (x == 1 || x == source - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                    return false;
            }

            return true;
        }

        public static int BitCount(this BigInteger bigN)
        {
            var test = new DJD.Security.BigInteger(bigN.ToByteArray());
            return test.bitCount();
        }
    }
}
