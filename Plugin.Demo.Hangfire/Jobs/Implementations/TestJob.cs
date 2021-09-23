using Plugin.Demo.Hangfire.Jobs.Interfaces;

namespace Plugin.Demo.Hangfire.Jobs.Implementations
{
    public class TestJob : ITestJob
    {
        public void Excute()
        {
            System.Console.WriteLine("Excuted");
        }
    }
}
