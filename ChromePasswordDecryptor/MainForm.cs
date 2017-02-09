using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using CommonLib.UserInterface.WinForms;
using CommonLib.Logging;
using ChromePasswordDecryptor.Chrome;

namespace ChromePasswordDecryptor
{
    public partial class MainForm : NoBorderMainForm
    {
        public MainForm()
        {
            InitializeComponent();

            Logger.Instance.Init(new WriteLogDelegate(OutputLog));
            Logger.Instance.Info("Start");

            this.ctbFilter.DisplayTextBox.TextChanged += DisplayTextBox_TextChanged;
        }

        private void DisplayTextBox_TextChanged(object sender, EventArgs e)
        {
            Filter(this.ctbFilter.DisplayText);
        }

        private void Filter(string txt)
        {
            lock (lviAll)
            {
                List<ListViewItem> lviTemp = new List<ListViewItem>();

                if (string.IsNullOrEmpty(txt))
                {
                    lviAll.ForEach(a => lviTemp.Add(a));
                }
                else
                {
                    foreach (ListViewItem lvi in lviAll)
                    {
                        ChromePasswordItem cpi = lvi.Tag as ChromePasswordItem;

                        if (cpi.ActionUrl.ToLower().Contains(txt.ToLower()) 
                            || cpi.LoginUser.ToLower().Contains(txt.ToLower())
                            || cpi.WindowsUser.ToLower().Contains(txt.ToLower()))
                        {
                            lviTemp.Add(lvi);
                        }
                    }
                }

                this.LVPasswords.Items.Clear();
                this.LVPasswords.Items.AddRange(lviTemp.ToArray());
            }
        }

        protected void OutputLog(object color, string str)
        {
            rtbLog.AppendText(str + "\r\n");
        }

        private static List<ListViewItem> lviAll = new List<ListViewItem>();

        private void Reload()
        {
            try
            {
                lock (lviAll)
                {
                    lviAll.Clear();

                    ChromePasswordImpl cpi = new ChromePasswordImpl();

                    Dictionary<string, List<ChromePasswordItem>> map = cpi.GetChromePasswordItem();

                    this.LVPasswords.Items.Clear();

                    foreach (string user in map.Keys)
                    {
                        ListViewGroup lvg = new ListViewGroup(user);
                        List<ChromePasswordItem> cpiList = map[user];

                        foreach (ChromePasswordItem item in cpiList)
                        {
                            ListViewItem lvi = new ListViewItem(item.ActionUrl);
                            lvi.SubItems.Add(item.LoginUser);
                            lvi.SubItems.Add(item.EncryptedPasswordHexString);
                            lvi.SubItems.Add(item.DecryptPassword());
                            lvi.Tag = item;
                            lvi.Group = lvg;

                            this.LVPasswords.Items.Add(lvi);
                            lviAll.Add(lvi);
                        }

                        this.LVPasswords.Groups.Add(lvg);
                    }

                    Logger.Instance.Info(this.LVPasswords.Items.Count.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex.ToString());
            }
        }

        private void Reload(List<ChromePasswordItem> cpiList)
        {
            try
            {
                lock (lviAll)
                {
                    this.LVPasswords.Items.Clear();
                    lviAll.Clear();

                    foreach (ChromePasswordItem item in cpiList)
                    {
                        ListViewItem lvi = new ListViewItem(item.ActionUrl);
                        lvi.SubItems.Add(item.LoginUser);
                        lvi.SubItems.Add(item.EncryptedPasswordHexString);
                        lvi.SubItems.Add(item.DecryptPassword());
                        lvi.Tag = item;

                        this.LVPasswords.Items.Add(lvi);
                        lviAll.Add(lvi);
                    }

                    Logger.Instance.Info(this.LVPasswords.Items.Count.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex.ToString());
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            this.rtbLog.Text = "";
        }

        private void btnExportAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string HexData = GetAllData().ToString();
                    AcceptPasswordForm pf = new AcceptPasswordForm(false);

                    if (pf.ShowDialog() == DialogResult.OK)
                    {
                        EncryptionHelper.Utils.EncryptionUtils.TripleDESEnecryptFromPassword(saveFileDialog.FileName, pf.Password , HexData);
                    }
                }
            }
            catch (Exception ex)
            {
                FixedMessageBox.Show("An errors occurs, make sure the password is correct!\r\n" + ex.Message, "Error", FixedMessageBoxButtons.OK, FixedMessageBoxIcon.Error);
                return;
            }
        }

     

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    AcceptPasswordForm pf = new AcceptPasswordForm(true);

                    if (pf.ShowDialog() == DialogResult.OK)
                    {
                        string date = EncryptionHelper.Utils.EncryptionUtils.TripleDESDecryptFromPassword(openFileDialog.FileName, pf.Password);

                        List<ChromePasswordItem> cpiList = LoadAllDataFromFile(date);

                        Reload(cpiList);
                    }
                }
            }
            catch (Exception ex)
            {
                FixedMessageBox.Show("An errors occurs, make sure the password is correct!\r\n" + ex.Message, "Error", FixedMessageBoxButtons.OK, FixedMessageBoxIcon.Error);
                return;
            }
        }

        private StringBuilder GetAllData()
        {
            StringBuilder sb = new StringBuilder();
            ChromePasswordImpl cpi = new ChromePasswordImpl();
            Dictionary<string, List<ChromePasswordItem>> map = cpi.GetChromePasswordItem();

            foreach (string user in map.Keys)
            {
                List<ChromePasswordItem> cpiList = map[user];

                foreach (ChromePasswordItem item in cpiList)
                {
                    sb.AppendLine(item.ToBinary());
                }
            }

            return sb;
        }

        private List<ChromePasswordItem> LoadAllDataFromFile(string str)
        {
            string line = string.Empty;

            List<ChromePasswordItem> items = new List<ChromePasswordItem>();

            StringReader sr = new StringReader(str);

            while ((line = sr.ReadLine()) != null)
            {

                ChromePasswordItem item = ChromePasswordItem.FromBinary(line);
                if (item != null)
                {
                    items.Add(item);
                }
            }

            return items;
        }
    }
}
