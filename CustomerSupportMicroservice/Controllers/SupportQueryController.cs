using CustomerSupportMicroservice.Interface;
using CustomerSupportMicroservice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CustomerSupportMicroservice.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/customer/support")]
    public class SupportQueryController: ControllerBase
    {
        private readonly ISupportRepository _supportRepository;

        public SupportQueryController(ISupportRepository supportRepository)
        {
            _supportRepository = supportRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddQuery(Support support)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    var emailId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

                    return Ok(await _supportRepository.AddQuery(support.Query, support.BookingId, emailId));
                }

                return Unauthorized(new
                {
                    message = "User is not authorized"
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while processing your request. Please try again!!",
                    details = ex.Message
                });
            }
        }

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetQueryByBookingId(int bookingId)
        {
            try
            {
                return Ok(await _supportRepository.GetSupportDetailsByBookingId(bookingId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while processing your request. Please try again!!",
                    details = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSupportDetailsByUserId()
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    var emailId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                    return Ok(await _supportRepository.GetSupportDetailsByUserId(emailId));
                }

                return Unauthorized(new
                {
                    message = "User is not authorized"
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while processing your request. Please try again!!",
                    details = ex.Message
                });
            }
        }
    }
}
