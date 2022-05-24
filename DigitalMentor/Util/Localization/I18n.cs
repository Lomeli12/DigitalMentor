using Dalamud;

namespace DigitalMentor.Util.Localization;

public class I18n {
    private static Language currentLang;
    private static Language enLang;

    public static void setupLocalization(DigitalMentor plugin) {
        plugin.config.language = Services.ClientState.ClientLanguage switch {
            ClientLanguage.English => "en",
            ClientLanguage.French => "fr",
            ClientLanguage.German => "ge",
            ClientLanguage.Japanese => "ja",
            _ => "en"
        };
        
        loadLocalization(plugin);
    }

    private static void loadLocalization(DigitalMentor plugin) {
        currentLang = new Language(plugin.config.language);
        enLang = new Language("en");

        var assemblyName = plugin.GetType().Assembly.GetName().Name;
        var locDir = $"{assemblyName}.assets.loc.";
        
        currentLang.loadLang(locDir);
        enLang.loadLang(locDir);
    }

    // Will always fallback to english if it can't find the correct localization file
    public static string localize(string key, params object[] args) {
        var output = currentLang.localize(key, args);
        return output.Equals(key) ? enLang.localize(key, args) : output;
    }
}