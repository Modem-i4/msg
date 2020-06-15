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
        private Thread notificationChecker;
        private Auth auth;
        public Client(DB db, Auth auth)
        {
            this.db = db;
            InitializeComponent();
            login.Text = db.UserLogin;
            ChatsInnit();
            notificationChecker = new Thread(new ThreadStart(CheckForNotifications));
            notificationChecker.Start();
            this.auth = auth;
            auth.Hide();
        }
        private void CheckForNotifications()
        {
            while (true)
            {
                try
                {
                    if (db.CheckForNotifications())
                    {
                        (new System.Media.SoundPlayer(Properties.Resources.Notification)).Play();
                        Invoke(new MethodInvoker(ChatsInnit));
                    }
                }
                catch { };
                Thread.Sleep(333);
            }
        }
        private void ChatsInnit()
        {
            Chat[] chat = db.LoadChats();
            foreach (Chat ch in chat)
            {
                ch.Click += new EventHandler(clearChat) + new EventHandler(openChat);
                if (ch.Id == db.ChatId && ch.isUnreaded)
                {
                    openChat(ch, new EventArgs());
                    return;
                }
            }
            chatsLayout.Controls.Clear();
            chatsLayout.Controls.AddRange(chat);
        }

        private void clearChat(object sender, EventArgs e)
        {
            msgsLayout.Controls.Clear();
        }
        private void openChat(object sender, EventArgs e)
        {
            // local info introduction
            setExtraChatInfo((Chat)sender);
            msgBox.Clear();
            //fill layout
            Messages[] msg = db.LoadMsgs((sender as Chat).Id);
            int newMsgAm = db.GetNewMessagesAmount();
            for(int i = msgsLayout.Controls.Count; i < msg.Length; i++)
            {
                msgsLayout.Controls.Add(msg[i]);
            }
            // set scroll to last unreadeds
            msgsLayout.ScrollControlIntoView(msgsLayout.Controls[msgsLayout.Controls.Count - newMsgAm - (newMsgAm == 0 ? 1 : 0)]);
            msgBox.Focus();
        }
        private void send_Click(object sender, EventArgs e)
        {
            msgBox.Text = msgBox.Text.Trim('\n');
            if (msgBox.Text.Replace(" ","").Length > 0)
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
            msgBox.Text = msgBox.Text.Trim('\n');
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

        private void Client_FormClosed(object sender, FormClosedEventArgs e)
        {
            notificationChecker.Abort();
            auth.Show();
        }
    }
}
