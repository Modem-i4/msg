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
    public partial class Inbox : Messages
    {
        public Inbox(String sender, String text, String time)
        {
            InitializeComponent();
            this.text.Text = text;
            this.time.Text = time;
            container.Text = sender;
        }
    }
}
