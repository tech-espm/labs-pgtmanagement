using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PGTManagement.Common.WebClient;
using PGTManagement.Gateway.PGTData;
using PGTManagement.Gateway.PGTData.Requests;

namespace PGTManagement.Controllers
{
    public class GroupController : Controller
    {
        private readonly GroupClient _groupClient;

        public GroupController(IGroupClient groupClient)
        {

            _groupClient = (GroupClient)groupClient;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Get(int GroupID)
        {

            var groups = await _groupClient.Get(GroupID);

            return Json(groups);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupRequest request)
        {
            try
            {
                var group = await _groupClient.Post(request);
                return Ok(group);
            }
            catch (WebClientOfTException ex)
            {
                return BadRequest(ex.ErrorResult);
            }
            catch (Exception ex)
            {
                return BadRequest(new Common.WebClient.ObjectResult { StatusCode = 500, Message = ex.Message });
            }
        }
    }
}
