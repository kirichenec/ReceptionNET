using System.Configuration;

namespace Reception.Extension.Test
{
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
        public void ConfigurationManagerExtensions_GetAppSettingsParam_ReturnsExpected(
            string parameterName)
        {
            // Arrange
            ConfigurationManager.AppSettings[parameterName] = "value";

            // Act
            var act = () => ConfigurationManagerExtensions.GetAppSettingsParam(parameterName);

            // Assert
            act().Should().Be(ConfigurationManager.AppSettings[parameterName]);
        }
    }
}
