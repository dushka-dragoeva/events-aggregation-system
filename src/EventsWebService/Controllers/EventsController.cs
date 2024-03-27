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
            switch (type)
            {
                case EventType.None:
                    return this.BadRequest("Invalid type");
                case EventType.FileDownload:
                    new MessageSender().Send(JsonConvert.DeserializeObject<FileDownloadDto>(content.ToString()), type.ToString());
                    break;
                case EventType.UserLogin:
                    new MessageSender().Send(JsonConvert.DeserializeObject<UserLoginDto>(content.ToString()), type.ToString());
                    break;
                default:
                    break;
            }

            return this.Ok("Success");
        }
    }
}