namespace Godot;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Draggable Shape - Pattern Block yang bisa di-drag, rotate, dan snap
/// UPDATED: Multiple rotation controls
/// - Q = Rotate CCW (Counter-Clockwise) ⟲
/// - R = Rotate CW (Clockwise) ⟳
/// - Arrow Left = Rotate CCW ⟲
/// - Arrow Right = Rotate CW ⟳
/// </summary>
public partial class DraggableShape : Node2D
{
    [Signal] public delegate void ShapeSnappedEventHandler(DraggableShape shape);
    [Signal] public delegate void ShapeDeletedEventHandler(DraggableShape shape);

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
    public float DimAlas { get; set; } = 0f;
    public float DimTinggi { get; set; } = 0f;
    public float DimLebar { get; set; } = 0f;
    public float DimSkew { get; set; } = 0f;
    public List<Vector2> OriginalPoints { get; private set; }
    public List<Vector2> TransformedPoints { get; private set; }

    private const float ROT_SNAP_TOLERANCE_DEG = 15f;
    private const float SIZE_TOLERANCE_PX = 3f;
    private const float ROTATION_STEP = 45f; // Rotasi per step

    // Drag properties
    private bool isDragging = false;
    private Vector2 dragOffset;

    // Transformation
    private Transformasi transformasi;
    private float[,] transformMatrix;
    private float currentRotation = 0f;
    public float CurrentRotationDeg => currentRotation;

    // BentukDasar for generating shapes
    private BentukDasar bentukDasar;

    // Bounds for collision detection
    private Rect2 bounds;

    // Snap properties
    public bool IsSnapped { get; private set; } = false;
    public Vector2 SnapPosition { get; private set; }
    private float snapThreshold = 50f;
    public OutlineShape SnappedToOutline { get; private set; } = null;

    // Palette/template properties
    public bool IsPaletteTemplate { get; set; } = false;
    public Vector2 PaletteSpawnPosition { get; set; }

    // Undo/Delete functionality
    public bool CanBeDeleted { get; private set; } = false;
    private Vector2 originalPosition;
    private float originalRotation;

    public override void _Ready()
    {
        bentukDasar = new BentukDasar();
        transformasi = new Transformasi();
        transformMatrix = new float[3, 3];
        Transformasi.Matrix3x3Identity(transformMatrix);

        GenerateShape();
        CalculateBounds();

        originalPosition = Position;
        originalRotation = currentRotation;

        GD.Print($"✅ DraggableShape Ready: {Type}, Size: {ShapeSize}, IsPalette: {IsPaletteTemplate}");
    }

