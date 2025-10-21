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

    // drag and drop
    // protected override void DefinePaletteShapes()
    // {
    //     AddPaletteShape(DraggableShape.ShapeType.SegitigaSamaKaki, Colors.Red, 60f, 2);
    //     AddPaletteShapeAuto(DraggableShape.ShapeType.JajarGenjang, Colors.Red, 1);

    //     GD.Print("Easy Level: Auto palette from outlines");
    // }

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
            2,   // Count
            100f, // DimAlas (Alas 80)
            50f // DimTinggi (Tinggi 120)
        );

        
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

        // otomatis mengambil dimensi dari outline yang cocok
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
            80f, // Snapping Size
            0,   // Rotation
            100f, // DimAlas
            50f // DimTinggi
        );
        CreateOutlineShape(
            DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(-120, -40),
            80f, // Snapping Size
            45,   // Rotation
            100f, // DimAlas
            50f // DimTinggi
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