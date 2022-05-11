using Dalamud.Plugin;

namespace DigitalMentor; 

public class DigitalMentor : IDalamudPlugin {
    public string Name => "Digital Mentor";
    public DalamudPluginInterface pluginInterface { get; private set;  }

    public DigitalMentor(DalamudPluginInterface pluginInterface) {
        pluginInterface.Create<Services>();

        this.pluginInterface = pluginInterface;
    }
    
    public void Dispose() {
    }
}