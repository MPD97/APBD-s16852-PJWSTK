using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cw3.Models;
using Cw4.DTOs.Requests;
using Cw4.DTOs.Responses;
using Cw4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            if (ModelState.IsValid == false)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

                return BadRequest(allErrors);
            }

            var result = _service.EnrollStudent(request);
            if (result.Success == false)
            {
                return BadRequest(result.Message);
            }

            return StatusCode(201, result.Response);
        }
    }
}