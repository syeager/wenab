using LittleByte.Common.AspNet.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LittleByte.Common.AspNet.Core
{
    [ApiController]
    [Route("[controller]")]
    public abstract class Controller : ControllerBase
    {
        public Guid? UserId => HttpContext.GetUserId();
    }
}
