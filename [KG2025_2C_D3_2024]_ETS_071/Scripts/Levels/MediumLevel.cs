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

        // tangan
        AddPaletteShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            Colors.Red,
           60f, // Snapping Size
            1,   // Rotation
            70f, // DimAlas
            35f // DimTinggi
        );

        // kaki
        AddPaletteShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            Colors.Purple,
           60f, // Snapping Size
            1,   // count
            70f, // DimAlas
            35f // DimTinggi
        );

        //ban depan/belakang motor

        AddPaletteShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            Colors.SkyBlue,
            80f, // Snapping Size
            1,   // count
            120f, // DimAlas
            60f // DimTinggi
        );

        // ban depan/belakang motor
        AddPaletteShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            Colors.Orange,
            80f, // Snapping Size
            1,   // count
            120f, // DimAlas
            60f // DimTinggi
        );

        // stang motor segitiga sama kaki
        AddPaletteShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            Colors.Orange,
            80f, // Snapping Size
            1,   // Rotation
            90f, // DimAlas
            45f // DimTinggi
        );

        // CONTOH: Jajar Genjang Kustom
        // Format: Type, Color, SnappingSize, Count, Alas, Tinggi, Lebar(0), Skew
        AddPaletteShape(
            DraggableShape.ShapeType.JajarGenjang,
            Colors.Yellow,
            60f, // Snapping Size
            1,   // count
            60f, // DimAlas
            30f, // DimTinggi
            0f,
            25f   // DimSkew
        );
        AddPaletteShape(
            DraggableShape.ShapeType.JajarGenjang,
            Colors.Yellow,
            60f, // Snapping Size
            1,   // count
            50f, // DimAlas
            50f, // DimTinggi
            0f,
            50f   // DimSkew 
        );

        // kepala & pundak

        AddPaletteShape(DraggableShape.ShapeType.Persegi, Colors.Green, 50f, 1,0,0);
        AddPaletteShape(DraggableShape.ShapeType.Persegi, Colors.Blue, 50f, 1,0,0);

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
            DraggableShape.ShapeType.SegitigaSamaKaki, new Vector2(530, 428),
            80f, // Snapping Size
            270,   // Rotation
            90f, // DimAlas
            45f // DimTinggi
        );

        // punggung bawah
        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki, new Vector2(547, 400),
            60f, // Snapping Size
            315,   // Rotation
            70f, // DimAlas
            35f // DimTinggi
        );

        // dudukan motor

        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki, new Vector2(554, 459),
            60f, // Snapping Size
            0,   // Rotation
            70f, // DimAlas
            35f // DimTinggi
        );
        // punggung atas
        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki, new Vector2(530, 366),
            60f, // Snapping Size
            135,   // Rotation
            70f, // DimAlas
            35f // DimTinggi
        );

        // pundak
        CreateOutlineShape(DraggableShape.ShapeType.Persegi, new Vector2(488, 359), 50, 0, 0, 0);

        // kepala 
        CreateOutlineShape(DraggableShape.ShapeType.Persegi, new Vector2(446, 317), 50, 315, 0, 0);

        //lengan
        CreateOutlineShape(DraggableShape.ShapeType.JajarGenjang, new Vector2(438, 423),
            60f, // Snapping Size
            0,   // Rotation
            50f, // DimAlas
            50f, // DimTinggi
            0f,
            50f
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

    private void _on_BtnBack_pressed()
    {
        GD.Print("🔙 Going back to Welcome screen");
        GetTree().ChangeSceneToFile("res://Scenes/Welcome.tscn");
    }
}