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
            var users = await _userClient.GetAll();

            return Json(users);
        }

        public async Task<IActionResult> Get(int UserID)
        {

            var users = await _userClient.Get(UserID);

            return Json(users);
        }

        [HttpPost]
        [Consumes("application/json", "application/json-patch+json", "multipart/form-data")]
        public async Task<IActionResult> Create([FromBody]UserRequest request)
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