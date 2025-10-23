namespace Godot;

public partial class HardLevel : BaseChallengeLevel
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
    private void CreatePauseButton()
    {
        pauseButton = new Button();
        pauseButton.Text = "⏸ PAUSE";
        pauseButton.Position = new Vector2(20, 20);
        pauseButton.Size = new Vector2(100, 40);

        // Connect signal
        pauseButton.Pressed += () => pauseMenu.TogglePause();

        // Add to scene
        AddChild(pauseButton);

        GD.Print("Pause button created programmatically");
    }
    protected override float GetLevelRotationStep()
    {
        return 30f; //mengubah rotasi default menjadi 30 derajat
    }
    /// untuk MENGAKTIFKAN tombol 'T' pada level ini
    protected override bool AllowRotationToggle => true;
    protected override void DefinePaletteShapes()
    {
        // CONTOH: Segitiga Sama Kaki Kustom
        // Format: Type, Color, SnappingSize, Count, Alas, Tinggi
        AddPaletteShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            Colors.DarkGreen,
            80f, // Snapping Size 
            1,   // Count
            40f, // DimAlas 
            30f // DimTinggi 
        );

        // CONTOH: Jajar Genjang Kustom
        // Format: Type, Color, SnappingSize, Count, Alas, Tinggi, Lebar(0), Skew
        AddPaletteShape(
            DraggableShape.ShapeType.JajarGenjang,
            Colors.Blue,
            100f, // Snapping Size
            1,    // Count
            40f, // DimAlas 
            30f,  // DimTinggi 
            0f,   // DimLebar 
            -20f   // DimSkew 
        );

        // jajar putih
        AddPaletteShape(
            DraggableShape.ShapeType.JajarGenjang,
            Colors.White,
            100f, // Snapping Size
            1,    // Count
            40f, // DimAlas 
            30f,  // DimTinggi 
            0f,   // DimLebar 
            -40f   // DimSkew 
        );

        AddPaletteShape(DraggableShape.ShapeType.Hexagon,
            Colors.Yellow,
            80f, // Snapping Size
            1     // Count
        );

        // trapesium sama kaki , sisi atas = lebar
        AddPaletteShape(DraggableShape.ShapeType.TrapesiumSamaKaki,
            Colors.OrangeRed,
            100f, // Snapping Size
            1,    // Count
            75f,  // DimAlas
            40f, // DimTinggi = sisi atas
            30f // DSimLebar = tinggi
        );

        // Atau menggunakan AddPaletteShapeAuto
        // Ini akan otomatis mengambil dimensi dari outline yang cocok
        // AddPaletteShapeAuto(DraggableShape.ShapeType.JajarGenjang, Colors.Red, 1);

        GD.Print("Easy Level: Custom palette defined");
    }

    protected override void CreateLevelOutlines()
    {
        // ban depan
        CreateOutlineShape(DraggableShape.ShapeType.Hexagon,
            new Vector2(457, 546),
            80f, // Snapping Size
            0,    // Rotation
            0, 0, 0, 0
        );
        CreateOutlineShape(DraggableShape.ShapeType.TrapesiumSamaKaki,
            new Vector2(457, 597),
            100f, // Snapping Size
            0,    // Rotation
            75f,  // DimAlas
            40f, // DimTinggi = sisi atas
            30f // DSimLebar = tinggi
        );
        CreateOutlineShape(DraggableShape.ShapeType.TrapesiumSamaKaki,
            new Vector2(502, 571),
            100f, // Snapping Size
            60,    // Rotation
            75f,  // DimAlas
            40f, // DimTinggi = sisi atas
            30f // DSimLebar = tinggi
        );
        CreateOutlineShape(DraggableShape.ShapeType.TrapesiumSamaKaki,
            new Vector2(414, 571),
            100f, // Snapping Size
            300,    // Rotation
            75f,  // DimAlas
            40f, // DimTinggi = sisi atas
            30f // DSimLebar = tinggi
        );
        CreateOutlineShape(DraggableShape.ShapeType.TrapesiumSamaKaki,
            new Vector2(414, 521),
            100f, // Snapping Size
            240,    // Rotation
            75f,  // DimAlas
            40f, // DimTinggi = sisi atas
            30f // DSimLebar = tinggi
        );

        // JOK MOTOR
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            new Vector2(437, 493),
            80f, // Snapping Size 
            180,   // Count
            40f, // DimAlas 
            30f // DimTinggi 
        );
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            new Vector2(457, 503),
            80f, // Snapping Size 
            0,   // Count
            40f, // DimAlas 
            30f // DimTinggi 
        );
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            new Vector2(477, 493),
            80f, // Snapping Size 
            180,   // Count
            40f, // DimAlas 
            30f // DimTinggi 
        );
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            new Vector2(497, 523),
            80f, // Snapping Size 
            180,   // Count
            40f, // DimAlas 
            30f // DimTinggi 
        );
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            new Vector2(497, 503),
            80f, // Snapping Size 
            0,   // Count
            40f, // DimAlas 
            30f // DimTinggi 
        );
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            new Vector2(517, 493),
            80f, // Snapping Size 
            180,   // Count
            40f, // DimAlas 
            30f // DimTinggi 
        );
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            new Vector2(517, 533),
            80f, // Snapping Size 
            0,   // Count
            40f, // DimAlas 
            30f // DimTinggi 
        );
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            new Vector2(539, 523),
            80f, // Snapping Size 
            180,   // Count
            40f, // DimAlas 
            30f // DimTinggi 
        );

        //tangki motor
        CreateOutlineShape(DraggableShape.ShapeType.JajarGenjang,
            new Vector2(537, 503),
            100f, // Snapping Size
            0,    // Rotation
            40f, // DimAlas 
            30f,  // DimTinggi 
            0f,   // DimLebar 
            20f   // DimSkew 
        );
        CreateOutlineShape(DraggableShape.ShapeType.JajarGenjang,
            new Vector2(577, 503),
            100f, // Snapping Size
            0,    // Rotation
            40f, // DimAlas 
            30f,  // DimTinggi 
            0f,   // DimLebar 
            20f   // DimSkew 
        );
        CreateOutlineShape(DraggableShape.ShapeType.JajarGenjang,
            new Vector2(557, 473),
            100f, // Snapping Size
            0,    // Rotation
            40f, // DimAlas 
            30f,  // DimTinggi 
            0f,   // DimLebar 
            20f   // DimSkew 
        );
        CreateOutlineShape(DraggableShape.ShapeType.JajarGenjang,
            new Vector2(598, 473),
            100f, // Snapping Size
            0,    // Rotation
            40f, // DimAlas 
            30f,  // DimTinggi 
            0f,   // DimLebar 
            20f   // DimSkew 
        );

        // shock depan motor
        CreateOutlineShape(DraggableShape.ShapeType.JajarGenjang,
            new Vector2(618, 443),
            100f, // Snapping Size
            0,    // Rotation
            40f, // DimAlas 
            30f,  // DimTinggi 
            0f,   // DimLebar 
            -20f   // DimSkew 
        );
        CreateOutlineShape(DraggableShape.ShapeType.JajarGenjang,
            new Vector2(598, 413),
            100f, // Snapping Size
            0,    // Rotation
            40f, // DimAlas 
            30f,  // DimTinggi 
            0f,   // DimLebar 
            -20f   // DimSkew 
        );
        CreateOutlineShape(DraggableShape.ShapeType.JajarGenjang,
            new Vector2(638, 493),
            100f, // Snapping Size
            300,    // Rotation
            40f, // DimAlas 
            30f,  // DimTinggi 
            0f,   // DimLebar 
            -20f   // DimSkew 
        );
        CreateOutlineShape(DraggableShape.ShapeType.JajarGenjang,
            new Vector2(663, 528),
            100f, // Snapping Size
            300,    // Rotation
            40f, // DimAlas 
            30f,  // DimTinggi 
            0f,   // DimLebar 
            -20f   // DimSkew 
        );

        //stang motor
        CreateOutlineShape(DraggableShape.ShapeType.JajarGenjang,
            new Vector2(577, 383),
            100f, // Snapping Size
            0,    // Rotation
            40f, // DimAlas 
            30f,  // DimTinggi 
            0f,   // DimLebar 
            -40f   // DimSkew 
        );
        // tarikan gas motor
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            new Vector2(523, 378),
            80f, // Snapping Size 
            30,   // Count
            40f, // DimAlas 
            30f // DimTinggi 
        );

        // ban belakang
        CreateOutlineShape(DraggableShape.ShapeType.Hexagon,
           new Vector2(703, 547),
           80f, // Snapping Size
           0,    // Rotation
           0, 0, 0, 0
       );
        CreateOutlineShape(DraggableShape.ShapeType.TrapesiumSamaKaki,
            new Vector2(703, 597),
            100f, // Snapping Size
            0,    // Rotation
            75f,  // DimAlas
            40f, // DimTinggi = sisi atas
            30f // DSimLebar = tinggi
        );
        CreateOutlineShape(DraggableShape.ShapeType.TrapesiumSamaKaki,
            new Vector2(750, 572),
            100f, // Snapping Size
            60,    // Rotation
            75f,  // DimAlas
            40f, // DimTinggi = sisi atas
            30f // DSimLebar = tinggi
        );
        CreateOutlineShape(DraggableShape.ShapeType.TrapesiumSamaKaki,
            new Vector2(660, 576),
            100f, // Snapping Size
            300,    // Rotation
            75f,  // DimAlas
            40f, // DimTinggi = sisi atas
            30f // DSimLebar = tinggi
        );
        CreateOutlineShape(DraggableShape.ShapeType.TrapesiumSamaKaki,
            new Vector2(747, 520),
            100f, // Snapping Size
            120,    // Rotation
            75f,  // DimAlas
            40f, // DimTinggi = sisi atas
            30f // DSimLebar = tinggi
        );
        CreateOutlineShape(DraggableShape.ShapeType.TrapesiumSamaKaki,
            new Vector2(697, 497),
            100f, // Snapping Size
            180,    // Rotation
            75f,  // DimAlas
            40f, // DimTinggi = sisi atas
            30f // DSimLebar = tinggi
        );
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            new Vector2(643, 536),
            80f, // Snapping Size 
            0,   // rotation
            40f, // DimAlas 
            30f // DimTinggi 
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
    private void _on_BtnBack_pressed()
    {
        GD.Print("🔙 Going back to Welcome screen");
        GetTree().ChangeSceneToFile("res://Scenes/Welcome.tscn");
    }
}