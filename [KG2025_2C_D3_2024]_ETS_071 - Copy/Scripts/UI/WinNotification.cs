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
        CreateUI();
        Visible = false;
    }

    private void CreateUI()
    {
        // Create semi-transparent background
        background = new ColorRect();
        background.Color = new Color(0, 0, 0, 0.7f);
        background.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
        AddChild(background);

        // Create main container
        container = new VBoxContainer();
        container.AddThemeConstantOverride("separation", 20);
        container.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.Center);
        container.Size = new Vector2(400, 300);
        AddChild(container);

        // Create win label
        winLabel = new Label();
        winLabel.Text = "🎉 LEVEL COMPLETED! 🎉";
        winLabel.HorizontalAlignment = HorizontalAlignment.Center;
        winLabel.AddThemeColorOverride("font_color", Colors.Gold);
        winLabel.AddThemeFontSizeOverride("font_size", 36);
        container.AddChild(winLabel);

        // Create next level button
        nextLevelButton = new Button();
        nextLevelButton.Text = "NEXT LEVEL";
        nextLevelButton.Size = new Vector2(300, 50);
        nextLevelButton.AddThemeColorOverride("font_color", Colors.White);
        nextLevelButton.AddThemeFontSizeOverride("font_size", 20);
        nextLevelButton.Pressed += OnNextLevelPressed;
        container.AddChild(nextLevelButton);

        // Create restart button
        restartButton = new Button();
        restartButton.Text = "RESTART LEVEL";
        restartButton.Size = new Vector2(300, 50);
        restartButton.AddThemeColorOverride("font_color", Colors.White);
        restartButton.AddThemeFontSizeOverride("font_size", 20);
        restartButton.Pressed += OnRestartPressed;
        container.AddChild(restartButton);

        // Create back to menu button
        backToMenuButton = new Button();
        backToMenuButton.Text = "BACK TO MENU";
        backToMenuButton.Size = new Vector2(300, 50);
        backToMenuButton.AddThemeColorOverride("font_color", Colors.White);
        backToMenuButton.AddThemeFontSizeOverride("font_size", 20);
        backToMenuButton.Pressed += OnBackToMenuPressed;
        container.AddChild(backToMenuButton);
    }

    public void ShowWin(string levelName, bool isLastLevel = false)
    {
        winLabel.Text = $"🎉 {levelName.ToUpper()} COMPLETED! 🎉";

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
