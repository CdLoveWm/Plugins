using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plugin.Demo.Hangfire.Jobs.Interfaces;
using Plugin.Demo.Hangfire.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Plugin.Demo.Hangfire.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Hangfire()
        {
            return View();
        }
        /// <summary>
        /// 动态更新定时任务执行周期
        /// </summary>
        /// <param name="cron"></param>
        /// <returns></returns>
        [Route("Home/Cron")]
        public bool CronUpdate(string cron)
        {
            // 更新Hangfire的Corn表达式
            RecurringJob.AddOrUpdate<ITestJob>(it => it.Excute(), cron, TimeZoneInfo.Local);
            return true;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
