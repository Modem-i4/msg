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
    public partial class Users : UserControl
    {
        public Users(int id, String name, bool isSelected)
        {
            InitializeComponent();
            this.name.Text = name;
            add.Tag = id;
            add.Click += AddUserEffects;
            if (isSelected)
            {
                AddUserEffects(new object(), new EventArgs());
                add.Text = "-";
            }
        }
        private void AddUserEffects(object sender, EventArgs e)
        {
            if(add.Text == "+")
            {
                this.BackColor = Color.Pink;
                add.BackColor = Color.Pink;
                add.ForeColor = Color.DeepPink;
            }
            else
            {
                this.BackColor = Color.PaleTurquoise;
                add.BackColor = Color.PaleTurquoise;
                add.ForeColor = Color.Turquoise;
            }
        }
    }
}
