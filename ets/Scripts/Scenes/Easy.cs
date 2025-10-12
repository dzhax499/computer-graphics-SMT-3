namespace Godot;

using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Easy : Node2D
{
    private BentukDasar bentukDasar;
    private Node2D outlineContainer;
    private Node2D shapesContainer;

    // Outline color
    private Color outlineColor = new Color(0.5f, 0.5f, 0.5f, 0.8f); // Gray

    // Template center position (where the motor outline will be)
    private Vector2 templateCenter = new Vector2(640, 300);

    // Shape spawn positions (on the right side)
    private Vector2 shapeSpawnStart = new Vector2(1050, 150);
    
    // Game state
    private bool isGameCompleted = false;
    private Label completionLabel;
    private Button nextLevelButton;
    private Button restartButton;
    

    public override void _Ready()
    {
        try
        {
            GD.Print("Easy scene _Ready() started");
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
            GD.Print("Easy scene _Ready() completed successfully");
        }
        catch (System.Exception e)
        {
            GD.PrintErr("Error in Easy._Ready(): " + e.Message);
            GD.PrintErr("Stack trace: " + e.StackTrace);
        }
    }

    private void CreateOutlineTemplate()
    {
        // Based on the tangram animal image, create outline shapes
        // I'll position them relative to templateCenter to form an animal silhouette

        float baseSize = 50f;

        // 1. Orange Small Triangle (tail) - kiri atas
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            templateCenter + new Vector2(-120, -40), baseSize * 0.8f, 0);

        // 2. Green Square (front leg, rotated 45°) - kiri bawah
        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            templateCenter + new Vector2(-80, 20), baseSize * 0.7f, 45);

        // 3. Red Large Triangle (upper body) - tengah atas
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            templateCenter + new Vector2(-20, -20), baseSize * 1.2f, 0);

        // 4. Purple Large Triangle (head) - kanan atas
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            templateCenter + new Vector2(60, -60), baseSize * 1.0f, 0);

        // 5. Blue Parallelogram (lower body) - tengah
        CreateOutlineShape(DraggableShape.ShapeType.JajarGenjang,
            templateCenter + new Vector2(20, 40), baseSize * 1.0f, 0);

        // 6. Blue Small Triangle (hind leg) - kanan bawah
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            templateCenter + new Vector2(100, 60), baseSize * 0.6f, 0);

        // 7. Yellow Small Triangle (hind leg) - kanan bawah
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            templateCenter + new Vector2(80, 80), baseSize * 0.6f, 0);
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
        float baseSize = 50f;
        float spacing = 100f;
        int row = 0;
        int col = 0;

        // Create draggable pieces with colors matching the tangram animal image
        var shapes = new List<(DraggableShape.ShapeType type, Color color, float size)>
        {
            (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(1f, 0.5f, 0f), baseSize * 0.8f),    // Orange Small Triangle (tail)
            (DraggableShape.ShapeType.Persegi, new Color(0f, 0.8f, 0f), baseSize * 0.7f),             // Green Square (front leg)
            (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(1f, 0f, 0f), baseSize * 1.2f),      // Red Large Triangle (upper body)
            (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(0.6f, 0f, 1f), baseSize * 1.0f),    // Purple Large Triangle (head)
            (DraggableShape.ShapeType.JajarGenjang, new Color(0f, 0f, 1f), baseSize * 1.0f),          // Blue Parallelogram (lower body)
            (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(0f, 0.5f, 1f), baseSize * 0.6f),    // Blue Small Triangle (hind leg)
            (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(1f, 1f, 0f), baseSize * 0.6f),      // Yellow Small Triangle (hind leg)
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
            if (col > 2) // 3 columns for better layout
            {
                col = 0;
                row++;
            }
        }
    }
    
    private void CreateCompletionLabel()
    {
        completionLabel = new Label();
        completionLabel.Text = "LEVEL EASY COMPLETED!";
        completionLabel.Position = new Vector2(ScreenUtils.ScreenWidth / 2 - 200, 100);
        completionLabel.AddThemeColorOverride("font_color", Colors.Green);
        completionLabel.AddThemeFontSizeOverride("font_size", 36);
        completionLabel.Visible = false;
        AddChild(completionLabel);
    }
    
    private void CreateLevelProgressionButtons()
    {
        // Next Level Button
        nextLevelButton = new Button();
        nextLevelButton.Text = "NEXT LEVEL (MEDIUM)";
        nextLevelButton.Position = new Vector2(ScreenUtils.ScreenWidth / 2 - 150, 150);
        nextLevelButton.Size = new Vector2(300, 50);
        nextLevelButton.AddThemeColorOverride("font_color", Colors.White);
        nextLevelButton.AddThemeFontSizeOverride("font_size", 20);
        nextLevelButton.Visible = false;
        nextLevelButton.Pressed += OnNextLevelPressed;
        AddChild(nextLevelButton);
        
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
            nextLevelButton.Visible = true;
            restartButton.Visible = true;
            GD.Print("Level Easy Completed!");
        }
    }
    
    private void OnNextLevelPressed()
    {
        GD.Print("Next Level button pressed - going to Medium");
        // Signal to parent to change level
        EmitSignal("level_completed", "Medium");
    }
    
    private void OnRestartPressed()
    {
        GD.Print("Restart button pressed - restarting Easy level");
        // Reset game state
        isGameCompleted = false;
        completionLabel.Visible = false;
        nextLevelButton.Visible = false;
        restartButton.Visible = false;
        
        // Reset all shapes
        var draggableShapes = shapesContainer.GetChildren().OfType<DraggableShape>().ToList();
        foreach (var shape in draggableShapes)
        {
            shape.ResetTransformation();
            // Reset position to original spawn position
            // This would need to be implemented based on your spawn logic
        }
        
        GD.Print("Easy level restarted");
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
                  "LEVEL EASY - TANGRAM ANIMAL", HorizontalAlignment.Left, -1, 32, Colors.White);

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

