using System;
using Dalamud.Configuration;
using Dalamud.Plugin;

namespace DigitalMentor.Util;

[Serializable]
public class DigitalMentorConfig : IPluginConfiguration {

    [NonSerialized] private DalamudPluginInterface pluginInterface;

    [NonSerialized] private DigitalMentor digitalMentor;
    
    public int Version { get; set; } = 3;
    public string language;
    public bool enableMentor = true;

    public void init(DigitalMentor digitalMentor, DalamudPluginInterface pluginInterface) {
        this.digitalMentor = digitalMentor;
        this.pluginInterface = pluginInterface;
        
    }

    public void save() {
        pluginInterface.SavePluginConfig(this);
    }
}