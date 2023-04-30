using SuperErp.Management.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperErp.Management.Model;
using SuperErp.Core;
using System.Data;

namespace SuperErp.Management
{
    [AllowAnonymous]
    [ApiController]
    [Route(Endpoints.Accounts.Base)]
    public class AccountController : ControllerBase
    {
        private readonly LoginService loginService;
        private readonly UserRegister userRegister;
        private readonly PasswordChanger passwordChanger;

        public AccountController(LoginService loginService, 
            UserRegister userRegister, 
            PasswordChanger passwordChanger)
        {
            this.loginService = loginService;
            this.userRegister = userRegister;
            this.passwordChanger = passwordChanger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationSuccessResponse>> Authenticate(LoginRequest model)
        {
            string token = await loginService.GetAuthenticationToken(model);

            return Ok(new AuthenticationSuccessResponse()
            {
                Username = model.Email,
                Token = token
            });
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> UpdatePassword(ChangePasswordRequest model)
        {
            await passwordChanger.Change(User, model.CurrentPassword, model.NewPassword);
            return NoContent();
        }


        [HttpGet("error1")]
        public IActionResult Error1()
        {
            throw new Exception("Ups error");
        }

        [HttpGet("error2")]
        public IActionResult Error2()
        {
            throw new ArgumentNullException("my-argument");
        }

        [HttpGet("error3")]
        public IActionResult Error3()
        {
            throw new DomainException("An error in domain ocurred");
        }

        [HttpGet("error4")]
        public IActionResult Error4()
        {
            throw new NotFoundException<Model.User>(17);
        }

        [HttpGet("error5")]
        public IActionResult Error5()
        {
            throw new UnauthorizedAccessException();
        }
    }
}
