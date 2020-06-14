using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace msg.InputForms
{
    public partial class GropNameEditInput : Form
    {
        public GropNameEditInput(String currentName)
        {
            InitializeComponent();
            input.Text = currentName;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
