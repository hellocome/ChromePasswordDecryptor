using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace EncryptionHelper.Utils
{
    public static class EncryptionUtils
    {
        const string KEY_HEADER = "-----BEGIN RSA PRIVATE KEY-----";
        const string KEY_FOOTER = "-----END RSA PRIVATE KEY-----";


        public static byte[] RSAEncrypt(string publicKey, string data)
        {
            CspParameters cspParams = new CspParameters
            {
                Flags = CspProviderFlags.UseMachineKeyStore,
                ProviderType = 1
            };

            using (RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(cspParams))
            {
                byte[] key = Convert.FromBase64String(publicKey);
                rsaProvider.ImportCspBlob(key);

                byte[] plainBytes = Encoding.UTF8.GetBytes(data);
                byte[] encryptedBytes = rsaProvider.Encrypt(plainBytes, false);

                return encryptedBytes;
            }
        }

        public static string RSADecrypt(string privateKey, byte[] encryptedBytes)
        {

            CspParameters cspParams = new CspParameters
            {
                Flags = CspProviderFlags.UseMachineKeyStore,
                ProviderType = 1
            };

            using (RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(cspParams))
            {
                try
                {
                    byte[] key = Convert.FromBase64String(privateKey);
                    rsaProvider.ImportCspBlob(key);

                    byte[] plainBytes = rsaProvider.Decrypt(encryptedBytes, false);

                    string plainText = Encoding.UTF8.GetString(plainBytes, 0, plainBytes.Length);

                    return plainText;
                }
                finally
                {
                    rsaProvider.PersistKeyInCsp = false;
                }
            }
        }

        public static byte[] SHA256Hash(string str)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                return hash.ComputeHash(enc.GetBytes(str));
            }
        }

        public static void TripleDESEnecryptFromPassword(string fileName, string password, string data)
        {
            byte[] Key = SHA256Hash(password).Take(24).ToArray();
            byte[] VI = SHA256Hash(password.Reverse().ToString()).Take(8).ToArray();

            EncryptTextToFile(data, fileName, Key, VI);
        }

        public static string TripleDESDecryptFromPassword(string fileName, string password)
        {
            byte[] Key = SHA256Hash(password).Take(24).ToArray();
            byte[] VI = SHA256Hash(password.Reverse().ToString()).Take(8).ToArray();

            return DecryptTextFromFile(fileName, Key, VI);
        }

        public static string DecryptTextFromFile(String FileName, byte[] Key, byte[] IV)
        {
            // Create or open the specified file. 
            using (FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate))
            {

                // Create a new TripleDES object.
                TripleDES tripleDESalg = TripleDES.Create();

                // Create a CryptoStream using the FileStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(fStream,
                    tripleDESalg.CreateDecryptor(Key, IV),
                    CryptoStreamMode.Read);

                // Create a StreamReader using the CryptoStream.
                StreamReader sReader = new StreamReader(cStream);

                // Read the data from the stream 
                // to decrypt it.
                string val = sReader.ReadToEnd();

                // Close the streams and
                // close the file.
                sReader.Close();
                cStream.Close();
                fStream.Close();

                // Return the string. 
                return val;
            }
        }

        public static void EncryptTextToFile(string Data, string FileName, byte[] Key, byte[] IV)
        {
            // Create or open the specified file.
            using (FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate))
            {

                // Create a new TripleDES object.
                TripleDES tripleDESalg = TripleDES.Create();

                // Create a CryptoStream using the FileStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(fStream, tripleDESalg.CreateEncryptor(Key, IV), CryptoStreamMode.Write);

                // Create a StreamWriter using the CryptoStream.
                StreamWriter sWriter = new StreamWriter(cStream);

                // Write the data to the stream 
                // to encrypt it.
                sWriter.WriteLine(Data);

                // Close the streams and
                // close the file.
                sWriter.Close();
                cStream.Close();
                fStream.Close();
            }
        }

        public static string ExportPrivateKeyToPEM(this RSACryptoServiceProvider csp)
        {
            if (csp.PublicOnly)
                throw new ArgumentException("CSP does not contain a private key", "csp");

            RSAParameters parameters = csp.ExportParameters(true);
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (MemoryStream innerStream = new MemoryStream())
                {
                    BinaryWriter innerWriter = new BinaryWriter(innerStream);
                    EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                    EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent);
                    EncodeIntegerBigEndian(innerWriter, parameters.D);
                    EncodeIntegerBigEndian(innerWriter, parameters.P);
                    EncodeIntegerBigEndian(innerWriter, parameters.Q);
                    EncodeIntegerBigEndian(innerWriter, parameters.DP);
                    EncodeIntegerBigEndian(innerWriter, parameters.DQ);
                    EncodeIntegerBigEndian(innerWriter, parameters.InverseQ);
                    int length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                return Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length);
            }
        }

        public static string ExportPublicKeyToPEM(this RSACryptoServiceProvider csp)
        {
            RSAParameters parameters = csp.ExportParameters(false);
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (MemoryStream innerStream = new MemoryStream())
                {
                    BinaryWriter innerWriter = new BinaryWriter(innerStream);
                    EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                    EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent);

                    //All Parameter Must Have Value so Set Other Parameter Value Whit Invalid Data  (for keeping Key Structure  use "parameters.Exponent" value for invalid data)
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.D
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.P
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.Q
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.DP
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.DQ
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent); // instead of parameters.InverseQ

                    int length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                return Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length);
            }
        }

        private static void EncodeLength(BinaryWriter stream, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
            if (length < 0x80)
            {
                // Short form
                stream.Write((byte)length);
            }
            else
            {
                // Long form
                int temp = length;
                int bytesRequired = 0;
                while (temp > 0)
                {
                    temp >>= 8;
                    bytesRequired++;
                }
                stream.Write((byte)(bytesRequired | 0x80));
                for (int i = bytesRequired - 1; i >= 0; i--)
                {
                    stream.Write((byte)(length >> (8 * i) & 0xff));
                }
            }
        }

        private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)0x02); // INTEGER
            int prefixZeros = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] != 0) break;
                prefixZeros++;
            }
            if (value.Length - prefixZeros == 0)
            {
                EncodeLength(stream, 1);
                stream.Write((byte)0);
            }
            else
            {
                if (forceUnsigned && value[prefixZeros] > 0x7f)
                {
                    // Add a prefix zero to force unsigned if the MSB is 1
                    EncodeLength(stream, value.Length - prefixZeros + 1);
                    stream.Write((byte)0);
                }
                else
                {
                    EncodeLength(stream, value.Length - prefixZeros);
                }
                for (int i = prefixZeros; i < value.Length; i++)
                {
                    stream.Write(value[i]);
                }
            }
        }

        public static RSAParameters DecodeRSAPrivateKey(string privateKeyInPEM)
        {
            if (!string.IsNullOrEmpty(privateKeyInPEM) == false)
                throw new ArgumentException("bad format");

            string keyFormatted = privateKeyInPEM;

            int cutIndex = keyFormatted.IndexOf(KEY_HEADER);
            keyFormatted = keyFormatted.Substring(cutIndex, keyFormatted.Length - cutIndex);
            cutIndex = keyFormatted.IndexOf(KEY_FOOTER);
            keyFormatted = keyFormatted.Substring(0, cutIndex + KEY_FOOTER.Length);
            keyFormatted = keyFormatted.Replace(KEY_HEADER, "");
            keyFormatted = keyFormatted.Replace(KEY_FOOTER, "");
            keyFormatted = keyFormatted.Replace("\r", "");
            keyFormatted = keyFormatted.Replace("\n", "");
            keyFormatted = keyFormatted.Trim();

            byte[] privateKeyInDER = System.Convert.FromBase64String(keyFormatted);

            byte[] paramModulus;
            byte[] paramDP;
            byte[] paramDQ;
            byte[] paramIQ;
            byte[] paramE;
            byte[] paramD;
            byte[] paramP;
            byte[] paramQ;

            using (MemoryStream memoryStream = new MemoryStream(privateKeyInDER))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream))
                {

                    ushort twobytes = 0;
                    int elements = 0;
                    byte bt = 0;

                    twobytes = binaryReader.ReadUInt16();
                    if (twobytes == 0x8130)
                        binaryReader.ReadByte();
                    else if (twobytes == 0x8230)
                        binaryReader.ReadInt16();
                    else
                        throw new CryptographicException("Wrong data");

                    twobytes = binaryReader.ReadUInt16();
                    if (twobytes != 0x0102)
                        throw new CryptographicException("Wrong data");

                    bt = binaryReader.ReadByte();
                    if (bt != 0x00)
                        throw new CryptographicException("Wrong data");

                    elements = GetIntegerSize(binaryReader);
                    paramModulus = binaryReader.ReadBytes(elements);

                    elements = GetIntegerSize(binaryReader);
                    paramE = binaryReader.ReadBytes(elements);

                    elements = GetIntegerSize(binaryReader);
                    paramD = binaryReader.ReadBytes(elements);

                    elements = GetIntegerSize(binaryReader);
                    paramP = binaryReader.ReadBytes(elements);

                    elements = GetIntegerSize(binaryReader);
                    paramQ = binaryReader.ReadBytes(elements);

                    elements = GetIntegerSize(binaryReader);
                    paramDP = binaryReader.ReadBytes(elements);

                    elements = GetIntegerSize(binaryReader);
                    paramDQ = binaryReader.ReadBytes(elements);

                    elements = GetIntegerSize(binaryReader);
                    paramIQ = binaryReader.ReadBytes(elements);

                    EnsureLength(ref paramD, 256);
                    EnsureLength(ref paramDP, 128);
                    EnsureLength(ref paramDQ, 128);
                    EnsureLength(ref paramE, 3);
                    EnsureLength(ref paramIQ, 128);
                    EnsureLength(ref paramModulus, 256);
                    EnsureLength(ref paramP, 128);
                    EnsureLength(ref paramQ, 128);

                    RSAParameters rsaParameters = new RSAParameters();
                    rsaParameters.Modulus = paramModulus;
                    rsaParameters.Exponent = paramE;
                    rsaParameters.D = paramD;
                    rsaParameters.P = paramP;
                    rsaParameters.Q = paramQ;
                    rsaParameters.DP = paramDP;
                    rsaParameters.DQ = paramDQ;
                    rsaParameters.InverseQ = paramIQ;

                    return rsaParameters;
                }
            }
        }

        private static int GetIntegerSize(BinaryReader binary)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;

            bt = binary.ReadByte();

            if (bt != 0x02)
                return 0;

            bt = binary.ReadByte();

            if (bt == 0x81)
                count = binary.ReadByte();
            else if (bt == 0x82)
            {
                highbyte = binary.ReadByte();
                lowbyte = binary.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
                count = bt;

            while (binary.ReadByte() == 0x00)
                count -= 1;

            binary.BaseStream.Seek(-1, SeekOrigin.Current);

            return count;
        }

        private static void EnsureLength(ref byte[] data, int desiredLength)
        {
            if (data == null || data.Length >= desiredLength)
                return;

            int zeros = desiredLength - data.Length;

            byte[] newData = new byte[desiredLength];
            Array.Copy(data, 0, newData, zeros, data.Length);

            data = newData;
        }
    }
}
