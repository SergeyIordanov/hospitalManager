using System.Security.Cryptography;

namespace HospitalManager.Core.Encryption
{
    public static class Entropy
    {
        public static byte[] CreateRandomEntropy()
        {
            var entropy = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(entropy);

            return entropy;
        }

        public static readonly byte[] EntropyBytes = { 1, 2, 3, 4, 5 };
    }
}