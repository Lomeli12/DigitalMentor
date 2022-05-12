using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DigitalMentor.Util.Localization; 

public class Language {
    private IDictionary<string, string> entries { get; set; }
    public string lang { get; private set; }

    public Language(string lang, string path) {
        this.lang = lang;
        loadLang(path);
    }

    protected void loadLang(string path) {
        if (!string.IsNullOrWhiteSpace(path) && File.Exists(path)) {
            var text = File.ReadAllText(path);
#pragma warning disable CS8601
            entries = JsonConvert.DeserializeObject<IDictionary<string, string>>(text);
#pragma warning restore CS8601
        }

        // Just don't want to deal with multiple null checks.
        entries ??= new Dictionary<string, string>();
    }
}