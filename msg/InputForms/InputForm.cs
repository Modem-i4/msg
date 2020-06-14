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
    public partial class InputForm : Form
    {
        public InputForm(String header, String okButton, String inputDefault)
        {
            InitializeComponent();
            this.header.Text = header;
            this.ok.Text = okButton;
            this.input.Text = inputDefault;
        }

        private void create_Click(object sender, EventArgs e)
        {
            if (input.Text.Length >= 3)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Назва чату повинна містити принаймні 3 символи", "You.NET messager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
