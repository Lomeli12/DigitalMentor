using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Dalamud.Logging;
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
        var localPath = path + $"{lang}.json";
        PluginLog.Debug($"Localization Path: {localPath}");

        var text = readEmbeddedStream(localPath);
        var readEntries = JsonConvert.DeserializeObject<IDictionary<string, string>>(text);
        if (readEntries is not {Count: > 0}) return;
        entries.Clear();
        foreach (var entry in readEntries)
            entries.Add(entry.Key, entry.Value);
    }

    private static string readEmbeddedStream(string path) {
        var resourceStream = Assembly.GetCallingAssembly().GetManifestResourceStream(path);
        if (resourceStream == null) return "";
        using var reader = new StreamReader(resourceStream);
        return reader.ReadToEnd();
    }

    protected internal string localize(string key, params object[] args) {
        var output = entries.ContainsKey(key) ? entries[key] : key;
        if (args.Length > 0)
            output = string.Format(output, args);
        return output;
    }
}