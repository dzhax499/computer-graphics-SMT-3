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
        // CONTOH: Segitiga Sama Kaki Kustom
        // Format: Type, Color, SnappingSize, Count, Alas, Tinggi
        AddPaletteShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            Colors.Red,
            80f, // Snapping Size (bisa disamakan dengan alas)
            2,   // Count
            80f, // DimAlas (Alas 80)
            120f // DimTinggi (Tinggi 120)
        );

        // CONTOH: Jajar Genjang Kustom
        // Format: Type, Color, SnappingSize, Count, Alas, Tinggi, Lebar(0), Skew
        AddPaletteShape(
            DraggableShape.ShapeType.JajarGenjang,
            Colors.Blue,
            100f, // Snapping Size
            1,    // Count
            100f, // DimAlas (Alas 100)
            40f,  // DimTinggi (Tinggi 40)
            0f,   // DimLebar (tidak dipakai)
            20f   // DimSkew (Kemiringan 20)
        );

        // Anda masih bisa menggunakan method lama
        AddPaletteShape(DraggableShape.ShapeType.Persegi, Colors.Green, 50f, 1);

        // Atau menggunakan AddPaletteShapeAuto
        // Ini akan otomatis mengambil dimensi dari outline yang cocok
        // AddPaletteShapeAuto(DraggableShape.ShapeType.JajarGenjang, Colors.Red, 1);

        GD.Print("Easy Level: Custom palette defined");
    }

    protected override void CreateLevelOutlines()
    {

        // CONTOH: Membuat Outline Segitiga Kustom
        // Format: Type, Position, SnappingSize, Rotation, Alas, Tinggi
        // CONTOH: Membuat Outline Jajar Genjang Kustom
        // Format: Type, Position, SnappingSize, Rotation, Alas, Tinggi, Lebar(0), Skew
        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(-120, -40),
            80f, // Snapping Size (harus SAMA dengan palette)
            0,   // Rotation
            80f, // DimAlas (harus SAMA dengan palette)
            120f // DimTinggi (harus SAMA dengan palette)
        );
        CreateOutlineShape(
            DraggableShape.ShapeType.JajarGenjang,
            boardCenter + new Vector2(20, 40),
            100f, // Snapping Size
            0,    // Rotation
            100f, // DimAlas
            40f,  // DimTinggi
            0f,   // DimLebar
            20f   // DimSkew
        );
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