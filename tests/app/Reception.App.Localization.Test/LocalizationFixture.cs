using Reception.App.Localization.Languages;
using System.Globalization;
using System.Resources;

namespace Reception.App.Localization.Test
{
    public class LocalizationFixture
    {
        private readonly ResourceManager _resources;

        public LocalizationFixture()
        {
            _resources = new ResourceManager(typeof(Language));
        }

        public IEnumerable<string> GetAllKeys()
        {
            var resourceSet = _resources
                .GetResourceSet(CultureInfo.CurrentCulture, true, true)!
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
