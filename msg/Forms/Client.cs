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
        private int lastMsgDate = 0;
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
            var reader = db.LoadChats();
            while(reader.Read())
            {
                var ch = new Chat(reader.GetInt32("chat_id"), reader.GetString("chat_name"), reader.GetInt32("new_msgs"), db);
                chatsLayout.Controls.Add(ch);
                ch.Click += new EventHandler(openChat);
            }
            reader.Close();
            foreach (Chat ch in chatsLayout.Controls)
                if (ch.Id == db.ChatId)
                    openChat(ch, new EventArgs());
        }

        private void openChat(object sender, EventArgs e)
        {
            setExtraChatInfo((Chat)sender);
            //fill layout
            var reader = db.LoadMsgs((sender as Chat).Id);
            msgsLayout.Controls.Clear();
            Messages m;
            while (reader.Read())
            {
                int senderId = reader.GetInt32("id");
                String senderLogin = reader.GetString("login");
                String msgText = reader.GetString("text");
                DateTime time = reader.GetDateTime("time");
                displayNewDateIfRequired(time);
                if (senderId == db.UserId)
                {
                    m = new Outbox(msgText, time.ToString("HH:mm:ss"), reader.GetInt32("msg_id"), db);
                }
                else if(senderId == 0)
                {
                    m = new ChatInfo(msgText);
                }
                else
                {
                    m = new Inbox(senderLogin, msgText, time.ToString("HH:mm:ss"));
                    m.Anchor = AnchorStyles.Right;
                }
                msgsLayout.Controls.Add(m);
            }
            reader.Close();
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
                    displayNewDateIfRequired(DateTime.Now);
                    var m = new Outbox(msgBox.Text, DateTime.Now.ToString("HH:mm:ss"), db.GetSentMsgId(), db);
                    msgsLayout.Controls.Add(m);
                    msgBox.Clear();
                    msgsLayout.ScrollControlIntoView(m);
                }
                else
                    MessageBox.Show("Перевірте з'єднання");
            }
        }
        private void displayNewDateIfRequired(DateTime date)
        {
            if (lastMsgDate != date.DayOfYear)
            {
                msgsLayout.Controls.Add(new ChatInfo(date.ToString("dddd, dd MMMM")));
            }
            lastMsgDate = date.DayOfYear;
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
