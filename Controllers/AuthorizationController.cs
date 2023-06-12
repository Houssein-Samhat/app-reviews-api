using Apps_Review_Api.Interface;
using Apps_Review_Api.Models;
using Apps_Review_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Apps_Review_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly IUserName _userNameRep;
        private readonly DecodingUserPassService _decodeUserPass;
        private readonly CreatingTokenService _createToken;
        public AuthorizationController(IUserName userNameRep, DecodingUserPassService decodeUserPass, CreatingTokenService createToken)
        {
            _userNameRep = userNameRep;
            _decodeUserPass = decodeUserPass;
            _createToken = createToken;
        }

        [HttpPost("login")]
        //[ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody] UserDto request)
        {
            User userN = _userNameRep.IsUserFound(request);
            if (userN != null)
            {
                if(_decodeUserPass.VerifyPasswordHash(request.Password,userN)==true)
                {
                    var token = _createToken.CreateToken(userN);
                    LoginResponse res = new LoginResponse
                    {
                        token = token,
                    };
                    return Ok(res);
                }
                else
                {
                    return BadRequest("Not correct password!!");
                }
            }
            else
            {
                return BadRequest("User Not Found");
            }
        }
    }
}
