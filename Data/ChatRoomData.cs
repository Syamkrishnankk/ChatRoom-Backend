using Dapper;
using Npgsql;
using ChatRoomApi.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomApi.Data
{
    public class ChatRoomData
    {
        private readonly NpgsqlConnection _db;
        private readonly ILogger<ChatRoomData> _logger;
        public ChatRoomData(IConfiguration config, ILogger<ChatRoomData> logger)
        {
            _db = new NpgsqlConnection(config.GetConnectionString("DefaultConnection"));
            _logger = logger;
        }
        public async Task<string> GetChatRooms([FromQuery] string msgCode)
        {
            try
            {
                var sql = "select * from chat_room cr inner join messages m on cr.code = m.code and cr.code = @Code";
                var result = await _db.QueryAsync(sql, new { Code = msgCode });
                var chatMessage = result.GroupBy(a => a.code).Select(s => new{
                    Code = s.Key,
                    Owner = s.First().owner,
                    Messages = s.Select(m => new{
                        MsgId = m.msg_id,
                        Sender = m.sender,
                        Message = m.message,
                        Time = m.msg_time
                    }).ToList()
                }).ToList();
                string json = JsonSerializer.Serialize(chatMessage);
                return json;
            }
            catch(Exception ex)
            {
                _logger.LogInformation("{0}",ex);
                return ex.Message;
            }
        }

        public async Task<string> JoinChatRoom([FromQuery] string code)
        {
            var sql = "select * from chat_room where code = @Code";
            var JoinChat = await _db.QueryAsync(sql, new{ Code = code });
            string json = JsonSerializer.Serialize(JoinChat);
            return json;
        }

        public async Task<int> CreateChatRoom(CreateChatRoom chat)
        {
            var sql = "Insert into chat_room(code,owner) values (@Code,@Owner) on conflict do nothing";
            var roomCreated = await _db.ExecuteAsync(sql, new{
                chat.Code,
                chat.Owner
            });
            return roomCreated;
        }

        public async Task<int> SendMessage(Messages msg)
        {
            var sql = "insert into messages(code, sender, message, msg_time) values(@code, @Sender, @Message, @MsgTime)";
            var msgSend = await _db.ExecuteAsync(sql, new{
                msg.Code,
                msg.Sender,
                msg.Message,
                MsgTime = DateTime.Now
            });
            return msgSend;
        }

        public async Task<int> DeleteMessage(int id)
        {
            var sql = "delete from messages where msg_id = @Id";
            var msgDelete = await _db.ExecuteAsync(sql, new { Id = id });
            return msgDelete;
        }
    }
}