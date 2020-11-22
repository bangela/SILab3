using DJD.Security;
using Newtonsoft.Json;
using SI.Core.Models;
using System;

namespace SI.Core.Helpers
{
    public static class DsaSerializer
    {
        public static string Serialize(Tuple<BigInteger, BigInteger> signTuple)
        {
            var key = new Key
            {
                R = signTuple.Item1.ToString(),
                S = signTuple.Item2.ToString()
            };
            return JsonConvert.SerializeObject(key);
        }

        public static Tuple<BigInteger, BigInteger> Deserialize(string str)
        {
            var key = JsonConvert.DeserializeObject<Key>(str);
            var key1 = new BigInteger(key.R, 10);
            var key2 = new BigInteger(key.S, 10);

            return new Tuple<BigInteger, BigInteger>(key1, key2);
        }
    }
}
