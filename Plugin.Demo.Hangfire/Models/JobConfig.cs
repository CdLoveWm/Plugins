using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plugin.Demo.Hangfire.Models
{
    /// <summary>
    /// Hangfire 定时任务配置
    /// </summary>
    public class JobConfig
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Corn表达式
        /// </summary>
        public string Corn { get; set; }
    }
}
