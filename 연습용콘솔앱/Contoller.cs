//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace 연습용콘솔앱
//{
//    internal class Contoller
//    {
//        [ApiController]
//        [Route("[api/contoller]")]
//        public class UserController : ControllerBase
//        {
//            [HttpGet]
//            public IActionResult GetUserInfo()
//            {
//                var users = new List<User>
//                {
//                    new User {Id = 1, Name = "Alice"},
//                    new User {Id = 2, Name = "Bob" }
//                };

//                for(int i = 0; i < users.Count; i++)
//                {
//                    if (users[i].Name is null)
//                    {
//                        throw new Exception("이름이 포함되어있어야합니다.");
//                    }
//                }
//                return Ok(users);
//            }
//        }
//        public class User
//        {
//            public int Id { get; set; }
//            public string Name { get; set; }
//        }
//    }
//}
