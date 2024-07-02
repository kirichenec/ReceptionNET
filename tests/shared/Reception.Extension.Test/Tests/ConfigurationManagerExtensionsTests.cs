using Reception.App.Service;
using System.Collections.ObjectModel;
using System.Configuration;

namespace Reception.Extension.Test;

/// <summary>
/// Class uses testhost.dll.config
/// </summary>
public class ConfigurationManagerExtensionsTests
{
    private readonly ReadOnlyDictionary<string, string> AppSettingsInitData =
        new Dictionary<string, string>
        {
            { "IsBoss", "False" },
            { "Language", "English" },
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
    [InlineData("PingDelay", 10)]
    [InlineData("Language", "Russian")]
    [InlineData("IsBoss", true)]
    public void ConfigurationManagerExtensions_UpdateAppSettingsParam_SavedSuccessfully<T>(string parameterName, T value)
    {
        // Arrange
        var initValue = ConfigurationManager.AppSettings[parameterName];
        value.UpdateAppSettingsParam(parameterName);
        var result = ConfigurationManager.AppSettings[parameterName];

        // Assert
        result.Should().NotBe(initValue);
        result.Should().Be(value.ToString());
    }

    [Fact]
    public void ConfigurationManagerExtensions_UpdateSection_SourceValueIsNull_ShouldThrow()
    {
        // Arrange
        TokenSection value = null;

        // Act
        var act = () => value.UpdateSection("tokenSettings");

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ConfigurationManagerExtensions_UpdateSection_ParamNameIsNull_ShouldThrow()
    {
        // Arrange
        var value = new TokenSection();

        // Act
        var act = () => value.UpdateSection(null);

        // Assert
        act.Should().Throw<ConfigurationErrorsException>();
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
