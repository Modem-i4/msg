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
    public partial class ChatInfo : Messages
    {
        public ChatInfo(String text)
        {
            InitializeComponent();
            this.text.Text = text;
        }
    }
}
