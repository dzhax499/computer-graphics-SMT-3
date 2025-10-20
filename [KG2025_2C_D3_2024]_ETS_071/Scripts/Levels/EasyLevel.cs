namespace Godot;

/// <summary>
/// Easy Challenge Level - 7 pieces Tangram Animal
/// </summary>
public partial class EasyLevel : BaseChallengeLevel
{
    private DraggableShape _lastSnapped;

    protected override void SetupUI()
    {
        base.SetupUI();

        var deleteBtn = GetNodeOrNull<TextureButton>("DeleteBtn");
        var undoBtn = GetNodeOrNull<TextureButton>("UndoBtn")
                        ?? GetNodeOrNull<TextureButton>("TextureButton"); 

        if (deleteBtn != null)
        {
            deleteBtn.Pressed += () =>
            {
                if (_lastSnapped != null && _lastSnapped.CanBeDeleted)
                    OnDeleteRequested(_lastSnapped);
            };
        }

        if (undoBtn != null)
        {
            undoBtn.Pressed += () =>
            {
                if (_lastSnapped != null)
                    OnUndoRequested(_lastSnapped);
            };
        }
    }
    protected override void CreateLevelOutlines()
    {
        float baseSize = 50f;

        // Tangram animal (7 pieces) - positioned on board center
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

        GD.Print("✅ Easy Level: 7 outlines created");
    }
    private void OnShapeSnapped(DraggableShape shape)
    {
        base.OnShapeSnapped(shape);
        _lastSnapped = shape;
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