using Microsoft.AspNetCore.Mvc;
using System;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    /// <summary>
    /// Controller responsible for validating PESEL number
    /// </summary>    
    [Route("api/[controller]")]
    [ApiController]
    public class PeselController : ControllerBase
    {
        /// <summary>
        /// Validate PESEL number
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Pesel?pesel=12312312312
        ///     
        /// </remarks>
        /// <param name="pesel"></param>
        /// <returns>Deleted item</returns>
        /// <response code="200">Returns validation results.
        /// 
        /// If PESEL is VALID, Date of birth and gender is also returned.
        /// If PESEL is INVALID, proper error list is returned.
        /// 
        /// Example of response for VALID PESEL
        /// {
        ///     "pesel": "48100779844",
        ///     "isValid": true,
        ///     "dateOfBirth": "1948-10-07T00:00:00",
        ///     "gender": "Female",
        ///     "errors": []
        /// }
        /// 
        /// Example of response for INVALID PESEL
        /// {
        ///     "pesel": "123",
        ///     "isValid": false,
        ///     "dateOfBirth": null,
        ///     "gender": null,
        ///     "errors": [
        ///         {
        ///             "errorCode": "INVL",
        ///             "errorMessage": "Invalid length. Pesel should have exactly 11 digits."
        ///         }
        ///     ]
        ///  }
        ///  
        /// Possible Error codes and corresponding messages:
        /// "INVL", "Invalid length. Pesel should have exactly 11 digits.
        /// "NBRQ", "Invalid characters. Pesel should be a number."
        /// "INVY", "Invalid year."
        /// "INVM", "Invalid month."
        /// "INVD", "Invalid day."
        /// "INVC", "Check sum is invalid. Check last digit."
        /// 
        /// </response>    
        /// <response code="400">If missing PESEL argument</response>   
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Produces("application/json")]        
        public ActionResult<PeselValidationResponse> Validate(string pesel)
        {
            if (string.IsNullOrEmpty(pesel))
                return BadRequest();

            var service = new PeselValidationService();
            return service.Validate(pesel);
        }
    }
}
