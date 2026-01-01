using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerence.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class APIBaseController:ControllerBase
    {
        protected string GetEmailFromToken()=> User.FindFirstValue(ClaimTypes.Email!);
    }
}