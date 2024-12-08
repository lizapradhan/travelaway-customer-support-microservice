using CustomerSupportMicroservice.Interface;
using CustomerSupportMicroservice.Models;
using CustomerSupportMicroservice.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSupportMicroservice.Controllers
{
    [Route("api/support")]
    [ApiController]
    public class CustomerServiceController : ControllerBase
    {
        private readonly ISupportRepository _supportRepository;

        public CustomerServiceController(ISupportRepository supportRepository)
        {
            _supportRepository = supportRepository;
        }

        [HttpPut("reply")]
        public async Task<IActionResult> ReplyToCustomerRequest(SupportReply reply)
        {
            try
            {
                var support = new Support
                {
                    CustomerSupportId = reply.SupportId,
                    Reply = reply.Reply
                };
                await _supportRepository.AddReplyToCustomerQuery(reply.Reply, reply.SupportId);
                return Ok("Replied to customer query");
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

        [HttpPut("close")]
        public async Task<IActionResult> CloseCustomerSupportTicket(int supportId)
        {
            try
            {
                await _supportRepository.CloseSupportTicket(supportId);
                return Ok("Support ticket closed");
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
