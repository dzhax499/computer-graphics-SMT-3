namespace Godot;

/// <summary>
/// Hard Challenge Level - 11 pieces Castle
/// </summary>
public partial class HardLevel : BaseChallengeLevel
{
    protected override void CreateLevelOutlines()
    {
        float baseSize = 40f;

        // Castle (11 pieces) - positioned on board center
        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            boardCenter + new Vector2(0, 60), baseSize * 3f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            boardCenter + new Vector2(-120, 20), baseSize * 1.2f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            boardCenter + new Vector2(120, 20), baseSize * 1.2f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(-120, -20), baseSize * 1.2f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(120, -20), baseSize * 1.2f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(0, 0), baseSize * 2.5f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            boardCenter + new Vector2(-60, 30), baseSize * 0.6f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            boardCenter + new Vector2(60, 30), baseSize * 0.6f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            boardCenter + new Vector2(0, 90), baseSize * 0.8f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(-120, -60), baseSize * 0.5f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(120, -60), baseSize * 0.5f, 0);

        GD.Print("✅ Hard Level: 11 outlines created");
    }

    protected override string GetLevelTitle()
    {
        return "LEVEL HARD";
    }

    protected override string GetNextLevelPath()
    {
        return ""; // No next level
    }

    protected override bool IsLastLevel()
    {
        return true;
    }
}