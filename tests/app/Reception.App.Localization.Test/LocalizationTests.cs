using Xunit;

namespace Reception.App.Localization.Test
{
    public class LocalizationTests : IClassFixture<LocalizationFixture>
    {
        private readonly LocalizationFixture _localizationFixture;

        public LocalizationTests(LocalizationFixture localizationFixture)
        {
            _localizationFixture = localizationFixture;
        }

        [Fact]
        public void Localizer_Resources_AllKeysHasValues()
        {
            // Arrange
            var resourceKeys = _localizationFixture.GetAllKeys();
            var languageKeys = Localizer.Languages
                .Where(lang => !string.IsNullOrEmpty(lang.Value))
                .Select(lang => lang.Key)
                .ToList();

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
    }
}
