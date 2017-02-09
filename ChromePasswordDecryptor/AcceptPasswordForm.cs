using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLib.UserInterface.WinForms;
using CommonLib.Logging;

namespace ChromePasswordDecryptor
{
    public partial class AcceptPasswordForm : FixedToolForm
    {
        public string Password
        {
            get;
            private set;
        }

        bool mAcceptPassword = false;


        public AcceptPasswordForm(bool acceptPassword = true)
        {
            mAcceptPassword = acceptPassword;

            InitializeComponent();

            DialogResult = DialogResult.Cancel;

            this.StartPosition = FormStartPosition.CenterParent;

            if(acceptPassword)
            {
                this.tbConfirmPassword.Visible = false;
                this.label5.Visible = false;
            }
            else
            {
                this.tbConfirmPassword.Visible = true;
                this.label5.Visible = true;
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (!mAcceptPassword)
            {
                if (!tbNewPassword.Text.Equals(tbConfirmPassword.Text))
                {
                    LabelError.Text = "Password does not match the confirm password!";
                    return;
                }

                if (!PCIPassword(this.tbNewPassword.Text))
                {
                    LabelError.Text = "Password must contain uppercase, lowercase, digit and nonalphanumeric chracters";
                    return;
                }
            }

            Password = this.tbNewPassword.Text;
            DialogResult = DialogResult.OK;
        }
        private static char[] Nonalphanumeric = new char[] { '!', ' ', '£', '$', '%', '^', '&', '*', '(', ')', '-', '=', '_', '+', '[', ']', '#', '{' };


        public static bool PCIPassword(string password)
        {
            int secureLevel = 0;

            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                return false;
            }

            if (password.Any(c => Char.IsDigit(c)))
            {
                secureLevel++;
            }

            if (password.Any(c => Char.IsUpper(c)))
            {
                secureLevel++;
            }

            if (password.Any(c => Char.IsLower(c)))
            {
                secureLevel++;
            }

            if (password.IndexOfAny(Nonalphanumeric) != -1)
            {
                secureLevel++;
            }

            return secureLevel >= 3;
        }
    }
}
