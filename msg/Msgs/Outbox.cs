using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace msg
{
    public partial class Outbox : Messages
    {
        public int Id { get; set; }
        DB db;
        public Outbox(String text, String time, int id, DB db)
        {
            InitializeComponent();
            this.text.Text = text;
            this.time.Text = time;
            Id = id;
            this.db = db;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Видалити повідомлення?", "You.NET", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                db.DeleteMsg(Id);
                this.Dispose();
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
        }
    }
}
