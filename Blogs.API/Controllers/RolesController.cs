using Blogs.Application.Services.Roles;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.API.Controllers
{
    [ApiController]
    public class RolesController:ControllerBase
    {
        private readonly IRoleservice _roleservice;
        public RolesController(IRoleservice roleservice)
        {
                _roleservice = roleservice;
        }


    }
}
