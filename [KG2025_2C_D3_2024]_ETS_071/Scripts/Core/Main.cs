namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

public partial class PatternBlockGame : Node2D
{
    // Enums
    public enum GameLevel
    {
        Easy,
        Medium,
        Hard
    }

    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        Completed
    }

    // Game State
    private GameState currentState = GameState.Menu;
    private GameLevel currentLevel = GameLevel.Easy;

    // UI Elements
    private Label titleLabel;
    private Button easyButton;
    private Button mediumButton;
    private Button hardButton;
    private Button backButton;

    // Game Scene Reference
    private Node2D gamePlayArea;
    private Control menuContainer;

    public override void _Ready()
    {
        ScreenUtils.Initialize(GetViewport());
        SetupMenu();
    }

    private void SetupMenu()
    {
        // Create Menu Container
        menuContainer = new Control();
        menuContainer.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        AddChild(menuContainer);

        // Title
        titleLabel = new Label();
        titleLabel.Text = "PATTERN BLOCK MOTOR";
        titleLabel.Position = new Vector2(ScreenUtils.ScreenWidth / 2 - 200, 100);
        titleLabel.AddThemeColorOverride("font_color", Colors.White);
        titleLabel.AddThemeFontSizeOverride("font_size", 48);
        menuContainer.AddChild(titleLabel);

        // Easy Button
        easyButton = CreateMenuButton("EASY", new Vector2(ScreenUtils.ScreenWidth / 2 - 100, 250));
        easyButton.Pressed += () => StartLevel(GameLevel.Easy);
        menuContainer.AddChild(easyButton);

        // Medium Button
        mediumButton = CreateMenuButton("MEDIUM", new Vector2(ScreenUtils.ScreenWidth / 2 - 100, 350));
        mediumButton.Pressed += () => StartLevel(GameLevel.Medium);
        menuContainer.AddChild(mediumButton);

        // Hard Button
        hardButton = CreateMenuButton("HARD", new Vector2(ScreenUtils.ScreenWidth / 2 - 100, 450));
        hardButton.Pressed += () => StartLevel(GameLevel.Hard);
        menuContainer.AddChild(hardButton);

        menuContainer.Visible = true;
    }

    private Button CreateMenuButton(string text, Vector2 position)
    {
        Button btn = new Button();
        btn.Text = text;
        btn.Position = position;
        btn.Size = new Vector2(200, 60);
        btn.AddThemeColorOverride("font_color", Colors.White);
        btn.AddThemeFontSizeOverride("font_size", 32);
        return btn;
    }

    private void StartLevel(GameLevel level)
    {
        currentLevel = level;
        currentState = GameState.Playing;
        menuContainer.Visible = false;

        // Clear existing game area
        if (gamePlayArea != null)
        {
            gamePlayArea.QueueFree();
        }

        // Create game area based on level
        switch (level)
        {
            case GameLevel.Easy:
                gamePlayArea = new Easy();
                break;
            case GameLevel.Medium:
                GD.Print("Medium level - Coming soon");
                BackToMenu();
                return;
            case GameLevel.Hard:
                GD.Print("Hard level - Coming soon");
                BackToMenu();
                return;
        }

        AddChild(gamePlayArea);

        // Create Back Button
        backButton = CreateMenuButton("BACK", new Vector2(20, 20));
        backButton.Size = new Vector2(120, 40);
        backButton.AddThemeFontSizeOverride("font_size", 20);
        backButton.Pressed += BackToMenu;
        AddChild(backButton);
    }

    private void BackToMenu()
    {
        currentState = GameState.Menu;

        if (gamePlayArea != null)
        {
            gamePlayArea.QueueFree();
            gamePlayArea = null;
        }

        if (backButton != null)
        {
            backButton.QueueFree();
            backButton = null;
        }

        menuContainer.Visible = true;
    }

    public override void _Draw()
    {
        // Background
        DrawRect(new Rect2(0, 0, ScreenUtils.ScreenWidth, ScreenUtils.ScreenHeight),
                 new Color(0.1f, 0.1f, 0.15f));
    }
}