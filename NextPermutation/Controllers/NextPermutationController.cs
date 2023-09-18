using Microsoft.AspNetCore.Mvc;
using NextPermutation.Core;
using NextPermutation.Models;
using System.Numerics;

namespace NextPermutation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NextPermutationController : ControllerBase
    {
        private readonly IOperations _operations;

        public NextPermutationController(IOperations operations)
        {
            _operations = operations;
        }

        [HttpGet("{vector}")]
        public ActionResult<Response> FindNextpermutation(string vector)
        {

            if (vector != null) { 
                var response = _operations.NextPermutation(vector);
                return Ok(response);
            }
            else { 
                var response = new Response { Code = -1, Message = "Empty entry", Vector = vector, Next = "" };
                return Ok(response);
            }
                        
        }


    }
}