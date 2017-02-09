using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

using CommonLib.Logging;

namespace ChromePasswordDecryptor.Chrome
{
    public class ChromePasswordImpl
    {
        // Windows 10 / 8 / 7 / Vista
        // Google Chrome: C:\Users\%USERNAME%\AppData\Local\Google\Chrome\User Data\Default
        private List<ChromePasswordItem> GetChromePasswordItemForUser(string userBase, string user)
        {
            string loginData = Path.GetFullPath(string.Format("{0}\\{1}\\AppData\\Local\\Google\\Chrome\\User Data\\Default\\Login Data", userBase, user));

            List<ChromePasswordItem> cpiList = new List<ChromePasswordItem>();

            try
            {
                if (File.Exists(loginData))
                {
                    using (TempFileManager tempFile = new TempFileManager(loginData))
                    {
                        tempFile.LoadTempFile();

                        string connectionString = string.Format(@"Data Source=""{0}""; Version=3;", tempFile.TempFile);

                        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                        {
                            Logger.Instance.Info("Opening: " + loginData);
                            conn.Open();
                            string sql = "SELECT action_url, username_value, password_value FROM logins";

                            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                            {
                                using (SQLiteDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        try
                                        {
                                            ChromePasswordItem cpi = new ChromePasswordItem();
                                            cpi.ActionUrl = reader["action_url"].ToString();
                                            cpi.LoginUser = reader["username_value"].ToString();
                                            cpi.EncryptedPassword = reader["password_value"] as byte[];
                                            cpi.WindowsUser = user;

                                            cpiList.Add(cpi);
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.Instance.Error(ex.ToString());
                                        }
                                    }
                                }
                            }
                            conn.Close();
                        }
                    }
                }
                else
                {
                    Logger.Instance.Info("No Login Data: " + loginData);
                }
            }
            catch (Exception exx)
            {
                Logger.Instance.Error(exx.ToString());
            }

            return cpiList;
        }

        public Dictionary<string, List<ChromePasswordItem>> GetChromePasswordItem()
        {
            Dictionary<string, List<ChromePasswordItem>> userCPIMap = new Dictionary<string, List<ChromePasswordItem>>();

            string userBase = Path.GetPathRoot(Environment.SystemDirectory) + System.IO.Path.DirectorySeparatorChar + "\\Users\\";
            string[] userDirectories = Directory.GetDirectories(userBase);

            if (userDirectories != null)
            {
                foreach (string userDir in userDirectories)
                {
                    DirectoryInfo di = new DirectoryInfo(userDir);

                    List<ChromePasswordItem> cpiList = GetChromePasswordItemForUser(userBase, di.Name);

                    userCPIMap.Add(di.Name, cpiList);
                }
            }

            return userCPIMap;
        }
    }
}
