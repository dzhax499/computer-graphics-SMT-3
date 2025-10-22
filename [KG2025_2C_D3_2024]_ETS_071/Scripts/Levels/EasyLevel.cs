namespace Godot;

public partial class EasyLevel : BaseChallengeLevel
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


    /// Membuat pause button secara programmatic
    private void CreatePauseButton()
    {
        pauseButton = new Button();
        pauseButton.Text = "⏸ PAUSE";
        pauseButton.Position = new Vector2(20, 20);
        pauseButton.Size = new Vector2(100, 40);

        // Styling (optional)
        pauseButton.AddThemeColorOverride("font_color", Colors.White);

        // Connect signal
        pauseButton.Pressed += () => pauseMenu.TogglePause();

        // Add to scene
        AddChild(pauseButton);

        GD.Print("Pause button created programmatically");
    }


    protected override void DefinePaletteShapes()
    {
        // Segitiga Sama Kaki 
        // Format: Type, Color, SnappingSize, Count, Alas, Tinggi
        // Jajar Genjang
        // Format: Type, Color, SnappingSize, Count, Alas, Tinggi, Lebar(0), Skew
        AddPaletteShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            Colors.Red,
            80f, // Snapping Size (bisa disamakan dengan alas)
            1,   // Count
            120f, // DimAlas
            60f // DimTinggi (Tinggi 120)
        );

        AddPaletteShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            Colors.Purple,
            80f, // Snapping Size
            1,   // Count
            120f, // DimAlas
            60f // DimTinggi (Tinggi 120)
        );


        AddPaletteShape(
            DraggableShape.ShapeType.JajarGenjang,
            Colors.DarkBlue,
            60f, // Snapping Size
            1,    // count
            60f, // DimAlas
            30f,  // DimTinggi
            0f,   // DimLebar
            30f   // DimSkew (Kemiringan 20)
        );

        AddPaletteShape(DraggableShape.ShapeType.Persegi, Colors.Green, 45f, 1);

        AddPaletteShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            Colors.Yellow,
            50f, // Snapping Size
            1,   // Rotation
            60f, // DimAlas
            30f // DimTinggi (Tinggi 120)
        );

        AddPaletteShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            Colors.DarkBlue,
            50f, // Snapping Size
            1,   // Rotation
            60f, // DimAlas
            30f // DimTinggi (Tinggi 120)
        );
        AddPaletteShape(
                DraggableShape.ShapeType.SegitigaSamaKaki,
                Colors.Orange,
                80f, // Snapping Size
                1,   
                100f, // DimAlas
                50f
            );

        // otomatis mengambil dimensi dari outline yang cocok
        // AddPaletteShapeAuto(DraggableShape.ShapeType.JajarGenjang, Colors.Red, 1);

        GD.Print("Easy Level: Custom palette defined");
    }

    protected override void CreateLevelOutlines()
    {

        // Segitiga 
        // Format: Type, Position, SnappingSize, Rotation, Alas, Tinggi
        // Jajar Genjang 
        // Format: Type, Position, SnappingSize, Rotation, Alas, Tinggi, Lebar(0), Skew

        // jok motor
        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(47, -132),
            80f, // Snapping Size
            180,   // Rotation
            120f, // DimAlas
            60f // DimTinggi
        );

        // stang motor
        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(137, -142),
            80f, // Snapping Size
            0,   // Rotation
            120f, // DimAlas
            60f // DimTinggi
        );

        //mesin motor
        CreateOutlineShape(
            DraggableShape.ShapeType.JajarGenjang,
            boardCenter + new Vector2(77, -98),
            60f, // Snapping Size
            0,    // Rotation
            60f, // DimAlas
            30f,  // DimTinggi
            0f,   // DimLebar
            30f   // DimSkew
        );

        // roda depan (segitiga ada 2)
        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(137, -100),
            50f, // Snapping Size
            0,   // Rotation
            60f, // DimAlas
            30f // DimTinggi
        );

        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(137, -80),
            50f, // Snapping Size
            180,   // Rotation
            60f, // DimAlas
            30f // DimTinggi
        );

        // roda belakang
        CreateOutlineShape(DraggableShape.ShapeType.Persegi, boardCenter + new Vector2(17, -93), 45, 45, 0, 0);

        // penahan duduk
        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(-17, -122),
            80f, // Snapping Size
            270,   // Rotation
            100f, // DimAlas
            50f // DimTinggi
        );

        GD.Print("✅ Easy Level: Custom outlines created");
    }

    protected override string GetLevelTitle()
    {
        return "LEVEL EASY";
    }

    protected override string GetNextLevelPath()
    {
        return "res://Scenes/MediumLevel.tscn";
    }

    protected override bool IsLastLevel()
    {
        return false;
    }
}