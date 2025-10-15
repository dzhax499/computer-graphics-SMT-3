namespace Godot;

using System;

public partial class LevelEasy : LevelConfiguration
{
    public override void CreateLevelOutlines(Node2D outlineContainer)
    {
        GD.Print("Menciptakan outline untuk Level EASY.");
        float baseSize = 50f;

        // Logika pembuatan outline yang tadinya di ChallengeLevel.cs
        CreateOutline(outlineContainer, DraggableShape.ShapeType.SegitigaSamaKaki,
            TemplateCenter + new Vector2(-120, -40), baseSize * 0.8f, 0);
        CreateOutline(outlineContainer, DraggableShape.ShapeType.Persegi,
            TemplateCenter + new Vector2(-80, 20), baseSize * 0.7f, 45);
        CreateOutline(outlineContainer, DraggableShape.ShapeType.SegitigaSamaKaki,
            TemplateCenter + new Vector2(-20, -20), baseSize * 1.2f, 0);
        CreateOutline(outlineContainer, DraggableShape.ShapeType.SegitigaSamaKaki,
            TemplateCenter + new Vector2(60, -60), baseSize * 1.0f, 0);
        CreateOutline(outlineContainer, DraggableShape.ShapeType.JajarGenjang,
            TemplateCenter + new Vector2(20, 40), baseSize * 1.0f, 0);
        CreateOutline(outlineContainer, DraggableShape.ShapeType.SegitigaSamaKaki,
            TemplateCenter + new Vector2(100, 60), baseSize * 0.6f, 0);
        CreateOutline(outlineContainer, DraggableShape.ShapeType.SegitigaSamaKaki,
            TemplateCenter + new Vector2(80, 80), baseSize * 0.6f, 0);
    }
}