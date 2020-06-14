using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace msg
{
    public class DB
    {
        private static String connStr = "Server=herbalife.in.ua;Database=dfbaddbt_msg;port=3306;User Id=dfbaddbt_admin;password=WUJRrtFb64qdaw7;Charset=utf8mb4";
        MySqlConnection conn = new MySqlConnection(connStr);
        MySqlConnection secondThreadConn = new MySqlConnection(connStr);
        public int UserId { get; private set; }
        public int ChatId { get; private set; }
        public String UserLogin { get; private set; }
        // AUTH //
        public bool CheckIfLoginExist(String login)
        {
            if (Convert.ToInt32(new MySqlCommand($"SELECT COUNT(id) FROM `user` WHERE login = '{login}'", conn).ExecuteScalar()) == 0)
                return false;
            return true;
        }
        public void Innit()
        {
            conn.Open();
            secondThreadConn.Open();
            (new Thread(new ThreadStart(ConnectionRestoring))).Start();
        }

        public void ConnectionRestoring()
        {
            while (true)
            {
                Thread.Sleep(10000);
                try
                {
                    conn.Open();
                }
                catch { };
                try
                {
                    secondThreadConn.Open();
                }
                catch { };
            }
        }
        public bool Login(String login, String pass)
        {
            int id = Convert.ToInt32(new MySqlCommand($"SELECT id FROM `user` WHERE (login = '{login}' AND pass = '{pass}')", conn).ExecuteScalar());
            if (id != 0)
            {
                UserId = id;
                UserLogin = login;
                return true;
            }
            return false;
        }
        public bool Register(String login, String pass)
        {
            if (new MySqlCommand($"INSERT INTO `user`(login, pass) VALUES ('{login}', '{pass}')", conn).ExecuteNonQuery() == 0)
                return false;
            if (!Login(login, pass))
                return false;
            return true;
        }
        // AUTH end //
        // LOAD CHATS //
        public MySqlDataReader LoadChats()
        {
            return new MySqlCommand($"SELECT max(msg.time) AS time, chat.chat_id, chat.chat_name, chat.new_msgs FROM `chat` INNER JOIN `msg` ON chat.chat_id = msg.chat_id WHERE (user_id = {UserId}) GROUP BY chat.chat_id ORDER BY time DESC", conn).ExecuteReader();
        }
        // LOAD MSGS //
        public MySqlDataReader LoadMsgs(int chatId)
        {
            ChatId = chatId;
            return new MySqlCommand($"SELECT user.id, user.login, msg.id AS msg_id, msg.text, msg.time FROM `msg` INNER JOIN `user` ON (user.id = sender_id) WHERE (chat_id = {chatId})", conn).ExecuteReader();
        }
        // SEND MSG //
        public bool SendMsg(String text)
        {
            if (new MySqlCommand($"INSERT INTO `msg`(chat_id, sender_id, text) VALUES ({ChatId}, {UserId}, '{text}')", conn).ExecuteNonQuery() == 0
            || new MySqlCommand($"UPDATE `chat` SET new_msgs = new_msgs + 1 WHERE(chat_id = {ChatId} AND user_id != {UserId})", conn).ExecuteNonQuery() == 0
            || !TriggerChattersUpdate(ChatId))
                return false;
            return true;
        }
        // ADD CHAT //
        public MySqlDataReader LoadUsers(String search)
        {
            return new MySqlCommand($"SELECT id, login FROM `user` WHERE (login LIKE '%{search}%' AND id != 0 AND id != {UserId})", conn).ExecuteReader();
        }
        // CREATE CHAT //
        public bool CreatePrivateChat(int participantId)
        {
            int chatId = GetFollowingChatId();
            if (!CreateChat(participantId, chatId, UserLogin) || !CreateChat(UserId, chatId, GetUsernameById(participantId)))
                // створення автоназваних екземплярів чатів для обох учасників
                return false;
            ChatId = chatId;
            InsertInfoMsg(ChatId, $"Чат розпочато");
            return true;
        }
        public bool CreatePublicChat(int[] participantIds, String chatname)
        {
            int chatId = GetFollowingChatId();
            foreach (var id in participantIds)
            {
                if (!CreateChat(id, chatId, chatname))
                    return false;
            }
            ChatId = chatId;
            InsertInfoMsg(ChatId, $"{UserLogin} створив чат!");
            return true;
        }
        public bool CreateChat(int participantId, int chatId, String chatname)
        {
            if (new MySqlCommand($"INSERT INTO `chat`(chat_id, user_id, chat_name, new_msgs) VALUES ({chatId}, {participantId}, '{chatname}', 1)", conn).ExecuteNonQuery() == 0)
                return false;
            return true;
        }
        // extra client actions //
        public int GetParticipantsAmountById(int id)
        {
            return Convert.ToInt32(new MySqlCommand($"SELECT COUNT(user_id) FROM `chat` WHERE chat_id = {id}", conn).ExecuteScalar());
        }
        public bool CheckForNotifications()
        {
            if ((bool)new MySqlCommand($"SELECT hasNewMsg FROM `user` WHERE id = {UserId}", secondThreadConn).ExecuteScalar())
            {
                new MySqlCommand($"UPDATE `user` SET hasNewMsg = FALSE WHERE id = {UserId}", secondThreadConn).ExecuteNonQuery();
                return true;
            }
            return false;
        }
        public bool SetMessagesRead()
        {
            if (new MySqlCommand($"UPDATE `chat` SET new_msgs = 0 WHERE(chat_id = {ChatId} AND user_id = {UserId})", conn).ExecuteNonQuery() == 0)
                return false;
            return true;
        }
        // DELETE MSG //
        public bool DeleteMsg(int id)
        {
            if (new MySqlCommand($"DELETE FROM `msg` WHERE `msg`.`id` = {id}", conn).ExecuteNonQuery() == 0)
                return false;
            return true;
        }
        // EDIT CHATNAME //
        public bool EditChatNameForYourself(int id, String newChatName)
        {
            if (new MySqlCommand($"UPDATE `chat` SET chat_name = '{newChatName}' WHERE (chat_id = {id} AND user_id = {UserId})", conn).ExecuteNonQuery() == 0)
                return false;
            return true;
        }
        public bool EditChatNameForGroup(int id, String newChatName)
        {
            if (new MySqlCommand($"UPDATE `chat` SET chat_name = '{newChatName}' WHERE chat_id = {id}", conn).ExecuteNonQuery() == 0)
                return false;
            return true;
        }
        // LEAVE CHAT //
        public bool LeaveChat(int id)
        {
            if (new MySqlCommand($"DELETE FROM `chat` WHERE (chat_id = {id} AND user_id = {UserId})", conn).ExecuteNonQuery() == 0)
                return false;
            InsertInfoMsg(id, $"{UserLogin} покинув чат");
            return true;
        }

        // COMMON //
        public String GetUsernameById(int id)
        {
            return (String)new MySqlCommand($"SELECT login FROM `user` WHERE id = {id}", conn).ExecuteScalar();
        }
        public int GetFollowingChatId()
        {
            return (int)new MySqlCommand($"SELECT chat_id FROM `chat` ORDER BY chat_id DESC LIMIT 1", conn).ExecuteScalar() + 1;
        }
        public int GetSentMsgId()
        {
            return (int)new MySqlCommand($"SELECT id FROM `msg` ORDER BY id DESC LIMIT 1", conn).ExecuteScalar();
        }
        public int GetNewMessagesAmount()
        {
            return (int)new MySqlCommand($"SELECT new_msgs FROM `chat` WHERE (chat_id = {ChatId} AND user_id = {UserId})", conn).ExecuteScalar();
        }
        private bool InsertInfoMsg(int chatId, String text)
        {
            if (new MySqlCommand($"INSERT INTO `msg`(chat_id, sender_id, text) VALUES ({chatId}, 0, '{text}')", conn).ExecuteNonQuery() == 0
                || TriggerChattersUpdate(chatId))
                return false;
            return true;
        }
        private bool TriggerChattersUpdate(int chatId)
        {
            if (new MySqlCommand($"UPDATE `user`, `chat` SET hasNewMsg = TRUE WHERE(user.id = chat.user_id AND chat.chat_id = {chatId} AND user.id != {UserId})", conn).ExecuteNonQuery() == 0)
                return false;
            return true;
        }
    }
}
