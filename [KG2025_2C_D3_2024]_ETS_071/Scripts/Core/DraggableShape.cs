namespace Godot;

using Godot;
using System;
using System.Collections.Generic;
using System.Numerics;

public partial class DraggableShape : Node2D
{
    public enum ShapeType
    {
        Persegi,
        TrapesiumSiku,
        SegitigaSamaKaki,
        SegitigaSiku,
        Hexagon,
        Lingkaran
    }

    // Shape properties
    public ShapeType Type { get; set; }
    public Color ShapeColor { get; set; }
    public float ShapeSize { get; set; } = 50f;
    public List<Vector2> OriginalPoints { get; private set; }
    public List<Vector2> TransformedPoints { get; private set; }

    // Drag properties
    private bool isDragging = false;
    private Vector2 dragOffset;
    private Vector2 shapeCenter;

    // Transformation
    private TransformasiFast transformasi;
    private Matrix4x4 transformMatrix;
    private float currentRotation = 0f; // in degrees

    // BentukDasar for generating shapes
    private BentukDasar bentukDasar;

    // Bounds for collision detection
    private Rect2 bounds;

    public override void _Ready()
    {
        bentukDasar = new BentukDasar();
        transformasi = new TransformasiFast();
        transformMatrix = TransformasiFast.Identity();

        GenerateShape();
        CalculateBounds();
    }

    private void GenerateShape()
    {
        OriginalPoints = new List<Vector2>();

        // Generate shape based on type (in world coordinates, center at 0,0)
        switch (Type)
        {
            case ShapeType.Persegi:
                OriginalPoints = bentukDasar.Persegi(-ShapeSize / 2, -ShapeSize / 2, ShapeSize);
                break;

            case ShapeType.TrapesiumSiku:
                OriginalPoints = bentukDasar.TrapesiumSiku(
                    new Vector2(-ShapeSize / 2, -ShapeSize / 3),
                    (int)(ShapeSize * 0.6f),
                    (int)ShapeSize,
                    (int)(ShapeSize * 0.6f)
                );
                break;

            case ShapeType.SegitigaSamaKaki:
                OriginalPoints = bentukDasar.SegitigaSamaKaki(
                    new Vector2(-ShapeSize / 2, -ShapeSize / 3),
                    (int)ShapeSize,
                    (int)(ShapeSize * 0.8f)
                );
                break;

            case ShapeType.SegitigaSiku:
                OriginalPoints = bentukDasar.SegitigaSiku(
                    new Vector2(-ShapeSize / 2, -ShapeSize / 2),
                    (int)ShapeSize,
                    (int)ShapeSize
                );
                break;

            case ShapeType.Hexagon:
                OriginalPoints = bentukDasar.Hexagon(Vector2.Zero, ShapeSize / 2, 0);
                break;

            case ShapeType.Lingkaran:
                OriginalPoints = bentukDasar.Lingkaran(Vector2.Zero, (int)(ShapeSize / 2));
                break;
        }

        TransformedPoints = new List<Vector2>(OriginalPoints);
    }

    private void CalculateBounds()
    {
        if (TransformedPoints == null || TransformedPoints.Count == 0) return;

        float minX = float.MaxValue, minY = float.MaxValue;
        float maxX = float.MinValue, maxY = float.MinValue;

        foreach (var point in TransformedPoints)
        {
            if (point.X < minX) minX = point.X;
            if (point.Y < minY) minY = point.Y;
            if (point.X > maxX) maxX = point.X;
            if (point.Y > maxY) maxY = point.Y;
        }

        bounds = new Rect2(minX, minY, maxX - minX, maxY - minY);
    }

    public override void _Process(double delta)
    {
        if (isDragging)
        {
            GlobalPosition = GetGlobalMousePosition() - dragOffset;
            QueueRedraw();
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.Left)
            {
                if (mouseButton.Pressed)
                {
                    Vector2 localMousePos = GetGlobalMousePosition() - GlobalPosition;
                    if (IsPointInShape(localMousePos))
                    {
                        isDragging = true;
                        dragOffset = GetGlobalMousePosition() - GlobalPosition;
                        GetViewport().SetInputAsHandled();
                    }
                }
                else
                {
                    isDragging = false;
                }
            }
        }

        // Rotation with R key
        if (@event is InputEventKey keyEvent && keyEvent.Pressed)
        {
            if (keyEvent.Keycode == Key.R && IsMouseOver())
            {
                RotateShape(15f); // Rotate 15 degrees
                GetViewport().SetInputAsHandled();
            }
        }
    }

    private bool IsPointInShape(Vector2 point)
    {
        // Simple bounds check
        return bounds.HasPoint(point);
    }

    private bool IsMouseOver()
    {
        Vector2 localMousePos = GetGlobalMousePosition() - GlobalPosition;
        return IsPointInShape(localMousePos);
    }

    public void RotateShape(float degrees)
    {
        currentRotation += degrees;
        float radians = Mathf.DegToRad(degrees);

        transformasi.RotationClockwise(ref transformMatrix, radians, Vector2.Zero);
        TransformedPoints = transformasi.GetTransformPoint(transformMatrix, OriginalPoints);

        CalculateBounds();
        QueueRedraw();
    }

    public void ResetTransformation()
    {
        transformMatrix = TransformasiFast.Identity();
        currentRotation = 0f;
        TransformedPoints = new List<Vector2>(OriginalPoints);
        CalculateBounds();
        QueueRedraw();
    }

    public override void _Draw()
    {
        if (TransformedPoints == null || TransformedPoints.Count == 0) return;

        // Draw filled polygon
        GraphicsUtils.FillPolygon(this, TransformedPoints, ShapeColor);

        // Draw outline
        GraphicsUtils.PutPixelAll(this, TransformedPoints, GraphicsUtils.DrawStyle.DotDot, Colors.Black);

        // Draw bounds for debugging (optional)
        // DrawRect(bounds, Colors.Yellow, false, 1.0f);
    }

    public override void _ExitTree()
    {
        bentukDasar?.Dispose();
    }
}