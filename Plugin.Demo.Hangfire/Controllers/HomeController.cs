using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NCrontab;
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

        /// <summary>
        /// Cron 表达式解析
        /// 返回最近运行的时间列表
        /// </summary>
        /// <param name="cron"></param>
        /// <returns></returns>
        public List<string> CronParse(string cron)
        {
            if (string.IsNullOrWhiteSpace(cron))
                return null;
            try
            {
                var parser = CrontabSchedule.Parse(cron);
                List<string> dateList = new List<string>();
                DateTime startTime = DateTime.Now;
                DateTime endTime = DateTime.MaxValue;
                for (int i = 0; i < 5; i++)
                {
                    startTime = parser.GetNextOccurrence(startTime, endTime);
                    dateList.Add(startTime.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                return dateList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
