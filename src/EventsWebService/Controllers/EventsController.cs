using EventsWebService.Dtos;
using EventsWebService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventsWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        [HttpPost]
        [Route("create")]
        public ActionResult Post([FromQuery]EventType type, [FromBody]object content)
        {
            if (content == null)
            {
                return this.BadRequest("Body can not be null.");
            }

            try
            {
                switch (type)
                {
                    case EventType.None:
                        return this.BadRequest("Invalid type");
                    case EventType.FileDownload:
                        var result = new MessageSender().SendEvent(JsonConvert.DeserializeObject<FileDownloadDto>(content.ToString()), type.ToString());
                        if (result.Length > 0)
                        {
                            return this.BadRequest(result);
                        }
                        break;
                    case EventType.UserLogin:
                        var result1 = new MessageSender().SendEvent(JsonConvert.DeserializeObject<UserLoginDto>(content.ToString()), type.ToString());
                        if (result1.Length > 0)
                        {
                            return this.BadRequest(result1);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Error = "Unexpected error.", Message = ex.Message });
            }

            return this.Ok(new { Status = "Event processed successfully", Time = DateTime.UtcNow, ReferenseId = Guid.NewGuid() });
        }

        [HttpDelete]
        [Route("gdpr")]
        public ActionResult DeleteUserData(string userEmail)
        {
            new MessageSender().SendUserDelete(userEmail);

            return this.Ok(new { Status = "User data deleted successfully" });
        }
    }
}