using Microsoft.EntityFrameworkCore;
using Plugin.Demo.Hangfire.Models;

namespace Plugin.Demo.Hangfire.Context
{
    /// <summary>
    /// 数据库上下文类
    /// </summary>
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
        }

        public DbSet<JobConfig> JobConfig { get; set; }
    }
}
