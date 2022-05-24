using DigitalMentor.Util.Localization;

namespace DigitalMentor.Util; 

public class Chat {
    public static void queueChat(bool displayTag, string msg, params object[] args) {
        var output = I18n.localize(msg, args);
        if (displayTag) output = $"[{I18n.localize("plugin.name")}]: {output}";
        Services.Chat.Print(output);
    }

    public static void queueChat(string msg, params object[] args) =>
        queueChat(false, msg, args);

    public static void popChat() {
        Services.Chat.UpdateQueue();
    }
}