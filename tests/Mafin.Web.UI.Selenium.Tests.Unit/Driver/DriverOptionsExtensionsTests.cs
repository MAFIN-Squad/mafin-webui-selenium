using System.Reflection;
using Mafin.Web.UI.Selenium.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace Mafin.Web.UI.Selenium.Tests.Unit.Driver
{
    public class DriverOptionsExtensionsTests
    {
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

            Assert.True(extractedFieldValue != null);
            Assert.True(extractedFieldValue.Count == 1);
            Assert.Equal(extractedFieldValue.First(), pathToExistingFile);
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

            Assert.True(extractedFieldValue != null);
            Assert.True(extractedFieldValue.Count == 1);
            Assert.True(pathToExistingFile.Contains(extractedFieldValue.First().Key));
        }

        [Fact]
        public void AddExtensionToNotValidOptions_ShouldThrowArgumentOutOfRangeException()
        {
            string pathToExistingFile = Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path);
            SafariOptions options = new SafariOptions();

            Assert.Throws<ArgumentOutOfRangeException>(() => options.AddExtension<SafariOptions>(pathToExistingFile));
        }

        [Fact]
        public void AddArgumentsToChromeOptions_ShouldAddArguments()
        {
            var argumentsFieldName = "arguments";
            List<string> arguments = new List<string>() { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            ChromiumOptions options = new ChromeOptions();

            options.AddArguments<ChromiumOptions>(arguments);
            Type extractedType = options.GetType();
            var extracedFieldInfo = extractedType.BaseType.GetField(argumentsFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var extractedFieldValue = (List<string>)extracedFieldInfo.GetValue(options);

            Assert.True(extractedFieldValue != null);
            Assert.True(extractedFieldValue.Count == 2);
            Assert.Equal<List<string>>(extractedFieldValue, arguments);
        }

        [Fact]
        public void AddArgumentsToFirefoxOptions_ShouldAddArguments()
        {
            var argumentsFieldName = "firefoxArguments";
            List<string> arguments = new List<string>() { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            FirefoxOptions options = new FirefoxOptions();

            options.AddArguments<FirefoxOptions>(arguments);
            Type extractedType = options.GetType();
            var extracedFieldInfo = extractedType.GetField(argumentsFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var extractedFieldValue = (List<string>)extracedFieldInfo.GetValue(options);

            Assert.True(extractedFieldValue != null);
            Assert.True(extractedFieldValue.Count == 2);
            Assert.Equal<List<string>>(extractedFieldValue, arguments);
        }

        [Fact]
        public void AddArgumentsToSafariOptions_ShouldAddArguments()
        {
            var argumentsFieldName = "additionalCapabilities";
            var argsStorageName = "args";
            List<string> arguments = new List<string>() { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            SafariOptions options = new SafariOptions();

            options.AddArguments<SafariOptions>(arguments);
            Type extractedType = options.GetType();
            var extracedFieldInfo = extractedType.BaseType.GetField(argumentsFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var extractedFieldValue = (Dictionary<string, object>)extracedFieldInfo.GetValue(options);

            Assert.True(extractedFieldValue != null);
            Assert.True(extractedFieldValue.Count == 1);
            Assert.Equal<List<string>>(arguments, (List<string>)extractedFieldValue[argsStorageName]);
        }

        [Fact]
        public void AddArgumentsToNotValidOptions_ShouldThrowArgumentOutOfRangeException()
        {
            List<string> arguments = new List<string>() { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            FakeOptions options = new FakeOptions();

            Assert.Throws<ArgumentOutOfRangeException>(() => options.AddArguments<FakeOptions>(arguments));
        }

        [Fact]
        public void AddPreferenceToChromeOptions_ShouldAddPreference()
        {
            var preferencesFieldName = "userProfilePreferences";
            var referenceName = Guid.NewGuid().ToString();
            var referenceValue = Guid.NewGuid().ToString();
            ChromiumOptions options = new ChromeOptions();

            options.AddPreference<ChromiumOptions>(referenceName, referenceValue);
            Type extractedType = options.GetType();
            var extracedFieldInfo = extractedType.BaseType.GetField(preferencesFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var extractedFieldValue = (Dictionary<string, object>)extracedFieldInfo.GetValue(options);

            Assert.True(extractedFieldValue != null);
            Assert.True(extractedFieldValue.Count == 1);
            Assert.True(extractedFieldValue[referenceName] == referenceValue);
        }

        [Fact]
        public void AddPreferenceToFirefoxOptions_ShouldAddPreference()
        {
            var preferencesFieldName = "profilePreferences";
            var referenceName = Guid.NewGuid().ToString();
            var referenceValue = Guid.NewGuid().ToString();
            var options = new FirefoxOptions();

            options.AddPreference<FirefoxOptions>(referenceName, referenceValue);
            Type extractedType = options.GetType();
            var extracedFieldInfo = extractedType.GetField(preferencesFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var extractedFieldValue = (Dictionary<string, object>)extracedFieldInfo.GetValue(options);

            Assert.True(extractedFieldValue != null);
            Assert.True(extractedFieldValue.Count == 1);
            Assert.True(extractedFieldValue[referenceName] == referenceValue);
        }

        [Fact]
        public void AddPreferenceToNotValidOptions_ShouldThrowArgumentOutOfRangeException()
        {
            var referenceName = Guid.NewGuid().ToString();
            var referenceValue = Guid.NewGuid().ToString();
            var options = new SafariOptions();

            Assert.Throws<ArgumentOutOfRangeException>(() => options.AddPreference<SafariOptions>(referenceName, referenceValue));
        }
    }

    public class FakeOptions : DriverOptions
    {
        public override ICapabilities ToCapabilities()
        {
            throw new NotImplementedException();
        }
    }
}
