using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangFire01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangfireController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("tamam test 1 2 ");
        }

        [HttpPost]
        [Route("Welcome")]
        public IActionResult Welcome()
        {
            var JobId=BackgroundJob.Enqueue(() => SendWelcomeEmail("tamam"));
            return Ok($"Job Id: {JobId} SendWelcomeEmail");
        }

        [HttpPost]
        [Route("remember")]
        public IActionResult Remember()
        {
            var JobId = BackgroundJob.Schedule(() => SendWelcomeEmail("tamam"),TimeSpan.FromSeconds(5));
            return Ok($"Job Id: {JobId} SendWelcomeEmail");
        }

        [HttpPost]
        [Route("confirm")]
        public IActionResult confirm()
        {
            var JobId = BackgroundJob.Schedule(() => SendWelcomeEmail("burasi oldu devami gelecek"), TimeSpan.FromSeconds(5));
            BackgroundJob.ContinueJobWith(JobId, () => Console.WriteLine("devami geldi"));
            return Ok("confirmed done ");
        }

        [HttpPost]
        [Route("DbUpdate")]
        public IActionResult DbUpdate()
        {
            RecurringJob.AddOrUpdate(() => Console.WriteLine("updated"), Cron.Minutely);
            //var JobId = BackgroundJob.Schedule(() => SendWelcomeEmail("tamam"), TimeSpan.FromSeconds(5));
            return Ok($"done");
        }
        public void SendWelcomeEmail(string text)
        {
            Console.WriteLine(text);
            
        }
    }
}