   private void GenerateShape()
    {
        OriginalPoints = new List<Vector2>();

        switch (Type)
        {
            case DraggableShape.ShapeType.Persegi:
                float lebarPersegi = DimLebar > 0 ? DimLebar : ShapeSize;
                OriginalPoints = bentukDasar.Persegi(-lebarPersegi / 2, -lebarPersegi / 2, lebarPersegi);
                break;

            case DraggableShape.ShapeType.TrapesiumSiku:
                float alasTrapesium = DimAlas > 0 ? DimAlas : ShapeSize;
                float tinggiTrapesium = DimTinggi > 0 ? DimTinggi : (ShapeSize * 0.6f);
                float sisiAtasTrapesium = DimLebar > 0 ? DimLebar : (ShapeSize * 0.6f);

                OriginalPoints = bentukDasar.TrapesiumSiku(
                    new Vector2(-alasTrapesium / 2, -tinggiTrapesium / 3),
                    (int)tinggiTrapesium,
                    (int)alasTrapesium,
                    (int)sisiAtasTrapesium
                );
                break;

            case DraggableShape.ShapeType.SegitigaSamaKaki:
                float alasSegitiga = DimAlas > 0 ? DimAlas : ShapeSize;
                float tinggiSegitiga = DimTinggi > 0 ? DimTinggi : (ShapeSize * 0.8f);

                // Definisikan titik-titik
                var pBawahKiri = new Vector2(-alasSegitiga / 2, tinggiSegitiga / 3);
                var pBawahKanan = new Vector2(alasSegitiga / 2, tinggiSegitiga / 3);
                var pPuncak = new Vector2(0, -tinggiSegitiga * 2 / 3);

                OriginalPoints = new List<Vector2>
                {
                    pBawahKiri,
                    pBawahKanan,
                    pPuncak,
                    pBawahKiri // <-- TAMBAHKAN BARIS INI (kembali ke titik awal)
                };
                break;

            case DraggableShape.ShapeType.SegitigaSiku:
                float alasSiku = DimAlas > 0 ? DimAlas : ShapeSize;
                float tinggiSiku = DimTinggi > 0 ? DimTinggi : ShapeSize;

                OriginalPoints = bentukDasar.SegitigaSiku(
                    new Vector2(-alasSiku / 2, -tinggiSiku / 2),
                    (int)alasSiku,
                    (int)tinggiSiku
                );
                break;

            case DraggableShape.ShapeType.Hexagon:
                float sizeHex = DimLebar > 0 ? DimLebar : ShapeSize;
                OriginalPoints = bentukDasar.Hexagon(Vector2.Zero, sizeHex / 2, 0);
                break;

            case DraggableShape.ShapeType.Lingkaran:
                float diameter = DimLebar > 0 ? DimLebar : ShapeSize;
                OriginalPoints = bentukDasar.Lingkaran(Vector2.Zero, (int)(diameter / 2));
                break;

            case DraggableShape.ShapeType.JajarGenjang:
                float alasJajar = DimAlas > 0 ? DimAlas : ShapeSize;
                float tinggiJajar = DimTinggi > 0 ? DimTinggi : (ShapeSize * 0.6f);
                float skewJajar = DimSkew > 0 ? DimSkew : (ShapeSize * 0.3f);

                OriginalPoints = bentukDasar.JajarGenjang(
                    new Vector2(-alasJajar / 2, -tinggiJajar / 4),
                    (int)alasJajar,
                    (int)tinggiJajar,
                    (int)skewJajar
                );
                break;
        }

        TransformedPoints = new List<Vector2>(OriginalPoints);
    }

    private static float AngleDeltaDeg(float a, float b)
    {
        float d = Mathf.Abs(Mathf.PosMod(a - b, 360f));
        return Mathf.Min(d, 360f - d);
    }

