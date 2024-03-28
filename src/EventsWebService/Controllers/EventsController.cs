using EventsWebService.Dtos;
using EventsWebService.Infrastructure;
using EventsWebService.Security;
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
        [ApiKey]
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
                    case EventType.UserLogout:
                        var result5 = new MessageSender().SendEvent(JsonConvert.DeserializeObject<UserLogoutDto>(content.ToString()), type.ToString());
                        if (result5.Length > 0)
                        {
                            return this.BadRequest(result5);
                        }
                        break;
                    case EventType.UserRegistered:
                        var result2 = new MessageSender().SendEvent(JsonConvert.DeserializeObject<UserRegisteredDto>(content.ToString()), type.ToString());
                        if (result2.Length > 0)
                        {
                            return this.BadRequest(result2);
                        }
                        break;
                    case EventType.ProductInstalled:
                        var result3 = new MessageSender().SendEvent(JsonConvert.DeserializeObject<ProductInstalledDto>(content.ToString()), type.ToString());
                        if (result3.Length > 0)
                        {
                            return this.BadRequest(result3);
                        }
                        break;
                    case EventType.ProductUninstalled:
                        var result4 = new MessageSender().SendEvent(JsonConvert.DeserializeObject<ProductUninstalledDto>(content.ToString()), type.ToString());
                        if (result4.Length > 0)
                        {
                            return this.BadRequest(result4);
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
        [ApiKey]
        public ActionResult DeleteUserData(string userEmail)
        {
            new MessageSender().SendUserDelete(userEmail);

            return this.Ok(new { Status = "User data deleted successfully" });
        }
    }
}