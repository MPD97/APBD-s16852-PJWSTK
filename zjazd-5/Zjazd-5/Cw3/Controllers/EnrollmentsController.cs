using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw3.Models;
using Cw4.DTOs.Requests;
using Cw4.DTOs.Responses;
using Cw4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentDbService _service;

        public EnrollmentsController(IStudentDbService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            _service.EnrollStudent(request);
            var response = new EnrolStudentResponse();
            //response.LastName = st.LastName;
            //...

            return Ok(response);
        }
    }
}