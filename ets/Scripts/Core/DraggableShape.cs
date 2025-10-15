namespace Godot;

using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DraggableShape : Node2D
{
    public enum ShapeType
    {
        Persegi,
        TrapesiumSiku,
        SegitigaSamaKaki,
        SegitigaSiku,
        Hexagon,
        Lingkaran,
        JajarGenjang
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
    private Transformasi transformasi;
    private float[,] transformMatrix;
    private float currentRotation = 0f; // in degrees

    // BentukDasar for generating shapes
    private BentukDasar bentukDasar;

    // Bounds for collision detection
    private Rect2 bounds;
    
    // Snap properties
    public bool IsSnapped { get; private set; } = false;
    public Vector2 SnapPosition { get; private set; }
    private float snapThreshold = 30f; // Distance threshold for snapping
    

    public override void _Ready()
    {
        bentukDasar = new BentukDasar();
        transformasi = new Transformasi();
        transformMatrix = new float[3, 3];
        Transformasi.Matrix3x3Identity(transformMatrix);

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

            case ShapeType.JajarGenjang:
                OriginalPoints = bentukDasar.JajarGenjang(
                    new Vector2(-ShapeSize / 2, -ShapeSize / 4),
                    (int)ShapeSize,
                    (int)(ShapeSize * 0.6f),
                    (int)(ShapeSize * 0.3f)
                );
                break;
        }

        TransformedPoints = new List<Vector2>(OriginalPoints);

        // Debug: Print shape info
        GD.Print($"Generated {Type} with {OriginalPoints.Count} points");
    }

    private void CheckSnap()
    {
        float dynThreshold = Mathf.Clamp(ShapeSize * 0.35f, 12f, 40f);
        bool wasSnapped = IsSnapped;
        IsSnapped = false;
        SnappedToOutline = null;

        OutlineShape bestOutline = null;
        float bestDist = float.MaxValue;

        foreach (var outline in outlineShapes)
        {
            if (outline.Type != Type) continue;

            // Tolak bila size tak cocok (agar presisi)
            if (Mathf.Abs(ShapeSize - outline.ShapeSize) > SIZE_TOLERANCE_PX) continue;

            // Jangan pilih outline yang sudah terisi
            bool occupied = false;
            var shapesContainer = parent.GetNodeOrNull("ShapesContainer") as Node;
            if (shapesContainer != null)
            {
                foreach (var otherShape in shapesContainer.GetChildren().OfType<DraggableShape>())
                {
                    if (otherShape != this && otherShape.IsSnapped && otherShape.SnappedToOutline == outline)
                    {
                        occupied = true;
                        break;
                    }
                }
            }
            if (occupied) continue;

            float d = GlobalPosition.DistanceTo(outline.GlobalPosition);
            if (d < bestDist)
            {
                bestDist = d;
                bestOutline = outline;
            }
        }

        if (bestOutline != null && bestDist <= dynThreshold)
        {
            GlobalPosition = bestOutline.GlobalPosition;

            // Rapihkan rotasi ke orientasi outline (mulai dari kelipatan 45° terdekat)
            float snapped = Mathf.Round(currentRotation / 45f) * 45f;
            float target = bestOutline.InitialRotation;
            float delta = Mathf.PosMod(target - snapped, 360f);
            if (delta > 180f) delta -= 360f;
            currentRotation = snapped + delta;

            // Jika masih berbeda lebih dari toleransi, paksa ke target
            if (AngleDeltaDeg(currentRotation, target) > ROT_SNAP_TOLERANCE_DEG)
                currentRotation = target;

            Transformasi.Matrix3x3Identity(transformMatrix);
            transformasi.RotationClockwise(transformMatrix, currentRotation, Vector2.Zero);
            TransformedPoints = transformasi.GetTransformPoint(transformMatrix, OriginalPoints);

            SnapPosition = bestOutline.GlobalPosition;
            IsSnapped = true;
            SnappedToOutline = bestOutline;
            CanBeDeleted = true;
            CalculateBounds();

            if (!wasSnapped) EmitSignal(SignalName.ShapeSnapped, this);
        }
        else
        {
            CanBeDeleted = false;
        }

        QueueRedraw();
    }

    private const float ROT_SNAP_TOLERANCE_DEG = 8f;     // toleransi cocok rotasi saat snap & win check
    private const float SIZE_TOLERANCE_PX = 2f;          // toleransi perbedaan size (px)

    private static float AngleDeltaDeg(float a, float b)
    {
        // selisih sudut terkecil 0..180
        float d = Mathf.Abs(Mathf.PosMod(a - b, 360f));
        return Mathf.Min(d, 360f - d);
    }

    private void ValidateStaySnappedAfterRotation()
    {
        if (!IsSnapped || SnappedToOutline == null) return;

        float rotDiff = AngleDeltaDeg(currentRotation, SnappedToOutline.InitialRotation);
        if (rotDiff > ROT_SNAP_TOLERANCE_DEG)
        {
            // keluar toleransi -> unsnap
            IsSnapped = false;
            CanBeDeleted = false;
            SnappedToOutline = null;
        }
        else
        {
            // dalam toleransi -> rapihkan ke rotasi & posisi outline
            currentRotation = SnappedToOutline.InitialRotation;
            GlobalPosition = SnappedToOutline.GlobalPosition;

            Transformasi.Matrix3x3Identity(transformMatrix);
            transformasi.RotationClockwise(transformMatrix, currentRotation, Vector2.Zero);
            TransformedPoints = transformasi.GetTransformPoint(transformMatrix, OriginalPoints);
            CalculateBounds();
        }
        QueueRedraw();
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
            CheckSnap();
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
                RotateShape(); // Rotate 45 degrees
                GetViewport().SetInputAsHandled();
            }
        }
    }

    public bool IsPointInShape(Vector2 point)
    {
        // Simple bounds check
        return bounds.HasPoint(point);
    }

    private bool IsMouseOver()
    {
        Vector2 localMousePos = GetGlobalMousePosition() - GlobalPosition;
        return IsPointInShape(localMousePos);
    }

    public void RotateShape()
    {
        currentRotation += 45f;
        if (currentRotation >= 360f) currentRotation -= 360f;

        Transformasi.Matrix3x3Identity(transformMatrix);
        transformasi.RotationClockwise(transformMatrix, currentRotation, Vector2.Zero);
        TransformedPoints = transformasi.GetTransformPoint(transformMatrix, OriginalPoints);

        CalculateBounds();
        QueueRedraw();
        GD.Print($"Rotated {Type} to {currentRotation}°");

        // Jika sedang snapped, pastikan tetap valid atau unsnap
        ValidateStaySnappedAfterRotation();
    }


    public void ResetTransformation()
    {
        Transformasi.Matrix3x3Identity(transformMatrix);
        currentRotation = 0f;
        TransformedPoints = new List<Vector2>(OriginalPoints);
        CalculateBounds();
        QueueRedraw();
    }
    
    public bool IsCorrectlyPlaced()
    {
        return IsSnapped;
    }

    public override void _Draw()
    {
        if (TransformedPoints == null || TransformedPoints.Count == 0) 
        {
            GD.PrintErr($"No points to draw for {Type}");
            return;
        }

        // Draw filled polygon with different color if snapped
        Color drawColor = IsSnapped ? ShapeColor.Lightened(0.2f) : ShapeColor;
        
        // Use simple polygon drawing
        if (TransformedPoints.Count >= 3)
        {
            DrawPolygon(TransformedPoints.ToArray(), new Color[] { drawColor });
        }

        // Draw outline with different style if snapped
        Color outlineColor = IsSnapped ? Colors.Green : Colors.Black;
        GraphicsUtils.PutPixelAll(this, TransformedPoints, GraphicsUtils.DrawStyle.DotDot, outlineColor);

        // Draw snap indicator
        if (IsSnapped)
        {
            DrawCircle(Vector2.Zero, 5, Colors.Green);
        }
        
    }

    public override void _ExitTree()
    {
        bentukDasar?.Dispose();
    }
}