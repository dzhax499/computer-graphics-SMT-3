namespace Godot;

/// <summary>
/// Hard Challenge Level - 11 pieces Castle
/// UPDATED: Custom palette dengan banyak variasi + extra shapes
/// </summary>
public partial class HardLevel : BaseChallengeLevel
{
    /// <summary>
    /// Define palette shapes untuk level Hard
    /// PALING CHALLENGING - banyak bentuk berbeda!
    /// </summary>
    protected override void DefinePaletteShapes()
    {
        // Castle membutuhkan banyak persegi dan segitiga
        // Beri ukuran yang BERBEDA dari outline agar lebih challenging!

        // Main building blocks
        AddPaletteShape(DraggableShape.ShapeType.Persegi, Colors.Gray, 120f, 1);       // Big body
        AddPaletteShape(DraggableShape.ShapeType.Persegi, Colors.DarkGray, 48f, 2);    // Towers
        AddPaletteShape(DraggableShape.ShapeType.Persegi, Colors.SlateGray, 24f, 2);   // Windows
        AddPaletteShape(DraggableShape.ShapeType.Persegi, Colors.LightGray, 32f, 1);   // Door

        // Roof triangles
        AddPaletteShape(DraggableShape.ShapeType.SegitigaSamaKaki, Colors.Red, 100f, 1);      // Main roof
        AddPaletteShape(DraggableShape.ShapeType.SegitigaSamaKaki, Colors.DarkRed, 48f, 2);   // Tower roofs
        AddPaletteShape(DraggableShape.ShapeType.SegitigaSamaKaki, Colors.IndianRed, 20f, 2); // Small flags

        // Extra shapes untuk variasi (tidak semua akan terpakai)
        AddPaletteShape(DraggableShape.ShapeType.TrapesiumSiku, Colors.Brown, 60f, 2);
        AddPaletteShape(DraggableShape.ShapeType.JajarGenjang, Colors.SaddleBrown, 50f, 1);
        AddPaletteShape(DraggableShape.ShapeType.Hexagon, Colors.Yellow, 35f, 2);
        AddPaletteShape(DraggableShape.ShapeType.SegitigaSiku, Colors.Tan, 40f, 2);
        AddPaletteShape(DraggableShape.ShapeType.Lingkaran, Colors.Yellow, 25f, 1);    // Sun/Moon

        GD.Print("✅ Hard Level: Custom palette defined (18+ shapes - very challenging!)");
    }

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