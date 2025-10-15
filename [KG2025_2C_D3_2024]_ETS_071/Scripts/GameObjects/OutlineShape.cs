namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

public partial class OutlineShape : Node2D
{
    // Shape properties
    public DraggableShape.ShapeType Type { get; set; }
    public float ShapeSize { get; set; } = 50f;
    public float InitialRotation { get; set; } = 0f;
    public Color OutlineColor { get; set; } = new Color(0.5f, 0.5f, 0.5f, 0.8f);

    private BentukDasar bentukDasar;
    private Transformasi transformasi;
    private List<Vector2> shapePoints;
    private List<Vector2> transformedPoints;
    private float[,] transformMatrix;

    public override void _Ready()
    {
        bentukDasar = new BentukDasar();
        transformasi = new Transformasi();
        transformMatrix = new float[3, 3];
        Transformasi.Matrix3x3Identity(transformMatrix);

        GenerateShape();
        ApplyInitialRotation();
        QueueRedraw();
    }

    private void GenerateShape()
    {
        shapePoints = new List<Vector2>();

        switch (Type)
        {
            case DraggableShape.ShapeType.Persegi:
                shapePoints = bentukDasar.Persegi(-ShapeSize / 2, -ShapeSize / 2, ShapeSize);
                break;

            case DraggableShape.ShapeType.TrapesiumSiku:
                shapePoints = bentukDasar.TrapesiumSiku(
                    new Vector2(-ShapeSize / 2, -ShapeSize / 3),
                    (int)(ShapeSize * 0.6f),
                    (int)ShapeSize,
                    (int)(ShapeSize * 0.6f)
                );
                break;

            case DraggableShape.ShapeType.SegitigaSamaKaki:
                shapePoints = bentukDasar.SegitigaSamaKaki(
                    new Vector2(-ShapeSize / 2, -ShapeSize / 3),
                    (int)ShapeSize,
                    (int)(ShapeSize * 0.8f)
                );
                break;

            case DraggableShape.ShapeType.SegitigaSiku:
                shapePoints = bentukDasar.SegitigaSiku(
                    new Vector2(-ShapeSize / 2, -ShapeSize / 2),
                    (int)ShapeSize,
                    (int)ShapeSize
                );
                break;

            case DraggableShape.ShapeType.Hexagon:
                shapePoints = bentukDasar.Hexagon(Vector2.Zero, ShapeSize / 2, 0);
                break;

            case DraggableShape.ShapeType.Lingkaran:
                shapePoints = bentukDasar.Lingkaran(Vector2.Zero, (int)(ShapeSize / 2));
                break;

            case DraggableShape.ShapeType.JajarGenjang:
                shapePoints = bentukDasar.JajarGenjang(
                    new Vector2(-ShapeSize / 2, -ShapeSize / 4),
                    (int)ShapeSize,
                    (int)(ShapeSize * 0.6f),
                    (int)(ShapeSize * 0.3f)
                );
                break;
        }

        transformedPoints = new List<Vector2>(shapePoints);
    }

    private void ApplyInitialRotation()
    {
        if (InitialRotation != 0)
        {
            Transformasi.Matrix3x3Identity(transformMatrix);
            transformasi.RotationClockwise(transformMatrix, InitialRotation, Vector2.Zero);
            transformedPoints = transformasi.GetTransformPoint(transformMatrix, shapePoints);
        }
    }

    public override void _Draw()
    {
        if (transformedPoints == null || transformedPoints.Count == 0) return;

        // Draw outline (dashed style)
        GraphicsUtils.PutPixelAll(this, transformedPoints, GraphicsUtils.DrawStyle.DotDot, OutlineColor);

        // Draw center point indicator
        DrawCircle(Vector2.Zero, 3, OutlineColor);
    }

    public override void _ExitTree()
    {
        bentukDasar?.Dispose();
    }
}