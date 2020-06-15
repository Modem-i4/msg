using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace msg
{
    public partial class Client : Form
    {
        private DB db;
        public Client(DB db)
        {
            this.db = db;
            InitializeComponent();
            login.Text = db.UserLogin;
            ChatsInnit();
            (new Thread(new ThreadStart(CheckForNotifications))).Start();
        }
        private void CheckForNotifications()
        {
            while (true)
            {
                try
                {
                    if (db.CheckForNotifications())
                    {
                        Invoke(new MethodInvoker(ChatsInnit));
                    }
                }
                catch { };
                Thread.Sleep(333);
            }
        }
        private void ChatsInnit()
        {
            chatsLayout.Controls.Clear();
            Chat[] chat = db.LoadChats();
            chatsLayout.Controls.AddRange(chat);
            foreach (Chat ch in chat)
            {
                ch.Click += new EventHandler(openChat);
                if (ch.Id == db.ChatId)
                    openChat(ch, new EventArgs());
            }
        }

        private void openChat(object sender, EventArgs e)
        {
            // local info introduction
            setExtraChatInfo((Chat)sender);
            //fill layout
            msgsLayout.Controls.Clear();
            msgsLayout.Controls.AddRange(db.LoadMsgs((sender as Chat).Id));
            // set scroll to last unreadeds
            int newMsgAm = db.GetNewMessagesAmount();
            msgsLayout.ScrollControlIntoView(msgsLayout.Controls[msgsLayout.Controls.Count - newMsgAm - (newMsgAm == 0 ? 1 : 0)]);
            msgsLayout.Focus();
        }
        private void send_Click(object sender, EventArgs e)
        {
            if(msgBox.Text.Replace(" ","").Replace("\n","").Length > 0)
            {
                if (db.SendMsg(msgBox.Text))
                {
                    msgsLayout.Controls.Add(db.displayNewDateIfRequired(DateTime.Now));
                    var m = new Outbox(msgBox.Text, DateTime.Now.ToString("HH:mm:ss"), db.GetSentMsgId(), db);
                    msgsLayout.Controls.Add(m);
                    msgBox.Clear();
                    msgsLayout.ScrollControlIntoView(m);
                }
                else
                    MessageBox.Show("Перевірте з'єднання");
            }
        }
        private void emoji_Click(object sender, EventArgs e)
        {
            msgBox.AppendText(((Label)sender).Text);
        }

        private void setExtraChatInfo(Chat c)
        {
            if(db.SetMessagesRead())
                c.unreaded.Text = "";
            chatName.Text = c.user.Text;
            int participantsAmount = db.GetParticipantsAmountById(c.Id);
            if (participantsAmount == 2)
                participants.Text = "";
            else
                participants.Text = participantsAmount + " particitants";
        }

        private void msgBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Enter))
            {
                send_Click(new object(), new EventArgs());
            }
        }

        private void addChat_Click(object sender, EventArgs e)
        {
            (new AddChat(db)).ShowDialog();
            ChatsInnit();
        }

        private void attach_Click(object sender, EventArgs e)
        {
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
