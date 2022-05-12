using Dalamud.Plugin;
using DigitalMentor.Util;

namespace DigitalMentor; 

public class DigitalMentor : IDalamudPlugin {
    public string Name => "Digital Mentor";
    
    public DalamudPluginInterface pluginInterface { get; private set;  }

    public DigitalMentorConfig config { get; private set; }

    public DigitalMentor(DalamudPluginInterface pluginInterface) {
        pluginInterface.Create<Services>();

        this.pluginInterface = pluginInterface;

        this.config = (DigitalMentorConfig) pluginInterface.GetPluginConfig() ?? new DigitalMentorConfig();
        this.config.init(this, pluginInterface);
    }
    
    public void Dispose() {
    }
}