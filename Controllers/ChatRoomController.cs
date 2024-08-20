using Microsoft.AspNetCore.Mvc;
using ChatRoomApi.Data;
using ChatRoomApi.Models;

namespace ChatRoomApi.Controllers
{
    [ApiController]
    [Route("api/ChatRoom")]
    public class ChatRoomController : ControllerBase
    {
        

        private readonly ChatRoomData _chatRoomData;
        public ChatRoomController(ChatRoomData chatRoomData)
        {
            _chatRoomData = chatRoomData;
            
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string code)
        {
            var chatMessage = await _chatRoomData.GetChatRooms(code);
            return Ok(chatMessage);
        }

        [HttpGet("JoinChat")]
        public async Task<string> Join([FromQuery] string code)
        {
            var join = await _chatRoomData.JoinChatRoom(code);
            // if(join > 0)
            // {
            //     return Ok(join);
            // }
            // return NotFound();
            return join;
        } 

        [HttpPost("CreateRoom")]
        public async Task<IActionResult> PostCreateRoom([FromBody] CreateChatRoom chat)
        {
            var createdRoom = await _chatRoomData.CreateChatRoom(chat);
            if(createdRoom > 0)
            {
                return Ok(createdRoom);
            }
            return BadRequest();
        }

        [HttpPost("SendMsg")]
        public async Task<IActionResult> PostSendMsg([FromBody] Messages msg)
        {
            var sendMsg = await _chatRoomData.SendMessage(msg); 
            if(sendMsg > 0)
            {
                return Ok(sendMsg);
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMsg([FromQuery] int id)
        {
            var deleteMsg = await _chatRoomData.DeleteMessage(id);
            if(deleteMsg > 0)
            {
                return Ok(deleteMsg);
            }
            return BadRequest();
        }

    }
}