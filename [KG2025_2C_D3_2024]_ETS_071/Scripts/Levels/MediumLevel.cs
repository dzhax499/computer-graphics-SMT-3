namespace Godot;

/// <summary>
/// Medium Challenge Level - 8 pieces House
/// </summary>
public partial class MediumLevel : BaseChallengeLevel
{
    protected override void CreateLevelOutlines()
    {
        float baseSize = 50f;

        // House (8 pieces) - positioned on board center
        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            boardCenter + new Vector2(0, 50), baseSize * 2.5f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(0, -30), baseSize * 2.5f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            boardCenter + new Vector2(-60, 20), baseSize * 0.8f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            boardCenter + new Vector2(60, 20), baseSize * 0.8f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            boardCenter + new Vector2(0, 80), baseSize * 0.6f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            boardCenter + new Vector2(80, -60), baseSize * 0.5f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.Hexagon,
            boardCenter + new Vector2(-150, 50), baseSize * 0.8f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.Hexagon,
            boardCenter + new Vector2(150, 50), baseSize * 0.8f, 0);

        GD.Print("✅ Medium Level: 8 outlines created");
    }

    protected override string GetLevelTitle()
    {
        return "LEVEL MEDIUM";
    }

    protected override string GetNextLevelPath()
    {
        return "res://Scenes/HardLevel.tscn";
    }

    protected override bool IsLastLevel()
    {
        return false;
    }
}