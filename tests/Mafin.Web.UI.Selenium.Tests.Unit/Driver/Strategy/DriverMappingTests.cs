using System.IO;
using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy
{
    public class DriverMappingTests
    {
        public static List<object[]> DriverMappings => new List<object[]>
        {
            new object[] { "chrome", typeof(ChromeStrategy) },
            new object[] { "edge", typeof(EdgeStrategy) },
            new object[] { "safari", typeof(SafariStrategy) },
            new object[] { "firefox", typeof(FirefoxStrategy) }
        };

        [Theory]
        [MemberData(nameof(DriverMappings))]
        public void GetDriverStrategy_ShouldReturn_NotNull_SpecificDriverStrategy(string driverType, Type strategyType)
        {
            var webConfiguration = new WebConfiguration();
            webConfiguration.DriverType = driverType;
            var driverStrategy = DriverMapping.GetDriverStrategy(webConfiguration);

            Assert.NotNull(driverStrategy);
            Assert.IsAssignableFrom(strategyType, driverStrategy);
        }

        [Fact]
        public void GetDriverStrategy_NullDriverType_ThrowsArgumentNullException()
        {
            var webConfiguration = new WebConfiguration();

            Assert.Throws<ArgumentNullException>(() => DriverMapping.GetDriverStrategy(webConfiguration));
        }

        [Fact]
        public void GetDriverStrategy_EmptyDriverType_ThrowsArgumentNullException()
        {
            var webConfiguration = new WebConfiguration();
            webConfiguration.DriverType = string.Empty;

            Assert.Throws<ArgumentNullException>(() => DriverMapping.GetDriverStrategy(webConfiguration));
        }

        [Fact]
        public void GetDriverStrategy_NotValidDriverType_ThrowsKeyNotFoundException()
        {
            var webConfiguration = new WebConfiguration();
            webConfiguration.DriverType = "NotValidDriverType";

            Assert.Throws<KeyNotFoundException>(() => DriverMapping.GetDriverStrategy(webConfiguration));
        }

        [Fact]
        public void GetDriverStrategy_NullWebConfigurationArgument_ThrowsNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => DriverMapping.GetDriverStrategy(null));
        }
    }
}
