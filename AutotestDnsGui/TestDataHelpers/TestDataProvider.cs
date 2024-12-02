using AppiumFramework.Config;

namespace AutotestDnsGui.TestDataHelpers
{
    public static class TestDataProvider
    {
        public static IEnumerable<string> GetCities()
        {
            var config = ConfigManager.GetSection<AutotestDnsGuiTestData>("AutotestDnsGui");
            return config.Cities;
        }

        public static IEnumerable<int> GetMemoryCardCapacity()
        {
            var config = ConfigManager.GetSection<AutotestDnsGuiTestData>("AutotestDnsGui");
            return config.MemoryCardCapacity;
        }
    }

}