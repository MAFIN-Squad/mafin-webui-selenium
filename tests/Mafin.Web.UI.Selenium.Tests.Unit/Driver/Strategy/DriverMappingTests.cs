using Mafin.Web.UI.Selenium.Driver.Strategy;
using Mafin.Web.UI.Selenium.Models;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver.Strategy;

public class DriverMappingTests
{
    public static IEnumerable<object[]> DriverMappings
    {
        get
        {
            yield return new object[] { "chrome", typeof(ChromeStrategy) };
            yield return new object[] { "edge", typeof(EdgeStrategy) };
            yield return new object[] { "safari", typeof(SafariStrategy) };
            yield return new object[] { "firefox", typeof(FirefoxStrategy) };
        }
    }

    [Theory]
    [MemberData(nameof(DriverMappings))]
    public void GetDriverStrategy_WhenPassedWebConfiguration_ShouldReturnCorrespondingDriverStrategy(string driverType, Type strategyType)
    {
        WebConfiguration webConfiguration = new()
        {
            DriverType = driverType
        };

        var driverStrategy = DriverMapping.GetDriverStrategy(webConfiguration);

        driverStrategy.Should().NotBeNull();
        driverStrategy.Should().BeAssignableTo(strategyType);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetDriverStrategy_WhenNullOrEmptyDriverType_ShouldThrow(string driverType)
    {
        WebConfiguration webConfiguration = new()
        {
            DriverType = driverType
        };

        Action act = () => DriverMapping.GetDriverStrategy(webConfiguration);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void GetDriverStrategy_WhenUnsupportedDriverType_ShouldThrow()
    {
        WebConfiguration webConfiguration = new()
        {
            DriverType = "NotValidDriverType"
        };

        Action act = () => DriverMapping.GetDriverStrategy(webConfiguration);

        act.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void GetDriverStrategy_WhenNullWebConfiguration_ShouldThrow()
    {
        Action act = () => DriverMapping.GetDriverStrategy(null!);

        act.Should().Throw<NullReferenceException>();
    }
}
