namespace ChatRoomApi.Models
{
    public class Messages
    {
        public string? Code { get; set; }
        public int? MsgId { get; set; }
        public string? Sender { get; set; }
        public string? Message { get; set; }
        public TimeOnly MsgTime { get; set; }

    }
}