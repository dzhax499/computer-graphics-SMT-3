namespace Godot;

using Godot;
using System;

public partial class WinNotification : Control
{
    
    private Label winLabel;
    private Button nextLevelButton;
    private Button restartButton;
    private Button backToMenuButton;
    private VBoxContainer container;
    private ColorRect background;
    

    [Signal] public delegate void NextLevelRequestedEventHandler();
    [Signal] public delegate void RestartRequestedEventHandler();
    [Signal] public delegate void BackToMenuRequestedEventHandler();

    public override void _Ready()
    {
        background = GetNodeOrNull<ColorRect>("Background");
        container = GetNodeOrNull<VBoxContainer>("Container");
        winLabel = GetNodeOrNull<Label>("Container/WinLabel");
        nextLevelButton = GetNodeOrNull<Button>("Container/NextLevelButton");
        restartButton = GetNodeOrNull<Button>("Container/RestartButton");
        backToMenuButton = GetNodeOrNull<Button>("Container/BackToMenuButton");

        // 3. Pindahkan koneksi sinyal ke _Ready()
        if (nextLevelButton != null)
            nextLevelButton.Pressed += OnNextLevelPressed;
        if (restartButton != null)
            restartButton.Pressed += OnRestartPressed;
        if (backToMenuButton != null)
            backToMenuButton.Pressed += OnBackToMenuPressed;
    }

    public void ShowWin(string levelName, bool isLastLevel = false)
    {
        winLabel.Text = $" {levelName.ToUpper()} COMPLETED!";

        // Show/hide appropriate buttons
        nextLevelButton.Visible = !isLastLevel;
        backToMenuButton.Visible = isLastLevel;
        restartButton.Visible = true;

        Visible = true;

        // Add some animation effect
        Modulate = new Color(1, 1, 1, 0);
        var tween = CreateTween();
        tween.TweenProperty(this, "modulate", Colors.White, 0.5f);
    }

    public void HideWin()
    {
        Visible = false;
    }

    private void OnNextLevelPressed()
    {
        EmitSignal(SignalName.NextLevelRequested);
        HideWin();
    }

    private void OnRestartPressed()
    {
        EmitSignal(SignalName.RestartRequested);
        HideWin();
    }

    private void OnBackToMenuPressed()
    {
        EmitSignal(SignalName.BackToMenuRequested);
        HideWin();
    }
}