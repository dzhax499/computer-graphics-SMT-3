namespace Godot;

public partial class MediumLevel : BaseChallengeLevel
{
    private Button pauseButton;
    protected override void SetupUI()
    {
        base.SetupUI();

        pauseButton = GetNodeOrNull<Button>("PauseButton");
        if (pauseButton != null && pauseMenu != null)
        {
            pauseButton.Pressed += () => pauseMenu.TogglePause();
            GD.Print("Pause button connected");
        }

        var deleteBtn = GetNodeOrNull<TextureButton>("DeleteBtn");
        var undoBtn = GetNodeOrNull<TextureButton>("UndoBtn")
                        ?? GetNodeOrNull<TextureButton>("TextureButton");

        if (deleteBtn != null)
            deleteBtn.Pressed += () =>
            {
                if (_activeShape != null)
                    OnDeleteRequested(_activeShape);
            };

        if (undoBtn != null)
            undoBtn.Pressed += () =>
            {
                if (_activeShape != null)
                    OnUndoRequested(_activeShape);
            };
    }
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

        // ban roda depan 2 segitga sama kaki
        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki, new Vector2(357, 528),
            80f, // Snapping Size
            90,   // Rotation
            120f, // DimAlas
            60f // DimTinggi
        );

        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki, new Vector2(397, 528),
            80f, // Snapping Size
            270,   // Rotation
            120f, // DimAlas
            60f // DimTinggi
        );
        // stang motor
        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki, new Vector2(397, 428),
            80f, // Snapping Size
            45,   // Rotation
            90f, // DimAlas
            45f // DimTinggi
        );
        // ban belakang 2 segitga sama kaki
        CreateOutlineShape(
        DraggableShape.ShapeType.SegitigaSamaKaki, new Vector2(540, 528),
        80f, 90, 120f, 60f
        );

        CreateOutlineShape(
             DraggableShape.ShapeType.SegitigaSamaKaki, new Vector2(580, 528),
             80f, 270, 120f, 60f
         );

        // kaki
        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki, new Vector2(480, 528),
            60f, // Snapping Size
            225,   // Rotation
            70f, // DimAlas
            35f // DimTinggi
        );

        // paha atas
        CreateOutlineShape(
            DraggableShape.ShapeType.JajarGenjang, new Vector2(490, 488),
            60f, // Snapping Size
            45,   // Rotation
            60f, // DimAlas
            30f, // DimTinggi
            0f,
            25f
        );

        //pinggang
        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki, new Vector2(530, 438),
            80f, // Snapping Size
            270,   // Rotation
            90f, // DimAlas
            45f // DimTinggi
        );
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