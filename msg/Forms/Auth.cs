using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace msg
{
    public partial class Auth : Form
    {
        private DB db = new DB();
        public Auth()
        {
            InitializeComponent();
            //db.Innit();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (CheckFill())
            {
                if (db.CheckIfLoginExist(loginTb.Text))
                {
                    if (db.Login(loginTb.Text, passwordTb.Text))
                    {
                        passwordTb.Text = "";
                        (new Client(db, this)).ShowDialog();
                    }
                    else
                        Say("Пароль неправильний");
                }
                else
                {
                    Say("Логін не існує");
                }
            }
        }
        private void register_Click(object sender, EventArgs e)
        {
            if (CheckFill())
            {
                if (!db.CheckIfLoginExist(loginTb.Text))
                {
                    if (db.Register(loginTb.Text, passwordTb.Text))
                    {
                        passwordTb.Text = "";
                        (new Client(db, this)).ShowDialog();
                    }
                    else
                        Say("Перевірте підключення");
                }
                else
                {
                    Say("Логін зайнято");
                }
            }
        }
        private bool CheckFill()
        {
            if (loginTb.Text.Length < 3)
            {
                Say("Логін поваинен містити принаймні 3 символи");
                return false;
            }
            else if (passwordTb.Text.Length < 3)
            {
                Say("Пароль поваинен містити принаймні 3 символи");
                return false;
            }
            else if(loginTb.Text.Contains("'") || passwordTb.Text.Contains("'"))
            {
                Say("Символ одинарних лапок ( ' ) заборонено");
                return false;
            }
            return true;
        }
        private void Say(String text)
        {
            MessageBox.Show(text, "You.NET messager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void passwordTb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                loginBtn_Click(new object(), new EventArgs());
            }
        }
    }
}
