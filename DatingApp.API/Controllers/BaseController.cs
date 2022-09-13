using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    // Route("api/[controller]") atribute cấu hình router của controller [controller] sẽ được thay thế bằng tên controller
    // Ví dụ: DemoControler thì controller sẽ là Demo
    // ApiController: attribute đánh dấu cho biết đây là Api controller trong ASP .NET core
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}