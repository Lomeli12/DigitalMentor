using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DigitalMentor.Util.Localization;

public class Language {
    private IDictionary<string, string> entries { get; set; }
    public string lang { get; private set; }

    public Language(string lang) {
        this.lang = lang;
        entries = new Dictionary<string, string>();
    }

    protected internal void loadLang(string path) {
        var localPath = Path.Combine(path, $"{lang}.json");
        if (!File.Exists(localPath)) return;

        var text = File.ReadAllText(localPath);
        entries = JsonConvert.DeserializeObject<IDictionary<string, string>>(text)!;
    }

    protected internal string localize(string key, params object[] args) {
        var output = entries.ContainsKey(key) ? entries[key] : key;
        if (args.Length > 0)
            output = string.Format(output, args);
        return output;
    }
}