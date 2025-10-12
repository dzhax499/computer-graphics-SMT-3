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
        GD.Print("Main._Ready() started");
        ScreenUtils.Initialize(GetViewport());
        GD.Print("ScreenUtils initialized");
        SetupMenu();
        GD.Print("Main._Ready() completed");
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
                var easyScene = GD.Load<PackedScene>("res://Scenes/ChallengeEasy.tscn");
                if (easyScene != null)
                {
                    gamePlayArea = easyScene.Instantiate() as Node2D;
                    // Connect level completion signal
                    if (gamePlayArea.HasSignal("level_completed"))
                    {
                        gamePlayArea.Connect("level_completed", new Callable(this, nameof(OnLevelCompleted)));
                    }
                    GD.Print("Easy scene created successfully");
                }
                else
                {
                    GD.PrintErr("Failed to load ChallengeEasy.tscn");
                    BackToMenu();
                    return;
                }
                break;
            case GameLevel.Medium:
                var mediumScene = GD.Load<PackedScene>("res://Scenes/ChallengeMedium.tscn");
                if (mediumScene != null)
                {
                    gamePlayArea = mediumScene.Instantiate() as Node2D;
                    // Connect level completion signal
                    if (gamePlayArea.HasSignal("level_completed"))
                    {
                        gamePlayArea.Connect("level_completed", new Callable(this, nameof(OnLevelCompleted)));
                    }
                    GD.Print("Medium scene created successfully");
                }
                else
                {
                    GD.PrintErr("Failed to load ChallengeMedium.tscn");
                    BackToMenu();
                    return;
                }
                break;
            case GameLevel.Hard:
                var hardScene = GD.Load<PackedScene>("res://Scenes/ChallengeHard.tscn");
                if (hardScene != null)
                {
                    gamePlayArea = hardScene.Instantiate() as Node2D;
                    // Connect level completion signal
                    if (gamePlayArea.HasSignal("level_completed"))
                    {
                        gamePlayArea.Connect("level_completed", new Callable(this, nameof(OnLevelCompleted)));
                    }
                    GD.Print("Hard scene created successfully");
                }
                else
                {
                    GD.PrintErr("Failed to load ChallengeHard.tscn");
                    BackToMenu();
                    return;
                }
                break;
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
    
    private void OnLevelCompleted(string nextLevel)
    {
        GD.Print($"Level completed, next level: {nextLevel}");
        
        if (nextLevel == "Medium")
        {
            StartLevel(GameLevel.Medium);
        }
        else if (nextLevel == "Hard")
        {
            StartLevel(GameLevel.Hard);
        }
        else if (nextLevel == "Menu")
        {
            BackToMenu();
        }
    }

    public override void _Draw()
    {
        // Background
        DrawRect(new Rect2(0, 0, ScreenUtils.ScreenWidth, ScreenUtils.ScreenHeight),
                 new Color(0.1f, 0.1f, 0.15f));
        
        // Debug: Draw a simple test rectangle to verify _Draw is working
        DrawRect(new Rect2(10, 10, 100, 50), Colors.Red);
    }
}