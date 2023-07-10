/*
    Based on localization for AmplitudeSoundboard
    https://github.com/dan0v/AmplitudeSoundboard/tree/master/Localization
*/

using Reception.App.Localization.Languages;
using Reception.Extension;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace Reception.App.Localization
{
    public class Localizer : INotifyPropertyChanged
    {
        public const string FALLBACK_LANGUAGE = "English";
        public const string SYSTEM_DEFAULT_LANGUAGE = "System default";

        private const string INDEXER_NAME = "Item";
        private const string INDEXER_ARRAY_NAME = $"{INDEXER_NAME}[]";

        private ResourceManager _resources;

        private Localizer()
        {
            LoadLanguage();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly ImmutableDictionary<string, string> Languages =
            new Dictionary<string, string>()
            {
                { SYSTEM_DEFAULT_LANGUAGE, string.Empty },
                { FALLBACK_LANGUAGE, "en" },
                { "Русский", "ru" },
            }.ToImmutableDictionary();

        public static Localizer Instance { get; set; } = new Localizer();

        public string this[string key] => GetValueOrDefault(key);

        public void SetLanguage(string language)
        {
            if (string.IsNullOrEmpty(language) || !Languages.ContainsKey(language))
            {
                language = TryUseSystemLanguageFallbackEnglish();
            }

            CultureInfo.CurrentUICulture = new CultureInfo(Languages[language]);
            LoadLanguage();
        }

        private string GetValueOrDefault(string key)
        {
            var ret = _resources?.GetString(key)?.Replace(@"\\n", "\n");
            return ret.IsNullOrEmpty() ? $"Localize:{key}" : ret;
        }

        private void Invalidate()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(INDEXER_NAME));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(INDEXER_ARRAY_NAME));
        }

        private void LoadLanguage()
        {
            _resources = new ResourceManager(typeof(Language));
            Invalidate();
        }

        private static string TryUseSystemLanguageFallbackEnglish()
        {
            string curLang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var fullLanguage = GetInverseLanguages().TryGetValue(curLang, out var fullLang)
                ? fullLang
                : FALLBACK_LANGUAGE;

            return fullLanguage;
        }

        private static Dictionary<string, string> GetInverseLanguages()
        {
            return Languages.ToDictionary(l => l.Value, l => l.Key);
        }
    }
}
