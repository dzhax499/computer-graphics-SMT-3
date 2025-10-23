namespace Godot;

using System;

public partial class PauseMenu : CanvasLayer
{
    // UI References
    private Panel panel;
    private Button resumeButton;
    private Button restartButton;
    private Button menuButton;

    // Pause state
    private bool isPaused = false;

    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.WhenPaused;

        panel = GetNode<Panel>("Panel");
        resumeButton = GetNode<Button>("Panel/Main-Vbox/ResumeButton");
        restartButton = GetNode<Button>("Panel/Main-Vbox/RestartButton");
        menuButton = GetNode<Button>("Panel/Main-Vbox/MenuButton");

        Hide();

        GD.Print("✅ PauseMenu Ready");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
        {
            if (keyEvent.Keycode == Key.Escape)
            {
                TogglePause();
                GetViewport().SetInputAsHandled();
            }
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    /// <summary>
    /// Pause game
    /// </summary>
    public void Pause()
    {
        isPaused = true;
        GetTree().Paused = true;  // Pause semua node di scene tree
        Show();
        GD.Print("⏸️ Game Paused");
    }

    /// <summary>
    /// Resume game
    /// </summary>
    public void Resume()
    {
        isPaused = false;
        GetTree().Paused = false;  // Resume game
        Hide();
        GD.Print("▶️ Game Resumed");
    }

    private void OnResumePressed()
    {
        Resume();
    }

    private void OnRestartPressed()
    {
        // Resume first before restart
        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
        GD.Print("🔄 Game Restarted");
    }

    private void OnMenuPressed()
    {
        // Resume first before changing scene
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
        GD.Print("🏠 Back to Main Menu");
    }
}