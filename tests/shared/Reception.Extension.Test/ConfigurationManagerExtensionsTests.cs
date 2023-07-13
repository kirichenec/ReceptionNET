using Reception.App.Service;
using System.Collections.ObjectModel;
using System.Configuration;

namespace Reception.Extension.Test
{
    /// <summary>
    /// Class uses testhost.dll.config
    /// </summary>
    public class ConfigurationManagerExtensionsTests
    {
        private readonly ReadOnlyDictionary<string, string> AppSettingsInitData =
            new Dictionary<string, string>
            {
                { "IsBoss", "False" },
                { "IsSystemTheme", "True" },
                { "PingDelay", "15" },
            }.AsReadOnly();


        public ConfigurationManagerExtensionsTests()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Clear();
            AppSettingsInitData.ForEach(x => config.AppSettings.Settings.Add(x.Key, x.Value));
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }


        [Fact]
        public void ConfigurationManagerExtensions_GetAppSettingsParam_ParameterNameIsNull_ShouldNotThrown()
        {
            // Act
            var act = () => ConfigurationManagerExtensions.GetAppSettingsParam(null);

            // Assert
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("IsBoss")]
        public void ConfigurationManagerExtensions_GetAppSettingsParam_ReturnsExpected(string parameterName)
        {
            // Arrange
            var expectedResult = ConfigurationManager.AppSettings[parameterName];

            // Act
            var act = () => ConfigurationManagerExtensions.GetAppSettingsParam(parameterName);

            // Assert
            act().Should().Be(expectedResult);
        }


        [Fact]
        public void ConfigurationManagerExtensions_GetSection_ParameterNameIsNull_ShouldNotThrown()
        {
            // Act
            var act = () => ConfigurationManagerExtensions.GetSection<object>(null);

            // Assert
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData("tokenSettings")]
        public void ConfigurationManagerExtensions_GetSection_ReturnsExpectedOfTypeTokenSection(string sectionName)
        {
            ConfigurationManagerExtensions_GetSection_ReturnsExpectedOfType<TokenSection>(sectionName);
        }

        private static void ConfigurationManagerExtensions_GetSection_ReturnsExpectedOfType<T>(string sectionName)
        {
            // Arrange
            var expectedResult = (T)ConfigurationManager.GetSection(sectionName);

            // Act
            var act = () => ConfigurationManagerExtensions.GetSection<T>(sectionName);

            // Assert
            act().Should().Be(expectedResult);
            act().Should().NotBeNull();
        }


        [Fact]
        public void ConfigurationManagerExtensions_UpdateAppSettingsParam_SourceValueIsNull_ShouldThrow()
        {
            // Arrange
            string value = null;

            // Act
            var act = () => value.UpdateAppSettingsParam("testParam");

            // Assert
            act.Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void ConfigurationManagerExtensions_UpdateAppSettingsParam_ParamNameIsNull_ShouldThrow()
        {
            // Arrange
            string value = "testValue";

            // Act
            var act = () => value.UpdateAppSettingsParam(null);

            // Assert
            act.Should().Throw<ConfigurationErrorsException>();
        }

        [Theory]
        [InlineData(0, 1, "TestInt")]
        [InlineData(0d, 1.1d, "TestDouble")]
        [InlineData(0f, -1.1f, "TestSingle")]
        [InlineData("InitTestValue", "TestValue", "TestString")]
        [InlineData(false, true, "TestBool")]
        public void ConfigurationManagerExtensions_UpdateAppSettingsParam_SavedSuccessfully<T>(
            T initValue, T value, string parameterName)
        {
            // Arrange
            ConfigurationManager.AppSettings[parameterName] = initValue.ToString();
            value.UpdateAppSettingsParam(parameterName);
            var result = ConfigurationManager.AppSettings[parameterName];

            // Assert
            result.Should().NotBe(initValue.ToString());
            result.Should().Be(value.ToString());
        }
    }
}
