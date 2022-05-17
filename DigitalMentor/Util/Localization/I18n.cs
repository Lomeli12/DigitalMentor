using Dalamud;
using Dalamud.Logging;

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
        var locDir = Services.PluginInterface.GetPluginLocDirectory();
        if (string.IsNullOrWhiteSpace(locDir)) return;
        
        PluginLog.Debug($"Local Dir: {locDir}");
        currentLang.loadLang(locDir);
        enLang.loadLang(locDir);
    }

    public static string localize(string key, params object[] args) {
        var output = currentLang.localize(key, args);
        return output.Equals(key) ? enLang.localize(key, args) : output;
    }
}