// Outline Shape class for displaying gray template
public partial class OutlineShape : Node2D
{
    public DraggableShape.ShapeType Type { get; set; }
    public float ShapeSize { get; set; } = 50f;
    public float InitialRotation { get; set; } = 0f;
    public Color OutlineColor { get; set; }

    private List<Vector2> shapePoints;
    private BentukDasar bentukDasar;
    private Transformasi transformasi;

    public override void _Ready()
    {
        bentukDasar = new BentukDasar();
        transformasi = new Transformasi();

        GenerateShape();

        if (InitialRotation != 0)
        {
            ApplyRotation(InitialRotation);
        }

        QueueRedraw();
    }

    private void GenerateShape()
    {
        shapePoints = new List<Vector2>();

        switch (Type)
        {
            case DraggableShape.ShapeType.Persegi:
                shapePoints = bentukDasar.Persegi(-ShapeSize / 2, -ShapeSize / 2, ShapeSize);
                break;

            case DraggableShape.ShapeType.TrapesiumSiku:
                shapePoints = bentukDasar.TrapesiumSiku(
                    new Vector2(-ShapeSize / 2, -ShapeSize / 3),
                    (int)(ShapeSize * 0.6f),
                    (int)ShapeSize,
                    (int)(ShapeSize * 0.6f)
                );
                break;

            case DraggableShape.ShapeType.SegitigaSamaKaki:
                shapePoints = bentukDasar.SegitigaSamaKaki(
                    new Vector2(-ShapeSize / 2, -ShapeSize / 3),
                    (int)ShapeSize,
                    (int)(ShapeSize * 0.8f)
                );
                break;

            case DraggableShape.ShapeType.Hexagon:
                shapePoints = bentukDasar.Hexagon(Vector2.Zero, ShapeSize / 2, 0);
                break;
        }
    }

    private void ApplyRotation(float degrees)
    {
        var matrix = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrix);
        transformasi.RotationClockwise(matrix, degrees, Vector2.Zero);
        shapePoints = transformasi.GetTransformPoint(matrix, shapePoints);
    }

    public override void _Draw()
    {
        if (shapePoints == null || shapePoints.Count == 0) return;

        // Draw outline only (dotted gray)
        GraphicsUtils.PutPixelAll(this, shapePoints,
            GraphicsUtils.DrawStyle.DotDot, OutlineColor);
    }

    public override void _ExitTree()
    {
        bentukDasar?.Dispose();
    }
}