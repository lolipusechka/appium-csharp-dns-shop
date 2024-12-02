namespace AppiumFramework.Config
{
    public class AppiumConfig
    {
        public string URL { get; set; }
        public bool AutoGrantPermissions { get; set; }
        public long TimeOut { get; set; }
    }

    public class DnsGuiConfig
    {
        public string Package { get; set; }
    }

    public class AutotestDnsGuiTestData
    {
        public string[] Cities { get; set; }
        public int[] MemoryCardCapacity { get; set; }
    }

    public class RootConfig
    {
        public AppiumConfig AppiumConfig { get; set; }
        public DnsGuiConfig DnsGuiConfig { get; set; }
        public AutotestDnsGuiTestData AutotestDnsGuiTestData { get; set; }
    }
}