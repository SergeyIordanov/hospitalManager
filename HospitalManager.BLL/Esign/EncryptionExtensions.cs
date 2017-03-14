using System;
using System.Text;
using System.Security.Cryptography;
using HospitalManager.Core.Encryption;

namespace HospitalManager.BLL.Esign
{
    public static class EncryptionExtensions
    {
        public static string ProtectString(this string clearText)
        {
            if (clearText == null)
            {
                throw new ArgumentNullException(nameof(clearText));
            }

            byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);
            byte[] encryptedBytes = ProtectedData.Protect(clearBytes, Entropy.EntropyBytes, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string UnprotectString(this string encryptedText)
        {
            if (encryptedText == null)
            {
                throw new ArgumentNullException(nameof(encryptedText));
            }

            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] clearBytes = ProtectedData.Unprotect(encryptedBytes, Entropy.EntropyBytes, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(clearBytes);
        }

        public static byte[] ProtectBytes(this byte[] clearBytes)
        {
            if (clearBytes == null)
            {
                throw new ArgumentNullException(nameof(clearBytes));
            }

            byte[] encryptedBytes = ProtectedData.Protect(clearBytes, Entropy.EntropyBytes, DataProtectionScope.CurrentUser);
            return encryptedBytes;
        }

        public static byte[] UnProtectBytes(this byte[] encryptedBytes)
        {
            if (encryptedBytes == null)
            {
                throw new ArgumentNullException(nameof(encryptedBytes));
            }

            byte[] clearBytes = ProtectedData.Unprotect(encryptedBytes, Entropy.EntropyBytes, DataProtectionScope.CurrentUser);
            return clearBytes;
        }
    }
}