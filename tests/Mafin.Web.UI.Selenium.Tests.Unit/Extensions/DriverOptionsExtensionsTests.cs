using System.Reflection;
using AutoFixture;
using Mafin.Web.UI.Selenium.Extensions;
using Mafin.Web.UI.Selenium.Tests.Unit.TestDoubles;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Extensions;

public class DriverOptionsExtensionsTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void AddExtension_WhenChromeOptionsPassed_ShouldAddExtension()
    {
        const string extensionFilesFieldName = "extensionFiles";
        var pathToExistingFile = Assembly.GetExecutingAssembly().Location;
        ChromeOptions options = new();

        options.AddExtension(pathToExistingFile);
        var extractedType = options.GetType().BaseType;
        var extracedFieldInfo = extractedType?.GetField(extensionFilesFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        var extractedFieldValue = extracedFieldInfo?.GetValue(options) as List<string>;

        extractedFieldValue.Should().NotBeNull();
        extractedFieldValue.Should().ContainSingle();
        extractedFieldValue.Should().HaveElementAt(0, pathToExistingFile);
    }

    [Fact]
    public void AddExtension_WhenFirefoxOptionsPassed_ShouldAddExtension()
    {
        const string extensionsFieldName = "extensions";
        var pathToExistingFile = Assembly.GetExecutingAssembly().Location;
        FirefoxOptions options = new();

        options.AddExtension(pathToExistingFile);
        var profile = options.Profile;
        var extractedType = profile.GetType();
        var extracedFieldInfo = extractedType.GetField(extensionsFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        var extractedFieldValue = extracedFieldInfo?.GetValue(profile) as Dictionary<string, FirefoxExtension>;

        extractedFieldValue.Should().NotBeNull();
        extractedFieldValue.Should().ContainSingle();
        pathToExistingFile.Should().Contain(extractedFieldValue!.First().Key);
    }

    [Fact]
    public void AddExtension_WhenUnsupportedOptionsPassed_ShouldThrow()
    {
        var pathToExistingFile = Assembly.GetExecutingAssembly().Location;
        SafariOptions options = new();

        Action act = () => options.AddExtension(pathToExistingFile);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void AddArguments_WhenChromeOptionsPassed_ShouldAddArguments()
    {
        const string argumentsFieldName = "arguments";
        List<string> arguments = [_fixture.Create<string>(), _fixture.Create<string>()];
        ChromeOptions options = new();

        options.AddArguments(arguments);
        var extractedType = options.GetType().BaseType;
        var extracedFieldInfo = extractedType?.GetField(argumentsFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        var extractedFieldValue = extracedFieldInfo?.GetValue(options) as List<string>;

        extractedFieldValue.Should().NotBeNull();
        extractedFieldValue.Should().HaveCount(2);
        extractedFieldValue.Should().BeEquivalentTo(arguments);
    }

    [Fact]
    public void AddArguments_WhenFirefoxOptionsPassed_ShouldAddArguments()
    {
        const string argumentsFieldName = "firefoxArguments";
        List<string> arguments = [_fixture.Create<string>(), _fixture.Create<string>()];
        FirefoxOptions options = new();

        options.AddArguments(arguments);
        var extractedType = options.GetType();
        var extracedFieldInfo = extractedType.GetField(argumentsFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        var extractedFieldValue = extracedFieldInfo?.GetValue(options) as List<string>;

        extractedFieldValue.Should().NotBeNull();
        extractedFieldValue.Should().HaveCount(2);
        extractedFieldValue.Should().BeEquivalentTo(arguments);
    }

    [Fact]
    public void AddArguments_WhenSafariOptionsPassed_ShouldAddArguments()
    {
        const string argumentsFieldName = "additionalCapabilities";
        const string argsStorageName = "args";
        List<string> arguments = [_fixture.Create<string>(), _fixture.Create<string>()];
        SafariOptions options = new();

        options.AddArguments(arguments);
        var extractedType = options.GetType().BaseType;
        var extracedFieldInfo = extractedType?.GetField(argumentsFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        var extractedFieldValue = extracedFieldInfo?.GetValue(options) as Dictionary<string, object>;

        extractedFieldValue.Should().NotBeNull();
        extractedFieldValue.Should().ContainSingle();
        extractedFieldValue![argsStorageName].Should().BeEquivalentTo(arguments);
    }

    [Fact]
    public void AddArguments_WhenUnsupportedOptionsPassed_ShouldThrow()
    {
        List<string> arguments = [_fixture.Create<string>(), _fixture.Create<string>()];
        DummyOptions options = new();

        Action act = () => options.AddArguments(arguments);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void AddPreference_WhenChromeOptionsPassed_ShouldAddPreference()
    {
        const string preferencesFieldName = "userProfilePreferences";
        var referenceName = _fixture.Create<string>();
        var referenceValue = _fixture.Create<string>();
        ChromeOptions options = new();

        options.AddPreference(referenceName, referenceValue);
        var extractedType = options.GetType().BaseType;
        var extracedFieldInfo = extractedType?.GetField(preferencesFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        var extractedFieldValue = extracedFieldInfo?.GetValue(options) as Dictionary<string, object>;

        extractedFieldValue.Should().NotBeNull();
        extractedFieldValue.Should().ContainSingle();
        extractedFieldValue![referenceName].Should().BeEquivalentTo(referenceValue);
    }

    [Fact]
    public void AddPreference_WhenFirefoxOptionsPassed_ShouldAddPreference()
    {
        const string preferencesFieldName = "profilePreferences";
        var referenceName = _fixture.Create<string>();
        var referenceValue = _fixture.Create<string>();
        FirefoxOptions options = new();

        options.AddPreference(referenceName, referenceValue);
        var extractedType = options.GetType();
        var extracedFieldInfo = extractedType.GetField(preferencesFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        var extractedFieldValue = extracedFieldInfo?.GetValue(options) as Dictionary<string, object>;

        extractedFieldValue.Should().NotBeNull();
        extractedFieldValue.Should().ContainSingle();
        extractedFieldValue![referenceName].Should().BeEquivalentTo(referenceValue);
    }

    [Fact]
    public void AddPreference_WhenUnsupportedOptionsPassed_ShouldThrow()
    {
        var referenceName = _fixture.Create<string>();
        var referenceValue = _fixture.Create<string>();
        SafariOptions options = new();

        Action act = () => options.AddPreference(referenceName, referenceValue);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}
