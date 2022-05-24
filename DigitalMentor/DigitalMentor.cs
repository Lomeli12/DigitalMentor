using Dalamud.Game.Command;
using Dalamud.Plugin;
using DigitalMentor.Util;
using DigitalMentor.Util.Localization;

namespace DigitalMentor; 

public class DigitalMentor : IDalamudPlugin {
    public string Name => Constants.pluginName;
    
    public DalamudPluginInterface pluginInterface { get; private set;  }

    public DigitalMentorConfig config { get; private set; }
    
    public OptionsUI options { get; private set; }

    public DigitalMentor(DalamudPluginInterface pluginInterface) {
        pluginInterface.Create<Services>();

        this.pluginInterface = pluginInterface;

        config = (DigitalMentorConfig) pluginInterface.GetPluginConfig() ?? new DigitalMentorConfig();
        options = new OptionsUI(this);
        
        config.init(this, pluginInterface);
        
        I18n.setupLocalization(this);

        Services.Commands.AddHandler(Constants.commandName, new CommandInfo(handleDMCommand) {
            HelpMessage = $"{I18n.localize("plugin.help.none")}\n" +
                          $"   help, ?  -  {I18n.localize("plugin.help.help")}\n" +
                          $"   enable, on, show  -  {I18n.localize("plugin.help.enable")}\n" +
                          $"   disable, off, hide  -  {I18n.localize("plugin.help.disable")}\n" +
                          $"   options, settings  -  {I18n.localize("plugin.help.options")}"
        });
    }

    //TODO: Localize arguments?
    private void handleDMCommand(string command, string args) {
        var arglist = args.Split(' ');
        if (arglist.Length == 0) {
            //TODO: Toggle tips on/off
            config.enableMentor = !config.enableMentor;
            config.save();
            Chat.queueChat(true, config.enableMentor ? "config.mentor.enable" : "config.mentor.disable");
        } else {
            switch (arglist[0]) {
                case "enable":
                case "on":
                case "show":
                    config.enableMentor = true;
                    config.save();
                    Chat.queueChat(true, "config.mentor.enable");
                    break;
                case "disable":
                case "off":
                case "hide":
                    config.enableMentor = false;
                    config.save();
                    Chat.queueChat(true,"config.mentor.disable");
                    break;
                case "help":
                case "?":
                    //TODO: Display help information
                    break;
                case "options":
                case "settings":
                    options.settingsVisible = !options.settingsVisible;
                    Chat.queueChat(true, options.settingsVisible ? "config.window.open_settings" : "config.window.close_settings");
                    break;
            }
        }
        Chat.popChat();
    }
    
    public void Dispose() {
        Services.Commands.RemoveHandler(Constants.commandName);
        options.Dispose();
    }
}