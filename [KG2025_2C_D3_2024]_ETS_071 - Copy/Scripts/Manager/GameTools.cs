namespace Godot;

/// <summary>
/// Game Tools Manager - Handles Undo and Delete functionality
/// Replaces ShapeControls with better positioning and visibility
/// </summary>
public partial class GameTools : Control
{
    [Signal] public delegate void UndoRequestedEventHandler(DraggableShape shape);
    [Signal] public delegate void DeleteRequestedEventHandler(DraggableShape shape);

    private DraggableShape targetShape;
    private Button undoButton;
    private Button deleteButton;
    private HBoxContainer buttonContainer;
    private Panel backgroundPanel;

    public override void _Ready()
    {
        CreateUI();
        Visible = false;
    }

    private void CreateUI()
    {
        // Background panel for better visibility
        backgroundPanel = new Panel();
        var styleBox = new StyleBoxFlat();
        styleBox.BgColor = new Color(0, 0, 0, 0.7f);
        styleBox.CornerRadiusTopLeft = 8;
        styleBox.CornerRadiusTopRight = 8;
        styleBox.CornerRadiusBottomLeft = 8;
        styleBox.CornerRadiusBottomRight = 8;
        backgroundPanel.AddThemeStyleboxOverride("panel", styleBox);
        AddChild(backgroundPanel);

        // Create container for buttons
        buttonContainer = new HBoxContainer();
        buttonContainer.AddThemeConstantOverride("separation", 10);
        backgroundPanel.AddChild(buttonContainer);

        // Create Undo button
        undoButton = new Button();
        undoButton.Text = "↺ UNDO";
        undoButton.CustomMinimumSize = new Vector2(90, 35);

        var undoStyleNormal = new StyleBoxFlat();
        undoStyleNormal.BgColor = new Color(0.3f, 0.5f, 0.8f);
        undoStyleNormal.CornerRadiusTopLeft = 5;
        undoStyleNormal.CornerRadiusTopRight = 5;
        undoStyleNormal.CornerRadiusBottomLeft = 5;
        undoStyleNormal.CornerRadiusBottomRight = 5;

        var undoStyleHover = new StyleBoxFlat();
        undoStyleHover.BgColor = new Color(0.4f, 0.6f, 0.9f);
        undoStyleHover.CornerRadiusTopLeft = 5;
        undoStyleHover.CornerRadiusTopRight = 5;
        undoStyleHover.CornerRadiusBottomLeft = 5;
        undoStyleHover.CornerRadiusBottomRight = 5;

        undoButton.AddThemeStyleboxOverride("normal", undoStyleNormal);
        undoButton.AddThemeStyleboxOverride("hover", undoStyleHover);
        undoButton.AddThemeColorOverride("font_color", Colors.White);
        undoButton.AddThemeFontSizeOverride("font_size", 16);
        undoButton.Pressed += OnUndoPressed;
        buttonContainer.AddChild(undoButton);

        // Create Delete button
        deleteButton = new Button();
        deleteButton.Text = "🗑 DELETE";
        deleteButton.CustomMinimumSize = new Vector2(90, 35);

        var deleteStyleNormal = new StyleBoxFlat();
        deleteStyleNormal.BgColor = new Color(0.8f, 0.2f, 0.2f);
        deleteStyleNormal.CornerRadiusTopLeft = 5;
        deleteStyleNormal.CornerRadiusTopRight = 5;
        deleteStyleNormal.CornerRadiusBottomLeft = 5;
        deleteStyleNormal.CornerRadiusBottomRight = 5;

        var deleteStyleHover = new StyleBoxFlat();
        deleteStyleHover.BgColor = new Color(0.9f, 0.3f, 0.3f);
        deleteStyleHover.CornerRadiusTopLeft = 5;
        deleteStyleHover.CornerRadiusTopRight = 5;
        deleteStyleHover.CornerRadiusBottomLeft = 5;
        deleteStyleHover.CornerRadiusBottomRight = 5;

        deleteButton.AddThemeStyleboxOverride("normal", deleteStyleNormal);
        deleteButton.AddThemeStyleboxOverride("hover", deleteStyleHover);
        deleteButton.AddThemeColorOverride("font_color", Colors.White);
        deleteButton.AddThemeFontSizeOverride("font_size", 16);
        deleteButton.Pressed += OnDeletePressed;
        buttonContainer.AddChild(deleteButton);

        // Set container size to fit buttons
        backgroundPanel.CustomMinimumSize = new Vector2(210, 50);
        buttonContainer.Position = new Vector2(10, 7);
    }

    public void ShowForShape(DraggableShape shape)
    {
        if (shape == null || !shape.IsSnapped) return;

        targetShape = shape;

        // Position tools ABOVE the shape for better visibility
        Vector2 shapePos = shape.GlobalPosition;
        GlobalPosition = shapePos + new Vector2(-105, -70);

        // Make sure it's on top
        ZIndex = 100;

        undoButton.Disabled = false;
        deleteButton.Disabled = false;

        Visible = true;
    }

    public void HideTools()
    {
        targetShape = null;
        Visible = false;
    }

    private void OnUndoPressed()
    {
        if (targetShape != null)
        {
            EmitSignal(SignalName.UndoRequested, targetShape);
            HideTools();
        }
    }

    private void OnDeletePressed()
    {
        if (targetShape != null)
        {
            EmitSignal(SignalName.DeleteRequested, targetShape);
            HideTools();
        }
    }

    public override void _Process(double delta)
    {
        // Hide tools if target shape is no longer snapped or deleted
        if (targetShape != null && (!IsInstanceValid(targetShape) || !targetShape.IsSnapped))
        {
            HideTools();
        }
    }
}