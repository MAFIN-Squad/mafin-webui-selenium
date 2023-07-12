using System.Reflection;
using AutoFixture;
using FluentAssertions;
using Mafin.Web.UI.Selenium.Driver;
using Mafin.Web.UI.Selenium.Tests.Unit.Stubs;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver
{
    public class DriverOptionsExtensionsTests
    {
        private readonly Fixture _fixture = new();

        [Fact]
        public void AddExtensionToChromeOptions_ShouldAddExtension()
        {
            string pathToExistingFile = Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path);
            var extensionFilesFieldName = "extensionFiles";
            ChromiumOptions options = new ChromeOptions();

            options.AddExtension<ChromiumOptions>(pathToExistingFile);
            Type extractedType = options.GetType();
            var extracedFieldInfo = extractedType.BaseType.GetField(extensionFilesFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var extractedFieldValue = (List<string>)extracedFieldInfo.GetValue(options);

            extractedFieldValue.Should().NotBeNull();
            extractedFieldValue.Count.Should().Be(1);
            extractedFieldValue.First().Should().Be(pathToExistingFile);
        }

        [Fact]
        public void AddExtensionToFirefoxOptions_ShouldAddExtension()
        {
            string pathToExistingFile = Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path);
            var extensionsFieldName = "extensions";
            FirefoxOptions options = new FirefoxOptions();

            options.AddExtension<FirefoxOptions>(pathToExistingFile);
            FirefoxProfile profile = options.Profile;
            Type extractedType = profile.GetType();
            var extracedFieldInfo = extractedType.GetField(extensionsFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var extractedFieldValue = (Dictionary<string, FirefoxExtension>)extracedFieldInfo.GetValue(profile);

            extractedFieldValue.Should().NotBeNull();
            extractedFieldValue.Count().Should().Be(1);
            pathToExistingFile.Should().Contain(extractedFieldValue.First().Key);
        }

        [Fact]
        public void AddExtensionToNotValidOptions_ShouldThrowArgumentOutOfRangeException()
        {
            string pathToExistingFile = Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path);
            SafariOptions options = new SafariOptions();

            Action act = () => options.AddExtension<SafariOptions>(pathToExistingFile);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void AddArgumentsToChromeOptions_ShouldAddArguments()
        {
            var argumentsFieldName = "arguments";
            List<string> arguments = new List<string>() { _fixture.Create<string>(), _fixture.Create<string>() };
            ChromiumOptions options = new ChromeOptions();

            options.AddArguments<ChromiumOptions>(arguments);
            Type extractedType = options.GetType();
            var extracedFieldInfo = extractedType.BaseType.GetField(argumentsFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var extractedFieldValue = (List<string>)extracedFieldInfo.GetValue(options);

            extractedFieldValue.Should().NotBeNull();
            extractedFieldValue.Count.Should().Be(2);
            extractedFieldValue.Should().BeEquivalentTo(arguments);
        }

        [Fact]
        public void AddArgumentsToFirefoxOptions_ShouldAddArguments()
        {
            var argumentsFieldName = "firefoxArguments";
            List<string> arguments = new List<string>() { _fixture.Create<string>(), _fixture.Create<string>() };
            FirefoxOptions options = new FirefoxOptions();

            options.AddArguments<FirefoxOptions>(arguments);
            Type extractedType = options.GetType();
            var extracedFieldInfo = extractedType.GetField(argumentsFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var extractedFieldValue = (List<string>)extracedFieldInfo.GetValue(options);

            extractedFieldValue.Should().NotBeNull();
            extractedFieldValue.Count.Should().Be(2);
            extractedFieldValue.Should().BeEquivalentTo(arguments);
        }

        [Fact]
        public void AddArgumentsToSafariOptions_ShouldAddArguments()
        {
            var argumentsFieldName = "additionalCapabilities";
            var argsStorageName = "args";
            List<string> arguments = new List<string>() { _fixture.Create<string>(), _fixture.Create<string>() };
            SafariOptions options = new SafariOptions();

            options.AddArguments<SafariOptions>(arguments);
            Type extractedType = options.GetType();
            var extracedFieldInfo = extractedType.BaseType.GetField(argumentsFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var extractedFieldValue = (Dictionary<string, object>)extracedFieldInfo.GetValue(options);

            extractedFieldValue.Should().NotBeNull();
            extractedFieldValue.Count.Should().Be(1);
            extractedFieldValue[argsStorageName].Should().BeEquivalentTo(arguments);
        }

        [Fact]
        public void AddArgumentsToNotValidOptions_ShouldThrowArgumentOutOfRangeException()
        {
            List<string> arguments = new List<string>() { _fixture.Create<string>(), _fixture.Create<string>() };
            FakeOptions options = new FakeOptions();

            Action act = () => options.AddArguments<FakeOptions>(arguments);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void AddPreferenceToChromeOptions_ShouldAddPreference()
        {
            var preferencesFieldName = "userProfilePreferences";
            var referenceName = _fixture.Create<string>();
            var referenceValue = _fixture.Create<string>();
            ChromiumOptions options = new ChromeOptions();

            options.AddPreference<ChromiumOptions>(referenceName, referenceValue);
            Type extractedType = options.GetType();
            var extracedFieldInfo = extractedType.BaseType.GetField(preferencesFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var extractedFieldValue = (Dictionary<string, object>)extracedFieldInfo.GetValue(options);

            extractedFieldValue.Should().NotBeNull();
            extractedFieldValue.Count.Should().Be(1);
            extractedFieldValue[referenceName].Should().BeEquivalentTo(referenceValue);
        }

        [Fact]
        public void AddPreferenceToFirefoxOptions_ShouldAddPreference()
        {
            var preferencesFieldName = "profilePreferences";
            var referenceName = _fixture.Create<string>();
            var referenceValue = _fixture.Create<string>();
            var options = new FirefoxOptions();

            options.AddPreference<FirefoxOptions>(referenceName, referenceValue);
            Type extractedType = options.GetType();
            var extracedFieldInfo = extractedType.GetField(preferencesFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var extractedFieldValue = (Dictionary<string, object>)extracedFieldInfo.GetValue(options);

            extractedFieldValue.Should().NotBeNull();
            extractedFieldValue.Count().Should().Be(1);
            extractedFieldValue[referenceName].Should().BeEquivalentTo(referenceValue);
        }

        [Fact]
        public void AddPreferenceToNotValidOptions_ShouldThrowArgumentOutOfRangeException()
        {
            var referenceName = _fixture.Create<string>();
            var referenceValue = _fixture.Create<string>();
            var options = new SafariOptions();

            Action act = () => options.AddPreference<SafariOptions>(referenceName, referenceValue);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
