namespace Godot;

using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Hard : Node2D
{
    private BentukDasar bentukDasar;
    private Node2D outlineContainer;
    private Node2D shapesContainer;

    // Outline color
    private Color outlineColor = new Color(0.5f, 0.5f, 0.5f, 0.8f); // Gray

    // Template center position (where the castle outline will be)
    private Vector2 templateCenter = new Vector2(640, 300);

    // Shape spawn positions (on the right side)
    private Vector2 shapeSpawnStart = new Vector2(1050, 150);
    [Signal] public delegate void LevelCompletedEventHandler(string nextLevel);
    // Game state
    private bool isGameCompleted = false;
    private Label completionLabel;
    private Button restartButton;
    private Button backToMenuButton;

    public override void _Ready()
    {
        try
        {
            GD.Print("Hard scene _Ready() started");
            bentukDasar = new BentukDasar();

            // Get containers from scene
            outlineContainer = GetNode<Node2D>("OutlineContainer");
            shapesContainer = GetNode<Node2D>("ShapesContainer");

            GD.Print("Creating outline template...");
            CreateOutlineTemplate();
            
            GD.Print("Creating draggable shapes...");
            CreateDraggableShapes();
            
            GD.Print("Creating completion label...");
            CreateCompletionLabel();
            
            GD.Print("Creating level progression buttons...");
            CreateLevelProgressionButtons();

            QueueRedraw();
            GD.Print("Hard scene _Ready() completed successfully");
        }
        catch (System.Exception e)
        {
            GD.PrintErr("Error in Hard._Ready(): " + e.Message);
            GD.PrintErr("Stack trace: " + e.StackTrace);
        }
    }

    private void CreateOutlineTemplate()
    {
        // Create a castle outline using pattern blocks
        float baseSize = 40f;

        // Castle base (large rectangle)
        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            templateCenter + new Vector2(0, 60), baseSize * 3f, 0);

        // Left tower (rectangle)
        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            templateCenter + new Vector2(-120, 20), baseSize * 1.2f, 0);

        // Right tower (rectangle)
        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            templateCenter + new Vector2(120, 20), baseSize * 1.2f, 0);

        // Left tower roof (triangle)
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            templateCenter + new Vector2(-120, -20), baseSize * 1.2f, 0);

        // Right tower roof (triangle)
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            templateCenter + new Vector2(120, -20), baseSize * 1.2f, 0);

        // Main roof (triangle)
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            templateCenter + new Vector2(0, 0), baseSize * 2.5f, 0);

        // Left window (small square)
        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            templateCenter + new Vector2(-60, 30), baseSize * 0.6f, 0);

        // Right window (small square)
        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            templateCenter + new Vector2(60, 30), baseSize * 0.6f, 0);

        // Door (rectangle)
        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            templateCenter + new Vector2(0, 90), baseSize * 0.8f, 0);

        // Left flag (triangle)
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            templateCenter + new Vector2(-120, -60), baseSize * 0.5f, 0);

        // Right flag (triangle)
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            templateCenter + new Vector2(120, -60), baseSize * 0.5f, 0);
    }

    private void CreateOutlineShape(DraggableShape.ShapeType type, Vector2 position, float size, float rotation)
    {
        OutlineShape outline = new OutlineShape();
        outline.Type = type;
        outline.Position = position;
        outline.ShapeSize = size;
        outline.InitialRotation = rotation;
        outline.OutlineColor = outlineColor;
        outlineContainer.AddChild(outline);
    }

    private void CreateDraggableShapes()
    {
        float baseSize = 40f;
        float spacing = 80f;
        int row = 0;
        int col = 0;

        // Create draggable pieces for castle
        var shapes = new List<(DraggableShape.ShapeType type, Color color, float size)>
        {
            (DraggableShape.ShapeType.Persegi, new Color(0.7f, 0.7f, 0.7f), baseSize * 3f),        // Gray castle base
            (DraggableShape.ShapeType.Persegi, new Color(0.6f, 0.6f, 0.6f), baseSize * 1.2f),      // Dark gray left tower
            (DraggableShape.ShapeType.Persegi, new Color(0.6f, 0.6f, 0.6f), baseSize * 1.2f),      // Dark gray right tower
            (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(0.8f, 0.2f, 0.2f), baseSize * 1.2f), // Red left tower roof
            (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(0.8f, 0.2f, 0.2f), baseSize * 1.2f), // Red right tower roof
            (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(0.8f, 0.2f, 0.2f), baseSize * 2.5f), // Red main roof
            (DraggableShape.ShapeType.Persegi, new Color(0.8f, 0.8f, 1f), baseSize * 0.6f),        // Light blue left window
            (DraggableShape.ShapeType.Persegi, new Color(0.8f, 0.8f, 1f), baseSize * 0.6f),        // Light blue right window
            (DraggableShape.ShapeType.Persegi, new Color(0.4f, 0.2f, 0.1f), baseSize * 0.8f),      // Dark brown door
            (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(1f, 1f, 0f), baseSize * 0.5f),    // Yellow left flag
            (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(1f, 1f, 0f), baseSize * 0.5f),    // Yellow right flag
        };

        foreach (var shapeData in shapes)
        {
            DraggableShape shape = new DraggableShape();
            shape.Type = shapeData.type;
            shape.ShapeColor = shapeData.color;
            shape.ShapeSize = shapeData.size;

            // Position in grid on the right side
            Vector2 spawnPos = shapeSpawnStart + new Vector2(col * spacing, row * spacing);
            shape.Position = spawnPos;

            shapesContainer.AddChild(shape);

            // Update grid position
            col++;
            if (col > 2) // 3 columns
            {
                col = 0;
                row++;
            }
        }
    }
    
    private void CreateCompletionLabel()
    {
        completionLabel = new Label();
        completionLabel.Text = "LEVEL HARD COMPLETED!";
        completionLabel.Position = new Vector2(ScreenUtils.ScreenWidth / 2 - 200, 100);
        completionLabel.AddThemeColorOverride("font_color", Colors.Gold);
        completionLabel.AddThemeFontSizeOverride("font_size", 36);
        completionLabel.Visible = false;
        AddChild(completionLabel);
    }
    
    private void CreateLevelProgressionButtons()
    {
        // Back to Menu Button (since this is the last level)
        backToMenuButton = new Button();
        backToMenuButton.Text = "BACK TO MENU";
        backToMenuButton.Position = new Vector2(ScreenUtils.ScreenWidth / 2 - 150, 150);
        backToMenuButton.Size = new Vector2(300, 50);
        backToMenuButton.AddThemeColorOverride("font_color", Colors.White);
        backToMenuButton.AddThemeFontSizeOverride("font_size", 20);
        backToMenuButton.Visible = false;
        backToMenuButton.Pressed += OnBackToMenuPressed;
        AddChild(backToMenuButton);
        
        // Restart Button
        restartButton = new Button();
        restartButton.Text = "RESTART LEVEL";
        restartButton.Position = new Vector2(ScreenUtils.ScreenWidth / 2 - 150, 220);
        restartButton.Size = new Vector2(300, 50);
        restartButton.AddThemeColorOverride("font_color", Colors.White);
        restartButton.AddThemeFontSizeOverride("font_size", 20);
        restartButton.Visible = false;
        restartButton.Pressed += OnRestartPressed;
        AddChild(restartButton);
    }
    
    public override void _Process(double delta)
    {
        if (!isGameCompleted)
        {
            CheckGameCompletion();
        }
    }
    
    private void CheckGameCompletion()
    {
        var draggableShapes = shapesContainer.GetChildren().OfType<DraggableShape>().ToList();
        bool allPlaced = true;
        
        foreach (var shape in draggableShapes)
        {
            if (!shape.IsCorrectlyPlaced())
            {
                allPlaced = false;
                break;
            }
        }
        
        if (allPlaced && !isGameCompleted)
        {
            isGameCompleted = true;
            completionLabel.Visible = true;
            backToMenuButton.Visible = true;
            restartButton.Visible = true;
            GD.Print("Level Hard Completed! All levels finished!");
        }
    }
    
    private void OnBackToMenuPressed()
    {
        GD.Print("Back to Menu button pressed");
        // Signal to parent to go back to menu
        EmitSignal(SignalName.LevelCompleted, "Hard");
    }
    
    private void OnRestartPressed()
    {
        GD.Print("Restart button pressed - restarting Hard level");
        // Reset game state
        isGameCompleted = false;
        completionLabel.Visible = false;
        backToMenuButton.Visible = false;
        restartButton.Visible = false;
        
        // Reset all shapes
        var draggableShapes = shapesContainer.GetChildren().OfType<DraggableShape>().ToList();
        foreach (var shape in draggableShapes)
        {
            shape.ResetTransformation();
        }
        
        GD.Print("Hard level restarted");
    }

    public override void _Draw()
    {
        // Draw background
        DrawRect(new Rect2(0, 0, ScreenUtils.ScreenWidth, ScreenUtils.ScreenHeight),
                 new Color(0.15f, 0.15f, 0.2f));

        // Draw separator line
        DrawLine(new Vector2(900, 0), new Vector2(900, ScreenUtils.ScreenHeight),
                 new Color(0.3f, 0.3f, 0.3f), 2);

        // Draw title
        DrawString(ThemeDB.FallbackFont, new Vector2(20, 40),
                  "LEVEL HARD - CASTLE", HorizontalAlignment.Left, -1, 32, Colors.White);

        // Draw instructions
        DrawString(ThemeDB.FallbackFont, new Vector2(920, 40),
                  "Drag pieces to outline", HorizontalAlignment.Left, -1, 24, Colors.LightGray);
        DrawString(ThemeDB.FallbackFont, new Vector2(920, 70),
                  "Press 'R' to rotate (45° steps)", HorizontalAlignment.Left, -1, 24, Colors.LightGray);
    }

    public override void _ExitTree()
    {
        bentukDasar?.Dispose();
    }
}
