namespace Godot;

/// <summary>
/// Medium Challenge Level - 8 pieces House
/// UPDATED: Custom palette dengan lebih banyak pilihan shape
/// </summary>
public partial class MediumLevel : BaseChallengeLevel
{
    /// <summary>
    /// Define palette shapes untuk level Medium
    /// Berbeda dari Easy - lebih banyak variasi!
    /// </summary>
    protected override void DefinePaletteShapes()
    {
        // Opsi 1: Gunakan auto size (ukuran sama dengan outline)
        // AddPaletteShapeAuto(DraggableShape.ShapeType.Persegi, Colors.Red, 5);
        // AddPaletteShapeAuto(DraggableShape.ShapeType.SegitigaSamaKaki, Colors.Orange);
        // AddPaletteShapeAuto(DraggableShape.ShapeType.Hexagon, Colors.Green, 2);

        // Opsi 2: Custom size - lebih challenging!
        AddPaletteShape(DraggableShape.ShapeType.Persegi, Colors.Red, 125f, 1);        // Body besar
        AddPaletteShape(DraggableShape.ShapeType.Persegi, Colors.OrangeRed, 40f, 2);   // Windows
        AddPaletteShape(DraggableShape.ShapeType.Persegi, Colors.DarkRed, 30f, 2);     // Door + chimney

        AddPaletteShape(DraggableShape.ShapeType.SegitigaSamaKaki, Colors.Orange, 125f, 1); // Roof

        AddPaletteShape(DraggableShape.ShapeType.Hexagon, Colors.Green, 40f, 2);       // Trees

        // Extra shapes untuk variasi
        AddPaletteShape(DraggableShape.ShapeType.Lingkaran, Colors.Yellow, 30f, 1);    // Sun
        AddPaletteShape(DraggableShape.ShapeType.SegitigaSiku, Colors.Brown, 40f, 2);  // Extra

        GD.Print("✅ Medium Level: Custom palette defined (12 shapes)");
    }

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