using Microsoft.AspNetCore.Mvc;
using SSMWarehouseManagement.Data.Models;
using SSMWarehouseManagement.Repositories;

namespace SSMWarehouseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private AuthenticationRepository _authenticationRepository;

        public AuthenticationController(AuthenticationRepository authenticationRepository)
        {
            this._authenticationRepository = authenticationRepository;
        }

        [Route("[action]")]
        [HttpGet]
        public Usuario Autenticar(string user, string pw)
        {
            return this._authenticationRepository.AutenticarUsuario(user, pw);
        }
    }
}