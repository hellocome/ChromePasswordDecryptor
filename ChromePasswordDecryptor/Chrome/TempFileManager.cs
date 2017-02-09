using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ChromePasswordDecryptor.Chrome
{
    public class TempFileManager : IDisposable
    {
        public string SourceFile { get; private set; }
        public string TempFile { get; private set; }
        public TempFileManager(string stringFile)
        {
            SourceFile = stringFile;
        }

        public bool LoadTempFile()
        {
            string tempFile = string.Empty;

            return LoadTempFile(out tempFile);
        }
        public bool LoadTempFile(out string tempFile)
        {
            tempFile = string.Empty;

            try
            {
                TempFile = Path.GetTempPath() + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyyMMddHHmmssfff");

                File.Copy(SourceFile, TempFile);

                tempFile = Path.GetFullPath(TempFile);

                return true;
            }
            catch (Exception ex)
            {
                CommonLib.Logging.Logger.Instance.Error(ex.ToString());
                return false;
            }
        }

        public void Dispose()
        {
            try
            {
                if(File.Exists(TempFile))
                {
                    File.Delete(TempFile);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
