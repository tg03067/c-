using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using 연습용콘솔앱.Models;
using MssqlTestProject;
using System.Data;

namespace 연습용콘솔앱.Controllers
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class NameController : ControllerBase
    {
        private INameRepository _nameRepo;
        private readonly MssqlLib _mssqlLib = new MssqlLib();

        [HttpGet]
        [Route("test-connection")]
        // DB 연결상태 확인
        public IActionResult TestConntion()
        {
            var result = _mssqlLib.ConnectionTest();
            if(result) return Ok("DB 연결 성공");
            return StatusCode(500, "DB 연결 실패");
        }

        [HttpGet]
        [Route("get-users")]
        // DB 유저 데이터 조회
        public IActionResult GetUsers()
        {
            var users = new List<Object>();

            var ds = _mssqlLib.GetUserInfo();
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                users.Add(new
                {
                    Id = row["id"],
                    Name = row["name"]
                });
            }
            return Ok(users);
        }
        [HttpPost]
        [Route("add-user")]
        // DB 유저 추가
        public IActionResult AddUser([FromBody] User user)
        { 
            _mssqlLib.InsertDB(user.id, user.name);
            return Ok("사용자 추가 완료");
        }

        // ID로 사용자 검색
        [HttpGet]
        [Route("get-one/{id}")]
        public IActionResult GetOneUserById([FromRoute] int id)
        {
            var user = new List<Object>();

            var ds = _mssqlLib.GetOneUserById(id);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                user.Add(new
                {
                    Id = row["id"],
                    Name = row["name"]
                });
            }
                
            return Ok(user);
        }

        // ID로 사용자 검색
        [HttpGet]
        [Route("get-detail")]
        public IActionResult GetOneUserByIdAndName([FromQuery] int id, [FromQuery] string name)
        {
            var user = new List<Object>();

            var ds = _mssqlLib.GetOneUserByIdAndName(id, name);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                user.Add(new
                {
                    Id = row["id"],
                    Name = row["name"]
                });
            }

            return Ok(user);
        }







        public NameController(INameRepository repo) // 저장소 객체 의존성 주입
        {
            _nameRepo = repo;
        }



        [HttpGet]
        public IEnumerable<Name> Get() => _nameRepo.Names;

        [HttpGet("{i}")]
        public Name Get(int i) => _nameRepo[i];

        [HttpPost]
        public Name Post([FromBody] Name name) => 
            _nameRepo.Add(new Name(name.id, name.name));

        //[HttpPost]
        //[Route("add-user")]
        //public IActionResult AddUser([FromQuery] int id, [FromQuery] string name)
        //{
        //    _mssqlLib.InsertDB(id, name);
        //    return Ok("사용자 추가 완료");
        //}

        [HttpPut]
        public Name Put([FromBody] Name name) => _nameRepo.Update(name);

        [HttpPatch("{i}")]
        public StatusCodeResult Patch(int i, [FromBody] JsonPatchDocument<Name> patch)
        {
            Name name = Get(i);

            if(name != null)
            {
                patch.ApplyTo(name);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete]
        public void Delete(int i) => _nameRepo.Delete(i);
    }
}
