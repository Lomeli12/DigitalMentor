using System;
using System.Numerics;
using DigitalMentor.Util.Localization;
using ImGuiNET;

namespace DigitalMentor; 

public class OptionsUI : IDisposable {
    public bool settingsVisible;

    private DigitalMentor instance;

    private bool enableMentor;

    public OptionsUI(DigitalMentor instance) {
        this.instance = instance;

        enableMentor = instance.config.enableMentor;

        instance.pluginInterface.UiBuilder.Draw += draw;
        instance.pluginInterface.UiBuilder.OpenConfigUi += showSettings;
    }

    // I'm honestly not sure what I'm doing here, just following other plugins on GitHub
    private void drawSettings() {
        ImGui.SetNextWindowSize(new Vector2(200, 200), ImGuiCond.FirstUseEver);
        ImGui.Begin(I18n.localize("config.window.title"), ref settingsVisible);

        if (ImGui.Checkbox(I18n.localize("config.window.enable_mentor"), ref enableMentor)) {
            instance.config.enableMentor = enableMentor;
            instance.config.save();
        }
        
        ImGui.End();
    }

    private void draw() {
        if (settingsVisible) drawSettings();
    }

    private void showSettings() {
        settingsVisible = true;
    }
    
    public void Dispose() {
        instance.pluginInterface.UiBuilder.Draw -= draw;
        instance.pluginInterface.UiBuilder.OpenConfigUi -= showSettings;
    }
}