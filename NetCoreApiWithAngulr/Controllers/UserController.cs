using AutoMapper;
using Business.Abstract.EntityServices;
using Business.Aspects.AutoFac;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApiWithAngular.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IAuthService authService;
        private IUserService userService;
        private IMapper mapper;
        public UserController(IAuthService authService, IUserService userService,IMapper mapper)
        {
            this.authService = authService;
            this.userService = userService;
            this.mapper = mapper;
        }
        [HttpGet("getlist")]
        public ActionResult GetList()
        {

            var result = userService.GetList();
            if (result.Success)
                return Ok(result.Data.Select(u => mapper.Map<UserListModel>(u)).ToList());
            else
                return BadRequest(result.Message);            
        }
        [HttpGet("get")]
        public ActionResult Get(int userId)
        {
            var result = userService.Get(userId);
            if (result.Success)
                return Ok(result.Data);
            else
                return BadRequest(result.Message);
        }
        [HttpPut("save")]
        public ActionResult Save(UserForRegisterDto userForRegisterDto)
        {
          var result= authService.Register(userForRegisterDto);
            if (result.Success)
                return Ok(result.Data);
            else
                return BadRequest(result.Message);
        }
        [HttpDelete("delete")]
        public ActionResult Delete(int userId)
        {
            var result = userService.Delete(userId);
            if (result.Success)
                return Ok();
            else
                return BadRequest(result.Message);
        }
    }
}
