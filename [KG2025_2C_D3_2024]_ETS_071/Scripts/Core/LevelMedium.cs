namespace Godot;

using System;

public partial class LevelMedium : LevelConfiguration
{
    public override void CreateLevelOutlines(Node2D outlineContainer)
    {
        GD.Print("Menciptakan outline untuk Level MEDIUM.");
        float baseSize = 50f;

        CreateOutline(outlineContainer, DraggableShape.ShapeType.Persegi,
            TemplateCenter + new Vector2(0, 50), baseSize * 2.5f, 0);
        // ... Lanjutkan memindahkan sisa logika outline Medium ke sini ...
    }
}