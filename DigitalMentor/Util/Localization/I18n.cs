using Dalamud;
using Dalamud.Logging;

namespace DigitalMentor.Util.Localization;

public class I18n {
    private static Language currentLang;

    public static void setupLocalization(DigitalMentor plugin) {
        plugin.config.language = Services.ClientState.ClientLanguage switch {
            ClientLanguage.English => "en",
            ClientLanguage.French => "fr",
            ClientLanguage.German => "ge",
            ClientLanguage.Japanese => "ja",
            _ => "en"
        };
        
        loadLocalization();
    }

    private static void loadLocalization() {
        currentLang = new Language("en", "");
        var locDir = Services.PluginInterface.GetPluginLocDirectory();
        if (string.IsNullOrWhiteSpace(locDir)) return;
        
        PluginLog.Information($@"Local Dir: {locDir}");
    }
}