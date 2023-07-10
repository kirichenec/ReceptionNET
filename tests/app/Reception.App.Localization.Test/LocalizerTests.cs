using FluentAssertions;
using System.Globalization;
using Xunit;

namespace Reception.App.Localization.Test
{
    public class LocalizerTests : IClassFixture<LocalizationFixture>
    {
        private const string NOT_EXISTED_KEY = "!!!";


        private readonly LocalizationFixture _localizationFixture;


        public LocalizerTests(LocalizationFixture localizationFixture)
        {
            _localizationFixture = localizationFixture;
        }


        [Fact]
        public void Localizer_Languages_HasAnyBesidesSystem()
        {
            // Arrange
            var languageKeys = GetLanguageKeys();

            // Assert
            Assert.True(languageKeys.Any());
        }

        [Theory]
        [InlineData(Localizer.ENGLISH)]
        [InlineData(Localizer.RUSSIAN)]
        public void Localizer_Languages_SetSupportedLanguage_NotThrowing(string language)
        {
            // Act
            var act = () => Localizer.Instance.SetLanguage(language);

            // Assert
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData(Localizer.ENGLISH)]
        [InlineData(Localizer.RUSSIAN)]
        public void Localizer_Languages_SetSupportedLanguage_ExpectedLanguage(string language)
        {
            // Act
            Localizer.Instance.SetLanguage(language);

            // Assert
            Assert.Equal(CultureInfo.CurrentUICulture.DisplayName, language, ignoreCase: true);
        }

        [Theory]
        [InlineData(NOT_EXISTED_KEY)]
        public void Localizer_Languages_SetUnsupportedLanguage_NotThrowing(string notSupportedLang)
        {
            // Act
            var act = () => Localizer.Instance.SetLanguage(notSupportedLang);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Localizer_Resources_AllLanguagesHasEqualKeys()
        {
            // Arrange
            var languageKeys = GetLanguageKeys();

            // Act
            var allLanguageKeys = languageKeys
                .Select(key =>
                {
                    Localizer.Instance.SetLanguage(key);
                    return _localizationFixture.GetAllResourcesKeys(key).Order().ToList();
                })
                .ToList();

            // Assert
            Assert.True(allLanguageKeys.All(lk => lk.SequenceEqual(allLanguageKeys.First())));
        }

        [Fact]
        public void Localizer_Resources_AllKeysHasValues()
        {
            // Arrange
            var resourceKeys = _localizationFixture.GetAllResourcesKeys();
            var languageKeys = GetLanguageKeys();

            // Act
            var allResourceValues = new Dictionary<string, List<string>>();
            foreach (var key in languageKeys)
            {
                Localizer.Instance.SetLanguage(key);
                allResourceValues.Add(
                    key, resourceKeys.Select(rKey => Localizer.Instance[rKey]).ToList()
                    );
            }

            // Assert
            Assert.True(allResourceValues.Values.All(l => l.Count == allResourceValues.Values.First().Count));
        }

        [Theory]
        [InlineData(NOT_EXISTED_KEY)]
        public void Localizer_Resources_KeyNotExists_True(string notExistedKey)
        {
            // Arrange
            var keys = _localizationFixture.GetAllResourcesKeys();

            // Assert
            Assert.DoesNotContain(notExistedKey, keys);
        }

        [Theory]
        [InlineData(NOT_EXISTED_KEY)]
        public void Localizer_Resources_LocalizedValueWithNonExistedKey_NotThrowing(string notExistedKey)
        {
            // Act
            Func<string> act = () => Localizer.Instance[notExistedKey];

            // Assert
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData(NOT_EXISTED_KEY)]
        public void Localizer_Resources_LocalizedValueWithNonExistedKey_ReturnsDefault(string notExistedKey)
        {
            // Arrange
            var localizedValue = Localizer.Instance[notExistedKey];

            // Assert
            Assert.False(string.IsNullOrEmpty(localizedValue));
        }


        private static List<string> GetLanguageKeys()
        {
            return Localizer.Languages
                .Where(lang => lang.Key != Localizer.SYSTEM_DEFAULT_LANGUAGE)
                .Select(lang => lang.Key)
                .ToList();
        }
    }
}
