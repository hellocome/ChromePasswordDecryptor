using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Security.Cryptography;

namespace ChromePasswordDecryptor.Chrome
{
    [Serializable]
    public class ChromePasswordItem
    {
        public string WindowsUser { get; set; }
        public string ActionUrl { get; set; }
        public string LoginUser { get; set; }
        public byte[] EncryptedPassword { get; set; }

        public string EncryptedPasswordHexString
        {
            get
            {
                if (EncryptedPassword != null)
                {
                    return CommonLib.LibEncoding.StaticEncoder.Ascii2HexString(EncryptedPassword);
                }

                return string.Empty;
            }
        }

        public string DecryptedPassword
        {
            get
            {
                return DecryptPassword();
            }
        }

        public string DecryptPassword()
        {
            try
            {
                if (Environment.UserName.Equals(WindowsUser, StringComparison.OrdinalIgnoreCase))
                {
                    byte[] temp = new byte[EncryptedPassword.Length];

                    EncryptedPassword.CopyTo(temp, 0);
                    DPAPI.Unprotect(ref temp, null, DataProtectionScope.CurrentUser);
                    return CommonLib.LibEncoding.StaticEncoder.Bytes2String(temp);
                }
            }
            catch (Exception) { }

            return "Unable to decrypt";
        }

        public string ToBinary()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    formatter.Serialize(ms, this);
                    return CommonLib.LibEncoding.StaticEncoder.Ascii2HexString(ms.ToArray());
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    return string.Empty;
                }
            }
        }

        public static ChromePasswordItem FromBinary(string hex)
        {
            byte[] bytes = CommonLib.LibEncoding.StaticEncoder.HexString2Ascii(hex);

            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                try
                {
                    ChromePasswordItem item = formatter.Deserialize(ms) as ChromePasswordItem;
                    return item;
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    return null;
                }
            }
        }
    }
}
