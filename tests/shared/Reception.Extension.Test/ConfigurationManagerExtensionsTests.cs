using Reception.App.Service;
using System.Configuration;

namespace Reception.Extension.Test
{
    /// <summary>
    /// Class uses testhost.dll.config
    /// </summary>
    public class ConfigurationManagerExtensionsTests
    {
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
    }
}
