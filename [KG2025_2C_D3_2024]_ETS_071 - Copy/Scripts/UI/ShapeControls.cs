namespace Godot;

using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ShapeControls : Control
{
    [Signal] public delegate void UndoRequestedEventHandler(DraggableShape shape);
    [Signal] public delegate void DeleteRequestedEventHandler(DraggableShape shape);

    private DraggableShape targetShape;
    private Button undoButton;
    private Button deleteButton;
    private VBoxContainer buttonContainer;

    public override void _Ready()
    {
        CreateUI();
        Visible = false;
    }

    private void CreateUI()
    {
        // Create container for buttons
        buttonContainer = new VBoxContainer();
        buttonContainer.AddThemeConstantOverride("separation", 5);
        AddChild(buttonContainer);

        // Create Undo button
        undoButton = new Button();
        undoButton.Text = "UNDO";
        undoButton.Size = new Vector2(80, 30);
        undoButton.AddThemeColorOverride("font_color", Colors.White);
        undoButton.AddThemeColorOverride("font_color_hover", Colors.LightBlue);
        undoButton.AddThemeColorOverride("font_color_pressed", Colors.DarkBlue);
        undoButton.Pressed += OnUndoPressed;
        buttonContainer.AddChild(undoButton);

        // Create Delete button
        deleteButton = new Button();
        deleteButton.Text = "DELETE";
        deleteButton.Size = new Vector2(80, 30);
        deleteButton.AddThemeColorOverride("font_color", Colors.White);
        deleteButton.AddThemeColorOverride("font_color_hover", Colors.LightCoral);
        deleteButton.AddThemeColorOverride("font_color_pressed", Colors.DarkRed);
        deleteButton.Pressed += OnDeletePressed;
        buttonContainer.AddChild(deleteButton);
    }

    public void ShowForShape(DraggableShape shape)
    {
        if (shape == null || !shape.IsSnapped) return;

        targetShape = shape;

        // Position controls near the shape
        Vector2 shapePos = shape.GlobalPosition;
        GlobalPosition = shapePos + new Vector2(60, -40);

        // Update button states
        undoButton.Disabled = false;
        deleteButton.Disabled = false;

        Visible = true;
    }

    public void HideControls()
    {
        targetShape = null;
        Visible = false;
    }

    private void OnUndoPressed()
    {
        if (targetShape != null)
        {
            EmitSignal(SignalName.UndoRequested, targetShape);
            HideControls();
        }
    }

    private void OnDeletePressed()
    {
        if (targetShape != null)
        {
            EmitSignal(SignalName.DeleteRequested, targetShape);
            HideControls();
        }
    }

    public override void _Process(double delta)
    {
        // Hide controls if target shape is no longer snapped
        if (targetShape != null && !targetShape.IsSnapped)
        {
            HideControls();
        }
    }
}
