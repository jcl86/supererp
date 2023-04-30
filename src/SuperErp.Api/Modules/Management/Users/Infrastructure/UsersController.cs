using AutoMapper;
using SuperErp.Management.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperErp.Core;

namespace SuperErp.Management.Api
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Policies.IsSuperAdministrator)]
    public class UsersController : ControllerBase
    {
        private readonly UserLister userLister;
        private readonly UserFinder userFinder;
        private readonly UserEraser userEraser;
        private readonly UserRegister userRegister;
        private readonly IMapper mapper;

        public UsersController(
            UserLister userLister,
            UserFinder userFinder,
            UserEraser userEraser, 
            UserRegister userRegister,
            IMapper mapper)
        {
            this.userLister = userLister;
            this.userFinder = userFinder;
            this.userEraser = userEraser;
            this.userRegister = userRegister;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model.User>>> GetAll()
        {
            var result = await userLister.GetAll();
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<Model.User>> GetById(string userId)
        {
            var entity = await userFinder.Find(userId);
            var result = entity.ToModel();
            return result;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Model.User>> Register(Model.RegisterUser model)
        {
            var user = await userRegister.Create(model);
            return Ok(user.ToModel());
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            await userEraser.Delete(userId);
            return NoContent();
        }
    }
}
