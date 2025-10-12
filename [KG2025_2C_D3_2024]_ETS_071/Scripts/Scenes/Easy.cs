namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

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

    public override void _Ready()
    {
        bentukDasar = new BentukDasar();

        // Create containers
        outlineContainer = new Node2D();
        outlineContainer.Name = "OutlineContainer";
        AddChild(outlineContainer);

        shapesContainer = new Node2D();
        shapesContainer.Name = "ShapesContainer";
        AddChild(shapesContainer);

        CreateOutlineTemplate();
        CreateDraggableShapes();

        QueueRedraw();
    }

    private void CreateOutlineTemplate()
    {
        // Based on the motor civic image, create outline shapes
        // I'll position them relative to templateCenter

        float baseSize = 60f;

        // 1. Orange Trapesium Siku (front/hood) - kiri depan
        CreateOutlineShape(DraggableShape.ShapeType.TrapesiumSiku,
            templateCenter + new Vector2(-180, 20), baseSize * 0.8f, 0);

        // 2. Small Green Persegi (front bottom)
        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            templateCenter + new Vector2(-180, 70), baseSize * 0.5f, 0);

        // 3. Large Red Trapesium (main body)
        CreateOutlineShape(DraggableShape.ShapeType.TrapesiumSiku,
            templateCenter + new Vector2(-50, 20), baseSize * 1.8f, 0);

        // 4. Large Purple Triangle (back top)
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            templateCenter + new Vector2(100, -30), baseSize * 1.2f, 180);

        // 5. Small Blue Triangle (middle)
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            templateCenter + new Vector2(50, 40), baseSize * 0.7f, 180);

        // 6. Small Yellow Trapesium (back bottom)
        CreateOutlineShape(DraggableShape.ShapeType.TrapesiumSiku,
            templateCenter + new Vector2(120, 70), baseSize * 0.6f, 0);

        // 7. Front wheel (Hexagon/Circle)
        CreateOutlineShape(DraggableShape.ShapeType.Hexagon,
            templateCenter + new Vector2(-150, 100), baseSize * 0.7f, 0);

        // 8. Back wheel (Hexagon/Circle)
        CreateOutlineShape(DraggableShape.ShapeType.Hexagon,
            templateCenter + new Vector2(80, 100), baseSize * 0.7f, 0);
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
        float baseSize = 60f;
        float spacing = 100f;
        int row = 0;
        int col = 0;

        // Create draggable pieces with colors matching the image
        var shapes = new List<(DraggableShape.ShapeType type, Color color, float size)>
        {
            (DraggableShape.ShapeType.TrapesiumSiku, new Color(1f, 0.5f, 0f), baseSize * 0.8f),      // Orange
            (DraggableShape.ShapeType.Persegi, new Color(0f, 1f, 0f), baseSize * 0.5f),              // Green
            (DraggableShape.ShapeType.TrapesiumSiku, new Color(1f, 0f, 0f), baseSize * 1.8f),        // Red
            (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(0.6f, 0f, 1f), baseSize * 1.2f),   // Purple
            (DraggableShape.ShapeType.SegitigaSamaKaki, new Color(0f, 0f, 1f), baseSize * 0.7f),     // Blue
            (DraggableShape.ShapeType.TrapesiumSiku, new Color(1f, 1f, 0f), baseSize * 0.6f),        // Yellow
            (DraggableShape.ShapeType.Hexagon, new Color(0.3f, 0.3f, 0.3f), baseSize * 0.7f),        // Dark Gray wheel
            (DraggableShape.ShapeType.Hexagon, new Color(0.3f, 0.3f, 0.3f), baseSize * 0.7f),        // Dark Gray wheel
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
            if (col > 1) // 2 columns
            {
                col = 0;
                row++;
            }
        }
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
                  "LEVEL EASY - MOTOR CIVIC", 32, Colors.White);

        // Draw instructions
        DrawString(ThemeDB.FallbackFont, new Vector2(920, 40),
                  "Drag pieces to outline", 24, Colors.LightGray);
        DrawString(ThemeDB.FallbackFont, new Vector2(920, 70),
                  "Press 'R' to rotate", 24, Colors.LightGray);
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
    private TransformasiFast transformasi;

    public override void _Ready()
    {
        bentukDasar = new BentukDasar();
        transformasi = new TransformasiFast();

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
        var matrix = TransformasiFast.Identity();
        float radians = Mathf.DegToRad(degrees);
        transformasi.RotationClockwise(ref matrix, radians, Vector2.Zero);
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