using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PGTManagement.Common.WebClient;
using PGTManagement.Gateway.PGTData;
using PGTManagement.Gateway.PGTData.Requests;
using ObjectResult = PGTManagement.Common.WebClient.ObjectResult;

namespace PGTManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly UserClient _userClient;

        public UserController(IUserClient userClient)
        {

            _userClient = (UserClient)userClient;

        }

        public ViewResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _userClient.GetAll();

            return Json(usuarios);
        }

        public async Task<IActionResult> Get(int UserID)
        {

            var usuarios = await _userClient.Get(UserID);

            return Json(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRequest request)
        {
            try
            {
                var user = await _userClient.Post(request);
                return Ok(user);
            }
            catch (WebClientOfTException ex)
            {
                return BadRequest(ex.ErrorResult);
            }
            catch (Exception ex)
            {
                return BadRequest(new ObjectResult { StatusCode = 500, Message = ex.Message });
            }
        }
    }
}