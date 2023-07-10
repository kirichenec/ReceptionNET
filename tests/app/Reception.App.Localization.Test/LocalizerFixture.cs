using Reception.App.Localization.Languages;
using System.Globalization;
using System.Resources;

namespace Reception.App.Localization.Test
{
    public class LocalizerFixture
    {
        private readonly ResourceManager _resources;

        public LocalizerFixture()
        {
            _resources = new ResourceManager(typeof(Language));
        }

        public IEnumerable<string> GetAllResourcesKeys(string? langKey = null)
        {
            var resourceSet = _resources
                .GetResourceSet(
                    culture: string.IsNullOrEmpty(langKey)
                        ? CultureInfo.CurrentCulture
                        : new CultureInfo(Localizer.Languages[langKey]),
                    createIfNotExists: true,
                    tryParents: true)!
                .GetEnumerator();
            while (resourceSet.MoveNext())
            {
                if (resourceSet.Key != null && !string.IsNullOrEmpty(resourceSet.Key.ToString()))
                {
                    yield return resourceSet.Key.ToString()!;
                }
            }
        }

        public ResourceManager GetResourceManager()
        {
            return _resources;
        }
    }
}
