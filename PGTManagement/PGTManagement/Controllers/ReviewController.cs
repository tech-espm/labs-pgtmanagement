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
    public class ReviewController : Controller
    {
        private readonly ReviewClient _reviewClient;

        public ReviewController(IReviewClient reviewClient)
        {
            _reviewClient = (ReviewClient)reviewClient;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Get(int ReviewID)
        {

            var reviews = await _reviewClient.Get(ReviewID);

            return Json(reviews);
        }

        [HttpPost]
        [Consumes("application/json", "application/json-patch+json", "multipart/form-data")]
        public async Task<IActionResult> Post([FromBody]ReviewRequest request)
        {

            try
            {
                var review = await _reviewClient.Post(request);
                return Ok(review);
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