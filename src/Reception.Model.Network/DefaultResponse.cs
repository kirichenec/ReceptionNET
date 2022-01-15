using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reception.Constant;

namespace Reception.Model.Network
{
    public static class DefaultResponse
    {
        public static readonly IActionResult UNAUTHORIZED_RESULT = new JsonResult(new { Message = StatusNames.UNAUTHORIZED }) { StatusCode = StatusCodes.Status401Unauthorized };
    }
}