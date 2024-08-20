namespace ChatRoomApi.Models
{
    public class ChatRoom
    {
        public string? Code { get; set; }
        public string? Owner { get; set; }
        public List<Messages>? Messages { get; set; }
    }
}