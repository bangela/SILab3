using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace SI.Core.Helpers
{
    public static class BigIntegerHelpers
    {
        public static BigInteger GetPrime(int size, int certenity)
        {
            var rand = new Random();
            var psudoPrime = DJD.Security.BigInteger.genPseudoPrime(size, certenity, rand);
            var bytes = psudoPrime.getBytes();
            var prime = new BigInteger(bytes);
            return prime;
        }

        public static BigInteger RandomNumber(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] randomNumber = new byte[size];
            rng.GetBytes(randomNumber);
            var number = new BigInteger(randomNumber);
            number = BigInteger.Abs(number);
            return number;
        }
    }
}
