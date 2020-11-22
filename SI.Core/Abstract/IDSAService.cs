using DJD.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI.Core.Abstract
{
    public interface IDSAService
    {
        void GenerateKey();

        Tuple<BigInteger, BigInteger> SignData(byte[] data);

        bool Verify(byte[] data, BigInteger r, BigInteger s);

        BigInteger Q { get;  }
        BigInteger P { get;  }
        BigInteger G { get;  }
        BigInteger Y { get;  }
        BigInteger X { get;  }

    }
}
