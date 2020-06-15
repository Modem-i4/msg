using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using msg.InputForms;

namespace msg
{
    public partial class Chat : UserControl
    {
        DB db;
        public int Id { get; private set; }
        public bool isUnreaded { get; private set; }
        public Chat(int id, String name, int unreaded, DB db)
        {
            InitializeComponent();
            Id = id;
            user.Text = name;
            this.db = db;
            isUnreaded = unreaded > 0;
            this.unreaded.Text = isUnreaded ? Convert.ToString(unreaded) : "";
        }

        private void edit_Click(object sender, EventArgs e)
        {
            var inp = new GropNameEditInput(user.Text);
            if (inp.ShowDialog() == DialogResult.OK)
            {
                if(inp.forEveryoneCheck.Checked)
                {
                    db.EditChatNameForGroup(Id, inp.input.Text);
                }
                else
                {
                    db.EditChatNameForYourself(Id, inp.input.Text);
                }
                user.Text = inp.input.Text;
            }
        }
        private void leave_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are u sure u want to leave this chat?", "You.NET", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if(db.LeaveChat(Id))
                    this.Dispose();
            }
        }
    }
}
