using System.Web.Http;

using WebApplication.Services;

namespace WebApplication.Controllers
{
	public class UserController : ApiController
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		public IHttpActionResult GetUsername()
			=> Ok(_userService.GetUsername());
	}
}