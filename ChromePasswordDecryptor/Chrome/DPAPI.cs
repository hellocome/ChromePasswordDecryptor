using System;
using System.Security.Cryptography;
using System.Text;
using CommonLib.LibEncoding;

namespace ChromePasswordDecryptor.Chrome
{
    public static class DPAPI
    {
        public static void Protect(ref byte[] btData, byte[] additionalEntropyBuf, DataProtectionScope scope)
        {            
            try
            {
                btData = ProtectedData.Protect(btData, additionalEntropyBuf, scope);
            }
            catch (Exception ex)
            {
                throw new Exception("Protect: " + ex.Message);
            }
        }

        public static void Unprotect(ref byte[] data, byte[] additionalEntropy, DataProtectionScope scope)
        {
            if (data == null) throw new Exception("Unprotect: null data!");

            try
            {
                data = ProtectedData.Unprotect(data, additionalEntropy, scope);
            }
            catch (Exception ex)
            {
                throw new Exception("Unprotect: null data! " + ex.Message);
            }
        }

        public static bool Protect(string data, ref string encryptedData, string additionalEntropy, DataProtectionScope scope)
        {
            try
            {
                byte[] btData = Encoding.Default.GetBytes(data);
                byte[] additionalEntropyBuf = Encoding.Default.GetBytes(additionalEntropy);
                byte[] btEncrypted = ProtectedData.Protect(btData, additionalEntropyBuf, scope);

                encryptedData = StaticEncoder.Ascii2HexString(btEncrypted);
                return true;
            }
            catch (CryptographicException)
            {
                return false;
            }
        }

        public static bool Unprotect(string encryptedData, ref string data, string additionalEntropy, DataProtectionScope scope)
        {
            byte[] encryptedDataBuf = StaticEncoder.HexString2Ascii(encryptedData);
            byte[] additionalEntropyBuf = Encoding.Default.GetBytes(additionalEntropy);

            if (encryptedDataBuf == null)
            {
                return false;
            }

            byte[] btPlaintext = ProtectedData.Unprotect(encryptedDataBuf, additionalEntropyBuf, scope);
            data = Encoding.Default.GetString(btPlaintext);
            return true;
        }

        public static void PMProtect(ref byte[] btData, MemoryProtectionScope scope)
        {
            // Pad the data up to a multiple of 16
            Array.Resize(ref btData, 16 * ((btData.Length + 15) / 16));

            // Protect it
            ProtectedMemory.Protect(btData, scope);
        }

        public static void PMUnprotect(ref byte[] btData, int realLength, MemoryProtectionScope scope)
        {
            // Unprotect the data
            ProtectedMemory.Unprotect(btData, scope);

            // Crop to realLength to remove padding
            Array.Resize(ref btData, realLength);
        }
    }
}
