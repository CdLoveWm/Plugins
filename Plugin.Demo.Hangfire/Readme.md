﻿﻿﻿

# **Hangfire**



### 一、使用Hangfire

Hangfire要用到数据存储， 保存它的一些配置数据。 本项目中采用的是MySQL。

1、Nugut引入包：

`Hangfire` 
`Hangfire.MySqlStorage`

2、在Startup中添加配置

ConfigureServices方法中配置如下：

```C#
// connectStr是数据库链接字符串。
string connectStr = Configuration.GetConnectionString("HangfireDemoConnection");
services.AddHangfire(config => {
    config.UseStorage(new MySqlStorage(connectStr, new MySqlStorageOptions()
    {
        // TablesPrefix配置的是Hangfire相关表的表名的前缀，设置后便于区分
        TablesPrefix = "HF_" 
    }));
});
services.AddHangfireServer(); // 添加服务
```

Configure方法中配置如下：

```c#
// 添加管理页面
app.UseHangfireDashboard("/hangfire", new DashboardOptions());
// 配置定时任务，这里配置的是每分钟执行TestJob.Excute()
RecurringJob.AddOrUpdate<ITestJob>(it => it.Excute(), Cron.Minutely, TimeZoneInfo.Local);
```

### 二、动态更新Cron

当我们需要在项目运行期间更改Hangfire的具体任务的执行周期时，同样可以使用`AddOrUpdate` 方法进行更新对应的Cron表达式。 比如在这个项目中添加了一个页面叫Hangfire：

![image-20210923104955815](https://i.loli.net/2021/09/23/tMFi2kKaeuS81mz.png)

在这个页面输入相应的Cron表达式， 点击确定调用接口执行更新Cron的操作。 如下： 

```c#
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
```

以上操作就能达到在程序不停止运行的情况下去更新Cron了。

### 三、生成Cron最近时间列表

这里使用到了一个包， 叫做 `NCrontab`  ，它提供Cron表达式的解析功能等等。 
GitHub地址： https://github.com/atifaziz/NCrontab

我在这只计算最近的5次时间，具体代码如下：

```C#
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
```

