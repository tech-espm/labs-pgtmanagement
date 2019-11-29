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
    public class CampusController : Controller
    {
        private readonly CampusClient _campusClient;

        public CampusController(ICampusClient campusClient)
        {

            _campusClient = (CampusClient)campusClient;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var campus = await _campusClient.GetAll();

            return Json(campus);
        }
    }
}
