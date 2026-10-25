using Microsoft.AspNetCore.Mvc;
using Store.BLL;
using Store.Shared.DTOs.Auth;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthBLL authBLL;
        private readonly ILogger<AuthController> logger;
        public AuthController(AuthBLL authBLL, ILogger<AuthController> logger)
        {
            this.authBLL = authBLL;
            this.logger = logger;
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
        {
            try
            {
                LoginResponse result = authBLL.Login(request);
                if (!result.Success)
                {
                    return Unauthorized(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Lỗi xử lí khi đăng nhập");
                return StatusCode(500, new LoginResponse
                {
                    Success = false,
                    Message = "Máy chủ đang gặp lỗi, vui lòng thử lại"
                });
            }
        }
    }
}
