namespace Godot;

using System;

public partial class LevelHard : LevelConfiguration
{
    public override void CreateLevelOutlines(Node2D outlineContainer)
    {
        GD.Print("Menciptakan outline untuk Level HARD.");
        float baseSize = 40f;

        CreateOutline(outlineContainer, DraggableShape.ShapeType.Persegi,
            TemplateCenter + new Vector2(0, 60), baseSize * 3f, 0);
        // ... Lanjutkan memindahkan sisa logika outline Hard ke sini ...
    }
}