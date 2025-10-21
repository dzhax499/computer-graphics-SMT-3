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
    protected override void DefinePaletteShapes()
    {
        AddPaletteShape(DraggableShape.ShapeType.SegitigaSamaKaki, Colors.Red, 60f, 2);
        AddPaletteShapeAuto(DraggableShape.ShapeType.JajarGenjang, Colors.Red, 1);

        GD.Print("Easy Level: Auto palette from outlines");
    }

    protected override void CreateLevelOutlines()
    {
        float baseSize = 50f;

        // Tangram animal (7 pieces)
        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(-120, -40), baseSize * 0.8f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.Persegi,
            boardCenter + new Vector2(-80, 20), baseSize * 0.7f, 45);

        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(-20, -20), baseSize * 1.2f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(60, -60), baseSize * 1.0f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.JajarGenjang,
            boardCenter + new Vector2(20, 40), baseSize * 1.0f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(100, 60), baseSize * 0.6f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
            boardCenter + new Vector2(80, 80), baseSize * 0.6f, 0);

        CreateOutlineShape(DraggableShape.ShapeType.SegitigaSamaKaki,
        boardCenter + new Vector2(80, 100), baseSize * 0.6f, 0);

        GD.Print("✅ Easy Level: 7 outlines created");
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