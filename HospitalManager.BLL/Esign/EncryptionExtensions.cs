using System;
using System.Text;
using System.Security.Cryptography;
using HospitalManager.Core.Encryption;

namespace HospitalManager.BLL.Esign
{
    public static class EncryptionExtensions
    {
        public static string ProtectString(this string clearText, byte[] entropy)
        {
            if (clearText == null)
            {
                throw new ArgumentNullException(nameof(clearText));
            }

            byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);
            byte[] encryptedBytes = ProtectedData.Protect(clearBytes, entropy, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string UnprotectString(this string encryptedText, byte[] entropy)
        {
            if (encryptedText == null)
            {
                throw new ArgumentNullException(nameof(encryptedText));
            }

            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] clearBytes = ProtectedData.Unprotect(encryptedBytes, entropy, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(clearBytes);
        }

        public static byte[] ProtectBytes(this byte[] clearBytes, byte[] entropy)
        {
            if (clearBytes == null)
            {
                throw new ArgumentNullException(nameof(clearBytes));
            }

            byte[] encryptedBytes = ProtectedData.Protect(clearBytes, entropy, DataProtectionScope.CurrentUser);
            return encryptedBytes;
        }

        public static byte[] UnProtectBytes(this byte[] encryptedBytes, byte[] entropy)
        {
            if (encryptedBytes == null)
            {
                throw new ArgumentNullException(nameof(encryptedBytes));
            }

            byte[] clearBytes = ProtectedData.Unprotect(encryptedBytes, entropy, DataProtectionScope.CurrentUser);
            return clearBytes;
        }
    }
}