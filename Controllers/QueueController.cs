using Azure_Queue_Storage_HW4.Models;
using Azure_Queue_Storage_HW4.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Azure_Queue_Storage_HW4.Controllers
{
    public class QueueController : Controller
    {

        private readonly IQueueService _queueService;
        private readonly IDiscountQueueService _discountQueueService;
        public QueueController(IQueueService queueService, IDiscountQueueService discountQueueService)
        {
            _queueService = queueService;
            _discountQueueService = discountQueueService;
            //_queueService.SendMessageAsync("10");
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> SendDiscount()
        {
            var vm = new DiscountViewModel
            {
                Message = null
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SendDiscount(DiscountViewModel vm)
        {
            await _discountQueueService.SendMessageAsync(vm.Message);
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ReceiveDiscount()
        {
            var message = await _discountQueueService.ReceiveMessageAsync();

            if (message == null)
            {
                message = "No discount yet";
            }
            return View("Index",message);
            
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] string message)
        {
            await _queueService.SendMessageAsync(message);
            return Ok("Message added to queue succesfully!");
        }

        [HttpGet("receive")]
        public async Task<IActionResult> ReceiveMessage()
        {
            var message = await _queueService.ReceiveMessageAsync();
            if (message != null)
            {
                return Ok($"Result Message : {message}");
            }
            return NotFound("No message found in queue!");
        }
    }
}
