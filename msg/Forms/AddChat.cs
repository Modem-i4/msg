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
    public partial class AddChat : Form
    {
        List<int> selectedUsers = new List<int>();
        private DB db;
        public AddChat(DB db)
        {
            this.db = db;
            InitializeComponent();
            LoadUsers();
            selectedUsers.Add(db.UserId);
        }
        private void LoadUsers()
        {
            Users[] users = db.LoadUsers(search.Text, selectedUsers.ToArray());
            usersFlow.Controls.AddRange(users);
            foreach (var c in users)
                c.add.Click += AddUser;
        }
        private void search_TextChanged(object sender, EventArgs e)
        {
            usersFlow.Controls.Clear();
            LoadUsers();
        }

        private void AddUser(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if(btn.Text == "+")
            {
                selectedUsers.Add((int)btn.Tag);
                btn.Text = "-";
            }
            else
            {
                selectedUsers.Remove((int)btn.Tag);
                btn.Text = "+";
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void create_Click(object sender, EventArgs e)
        {
            if(selectedUsers.Count == 1)
                MessageBox.Show("Виберіть принаймні одного учасника чату", "You.NET messager", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if(selectedUsers.Count == 2 )
            {
                if(db.CreatePrivateChat(selectedUsers[1]))
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else 
                    MessageBox.Show("Помилка з'єднання", "You.NET messager", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                var f = new InputForm("Enter group chat name", "create", "");
                f.ShowDialog();
                if(f.DialogResult == DialogResult.OK)
                {
                    if (db.CreatePublicChat(selectedUsers.ToArray(), f.input.Text))
                    {
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else 
                        MessageBox.Show("Помилка з'єднання", "You.NET messager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
