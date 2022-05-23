using Dalamud.Game.Command;
using Dalamud.Plugin;
using DigitalMentor.Util;
using DigitalMentor.Util.Localization;

namespace DigitalMentor; 

public class DigitalMentor : IDalamudPlugin {
    public string Name => Constants.pluginName;
    
    public DalamudPluginInterface pluginInterface { get; private set;  }

    public DigitalMentorConfig config { get; private set; }

    public DigitalMentor(DalamudPluginInterface pluginInterface) {
        pluginInterface.Create<Services>();

        this.pluginInterface = pluginInterface;

        config = (DigitalMentorConfig) pluginInterface.GetPluginConfig() ?? new DigitalMentorConfig();
        config.init(this, pluginInterface);
        
        I18n.setupLocalization(this);

        Services.Commands.AddHandler(Constants.commandName, new CommandInfo(handleDMCommand) {
            HelpMessage = $@"{Constants.commandName} help - Display help options"
        });
    }

    //TODO: Localize arguments?
    private void handleDMCommand(string command, string args) {
        var arglist = args.Split(' ');
        if (arglist.Length == 0) {
            //TODO: Toggle tips on/off
        } else {
            switch (arglist[0]) {
                case "enable":
                case "on":
                case "show":
                    //TODO: Toggle tips on
                    break;
                case "disable":
                case "off":
                case "hide":
                    //TODO: Toggle tips off
                    break;
                case "help":
                case "?":
                    //TODO: Display help information
                    break;
                case "options":
                case "settings":
                    Services.Chat.Print("Opening options window.");
                    break;
            }
        }
        Services.Chat.UpdateQueue();
    }
    
    public void Dispose() {
        Services.Commands.RemoveHandler(Constants.commandName);
    }
}