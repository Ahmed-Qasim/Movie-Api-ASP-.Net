using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetflexProject.DTO;
using NetflexProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetflexProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NetflexDB _context;
        private readonly IConfiguration _config;

        public UsersController(NetflexDB context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }




        // GET: api/Users/5
        [HttpPost("Login")]
        public async Task<ActionResult> GetUserAsync([FromBody] UserLoginDTO user)
        {
            UserLoginDTO userLoginDTO = new UserLoginDTO();
            bool emailExists = _context.Users.Any(u => u.Email.ToLower() == user.UserEmail.ToLower());
            bool passwordExists = _context.Users.Any(u => u.Password == user.UserPassword);
            string UserName = _context.Users.Where(u => u.Email.ToLower() == user.UserEmail.ToLower())
                                            .Select(u => u.Fname).FirstOrDefault();
            if (emailExists)
            {
                if (passwordExists)
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var claims = new List<Claim>()
                    {
                        new Claim("email", user.UserEmail)
                    };

                    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                       _config["Jwt:Audience"],
                       claims,
                       expires: DateTime.Now.AddMinutes(15),
                       signingCredentials: credentials);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        UserName,
                        //expires = token.ValidTo

                    });

                }
                else
                {
                    return BadRequest("Password is Wrong");
                }
            }
            else
            {

                return Unauthorized("Email doesn't exist");


            }

        }


        [HttpPost]
        public async Task<ActionResult> createUser(UserSubscriptionDTO userdto)
        {
            User user = new User();
            List<Subscription> sub = new List<Subscription>();
            //userdto.SubscriptionType = sub.Where(n => n.SubID == user.SubscriptionID).Select(n => n.Type).FirstOrDefault();\
            //user.SubscriptionID = sub.Where(n => n.Type == userdto.SubscriptionType).Select(n => n.SubID).FirstOrDefault();
            bool emailExists = _context.Users.Any(u => u.Email == userdto.Email);

            if (emailExists)
            {
                return BadRequest("This is Email is already Exist");
            }
            else
            {
                user.Fname = userdto.Fname;
                user.Lname = userdto.Lname;
                user.Email = userdto.Email;
                user.Password = userdto.Password;
                user.SubscriptionID = userdto.SubscriptionId;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok();

            }

        }


    }
}
