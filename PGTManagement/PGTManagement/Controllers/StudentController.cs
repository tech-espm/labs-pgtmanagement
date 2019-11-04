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
    public class StudentController : Controller
    {
        private readonly StudentClient _studentClient;

        public StudentController(IStudentClient studentClient)
        {

            _studentClient = (StudentClient)studentClient;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Get(int StudentID)
        {

            var students = await _studentClient.Get(StudentID);

            return Json(students);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentRequest request)
        {
            try
            {
                var student = await _studentClient.Post(request);
                return Ok(student);
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