    private void CheckSnap()
    {
        float dynThreshold = Mathf.Clamp(ShapeSize * 0.8f, 35f, 80f);
        bool wasSnapped = IsSnapped;

        // Reset snap state
        IsSnapped = false;
        OutlineShape previousSnap = SnappedToOutline;
        SnappedToOutline = null;
        CanBeDeleted = false;

        // Get scene root
        var sceneRoot = GetTree()?.CurrentScene;
        if (sceneRoot == null) return;

        // Find OutlineContainer
        var outlineContainer = sceneRoot.GetNodeOrNull<Node2D>("Outline");
        if (outlineContainer == null) return;

        var outlineShapes = outlineContainer.GetChildren().OfType<OutlineShape>().ToList();
        if (outlineShapes.Count == 0) return;

        OutlineShape bestOutline = null;
        float bestScore = float.MaxValue;

        foreach (var outline in outlineShapes)
        {
            // 1. TYPE CHECK
            if (outline.Type != Type) continue;

            // 2. SIZE CHECK
            float sizeDiff = Mathf.Abs(ShapeSize - outline.ShapeSize);
            if (sizeDiff > SIZE_TOLERANCE_PX) continue;

            // 3. CHECK IF OCCUPIED
            bool occupied = false;
            var shapesContainer = GetParent();
            if (shapesContainer != null)
            {
                foreach (var otherShape in shapesContainer.GetChildren().OfType<DraggableShape>())
                {
                    if (otherShape != this &&
                        otherShape.IsSnapped &&
                        otherShape.SnappedToOutline == outline)
                    {
                        occupied = true;
                        break;
                    }
                }
            }
            if (occupied) continue;

            // 4. DISTANCE CHECK
            float posDist = GlobalPosition.DistanceTo(outline.GlobalPosition);
            if (posDist > dynThreshold) continue;

            // 5. ROTATION CHECK
            float rotDiff = AngleDeltaDeg(currentRotation, outline.InitialRotation);

            // 6. CALCULATE SCORE
            float score = posDist + (rotDiff * 2f);

            if (score < bestScore)
            {
                bestScore = score;
                bestOutline = outline;
            }
        }

        // SNAP IF FOUND
        if (bestOutline != null)
        {
            float finalRotDiff = AngleDeltaDeg(currentRotation, bestOutline.InitialRotation);

            if (finalRotDiff <= ROT_SNAP_TOLERANCE_DEG)
            {
                // PERFORM SNAP
                GlobalPosition = bestOutline.GlobalPosition;
                currentRotation = bestOutline.InitialRotation;

                // Update transformation
                Transformasi.Matrix3x3Identity(transformMatrix);
                transformasi.RotationClockwise(transformMatrix, currentRotation, Vector2.Zero);
                TransformedPoints = transformasi.GetTransformPoint(transformMatrix, OriginalPoints);

                SnapPosition = bestOutline.GlobalPosition;
                IsSnapped = true;
                SnappedToOutline = bestOutline;
                CanBeDeleted = true;
                CalculateBounds();

                GD.Print($"✅ SNAPPED! {Type} → Outline at {bestOutline.GlobalPosition}");

                if (!wasSnapped || previousSnap != bestOutline)
                {
                    EmitSignal(SignalName.ShapeSnapped, this);
                }
            }
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
                        // Clone from palette
                        if (IsPaletteTemplate)
                        {
                            CreateReplacementTemplate();
                            IsPaletteTemplate = false;
                        }

                        isDragging = true;
                        dragOffset = GetGlobalMousePosition() - GlobalPosition;

                        // Notify level that this shape is selected
                        NotifyLevelShapeSelected();

                        // Unsnap if was snapped
                        if (IsSnapped)
                        {
                            IsSnapped = false;
                            SnappedToOutline = null;
                            CanBeDeleted = false;
                        }

                        GetViewport().SetInputAsHandled();
                    }
                }
                else
                {
                    if (isDragging)
                    {
                        isDragging = false;
                        CheckSnap();
                        GetViewport().SetInputAsHandled();
                    }
                }
            }
        }

        // ROTATION CONTROLS - Hanya jika mouse hover
        if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
        {
            if (IsMouseOver())
            {
                bool handled = false;

                // Q = Counter-Clockwise (CCW) ⟲
                if (keyEvent.Keycode == Key.Q)
                {
                    RotateShape(false); // CCW
                    handled = true;
                    GD.Print("⟲ Q - Rotate CCW");
                }
                // R = Clockwise (CW) ⟳
                else if (keyEvent.Keycode == Key.R)
                {
                    RotateShape(true); // CW
                    handled = true;
                    GD.Print("⟳ R - Rotate CW");
                }
                // Arrow Left = CCW ⟲
                else if (keyEvent.Keycode == Key.Left)
                {
                    RotateShape(false); // CCW
                    handled = true;
                    GD.Print("⟲ ← - Rotate CCW");
                }
                // Arrow Right = CW ⟳
                else if (keyEvent.Keycode == Key.Right)
                {
                    RotateShape(true); // CW
                    handled = true;
                    GD.Print("⟳ → - Rotate CW");
                }

                if (handled)
                {
                    GetViewport().SetInputAsHandled();
                }
            }
        }
    }

    private void NotifyLevelShapeSelected()
    {
        // Notify BaseChallengeLevel that this shape is selected
        Node parent = GetParent();
        while (parent != null)
        {
            if (parent is BaseChallengeLevel level)
            {
                level.NotifyShapeSelected(this);
                break;
            }
            parent = parent.GetParent();
        }
    }

    private void CreateReplacementTemplate()
    {
        var parent = GetParent();
        if (parent == null) return;

        var clone = new DraggableShape();
        clone.Type = Type;
        clone.ShapeColor = ShapeColor;
        clone.ShapeSize = ShapeSize;
        clone.Position = PaletteSpawnPosition;
        clone.IsPaletteTemplate = true;
        clone.PaletteSpawnPosition = PaletteSpawnPosition;

        clone.DimAlas = this.DimAlas;
        clone.DimTinggi = this.DimTinggi;
        clone.DimLebar = this.DimLebar;
        clone.DimSkew = this.DimSkew;

        parent.AddChild(clone);

        // Connect signal for the clone
        var sceneRoot = GetTree()?.CurrentScene;
        if (sceneRoot != null && sceneRoot is BaseChallengeLevel level)
        {
            clone.ShapeSnapped += level.OnShapeSnapped;
        }
    }

    public bool IsPointInShape(Vector2 point)
    {
        return bounds.HasPoint(point);
    }

    private bool IsMouseOver()
    {
        Vector2 localMousePos = GetGlobalMousePosition() - GlobalPosition;
        return IsPointInShape(localMousePos);
    }

    /// <summary>
    /// Rotate shape clockwise or counter-clockwise
    /// </summary>
    /// <param name="clockwise">True for CW ⟳, False for CCW ⟲</param>
    public void RotateShape(bool clockwise = true)
    {
        if (clockwise)
        {
            currentRotation += ROTATION_STEP;
            if (currentRotation >= 360f) currentRotation -= 360f;
        }
        else
        {
            currentRotation -= ROTATION_STEP;
            if (currentRotation < 0f) currentRotation += 360f;
        }

        Transformasi.Matrix3x3Identity(transformMatrix);
        transformasi.RotationClockwise(transformMatrix, currentRotation, Vector2.Zero);
        TransformedPoints = transformasi.GetTransformPoint(transformMatrix, OriginalPoints);

        CalculateBounds();

        // Check snap after rotation
        if (!isDragging)
        {
            CheckSnap();
        }

        QueueRedraw();
        GD.Print($"🔄 Rotated {Type} to {currentRotation}° ({(clockwise ? "CW ⟳" : "CCW ⟲")})");
    }

    public void ResetTransformation()
    {
        Transformasi.Matrix3x3Identity(transformMatrix);
        currentRotation = 0f;
        TransformedPoints = new List<Vector2>(OriginalPoints);
        IsSnapped = false;
        SnappedToOutline = null;
        CanBeDeleted = false;
        CalculateBounds();
        QueueRedraw();
    }

    public bool IsCorrectlyPlaced()
    {
        return IsSnapped && SnappedToOutline != null;
    }

    public void UndoToOriginalPosition()
    {
        if (IsPaletteTemplate) return;

        Position = originalPosition;
        currentRotation = originalRotation;
        IsSnapped = false;
        SnappedToOutline = null;
        CanBeDeleted = false;

        Transformasi.Matrix3x3Identity(transformMatrix);
        transformasi.RotationClockwise(transformMatrix, currentRotation, Vector2.Zero);
        TransformedPoints = transformasi.GetTransformPoint(transformMatrix, OriginalPoints);
        CalculateBounds();
        QueueRedraw();

        GD.Print($"[UNDO] {Type} -> original position");
    }

    public void DeleteShape()
    {
        if (IsPaletteTemplate) return;

        EmitSignal(SignalName.ShapeDeleted, this);
        QueueFree();
    }

    public override void _Draw()
    {
        if (TransformedPoints == null || TransformedPoints.Count == 0) return;

        // FILL COLOR
        Color drawColor;
        if (IsSnapped)
        {
            drawColor = ShapeColor.Lightened(0.3f);
        }
        else if (IsPaletteTemplate)
        {
            drawColor = ShapeColor;
            drawColor.A = 0.9f;
        }
        else
        {
            drawColor = ShapeColor;
            drawColor.A = 1f;
        }

        // Draw filled polygon
        if (TransformedPoints.Count >= 3)
        {
            DrawPolygon(TransformedPoints.ToArray(), new Color[] { drawColor });
        }

        // OUTLINE
        Color outlineColor;
        if (IsSnapped)
        {
            outlineColor = Colors.LimeGreen;
            DrawCircle(Vector2.Zero, 8, Colors.LimeGreen);
            DrawCircle(Vector2.Zero, 5, Colors.White);
        }
        else if (isDragging)
        {
            outlineColor = Colors.Yellow;
        }
        else
        {
            outlineColor = Colors.Black;
        }

        GraphicsUtils.PutPixelAll(this, TransformedPoints, GraphicsUtils.DrawStyle.DotDot, outlineColor);
    }

    public override void _ExitTree()
    {
        bentukDasar?.Dispose();
    }
}