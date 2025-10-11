namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

public partial class Karya1 : Node2D
{
    [Export] private Label _label;
    [Export] private CheckButton _frameToggleCheckBtn;
    [Export] private CheckButton _cartesianToggleCheckBtn;
    [Export] private HSlider _translateXSlider;
    [Export] private HSlider _translateYSlider;
    [Export] private HSlider _rotateSlider;
    [Export] private HSlider _scaleSlider;

    private BentukDasar bentukDasar = new BentukDasar();
    private Transformasi transformasi = new Transformasi();
    private Primitif primitif = new Primitif();

    // Shape data
    private int currentShapeIndex = 0;
    private List<Vector2> originalShape = new List<Vector2>();
    private List<Vector2> transformedShape = new List<Vector2>();
    private Vector2 shapeCenter = new Vector2(0, 0);
    private float[,] transformMatrix = new float[3, 3];

    // Transform values
    private float translateX = 0;
    private float translateY = 0;
    private float rotateAngle = 0;
    private float scaleValue = 1.0f;

    // Flags
    private bool showMargin = false;
    private bool showCartesian = false;

    // Shape definitions
    private string[] shapeNames = {
        "Kotak",
        "Segitiga Siku",
        "Persegi Panjang",
        "Trapesium Siku",
        "Trapesium Sama Kaki",
        "Jajar Genjang",
        "Segitiga Sama Kaki",
        "Lingkaran",
        "Elips",
        "Segienam Beraturan",
        "Garis"
    };

    public override void _Ready()
    {
        ScreenUtils.Initialize(GetViewport());

        // Initialize transform matrix
        Transformasi.Matrix3x3Identity(transformMatrix);
        if (_label == null)
            _label = GetNode<Label>("Control/VBoxContainer/HBoxContainer/Label");
        // Set initial slider value for scale
        if (_scaleSlider != null)
            _scaleSlider.Value = 1.0;

        // Load initial shape
        LoadShape(currentShapeIndex);
        UpdateDisplay();
    }

    // pusat bentuk 
    private Vector2 GetShapeCenter(List<Vector2> shape)
    {
        if (shape == null || shape.Count == 0) return Vector2.Zero;
        Vector2 sum = Vector2.Zero;
        foreach (var v in shape) sum += v;
        return sum / shape.Count;
    }
    private void LoadShape(int index)
    {
        originalShape.Clear();
        shapeCenter = new Vector2(0, 0);
        //posisi tengah viewport
        var viewportCenter = new Vector2(GetViewport().GetVisibleRect().Size.X / 2, GetViewport().GetVisibleRect().Size.Y / 2);

        switch (index)
        {
            case 0: // Kotak
                originalShape = bentukDasar.Polygon(bentukDasar.Persegi(50, 300, 100));
                break;
            case 1: // Segitiga Siku
                originalShape = bentukDasar.SegitigaSiku(new Vector2(-50, -50), 80, 80);
                break;
            case 2: // Persegi Panjang
                originalShape = bentukDasar.PersegiPanjang(-60, -40, 120, 80);
                break;
            case 3: // Trapesium Siku
                originalShape = bentukDasar.TrapesiumSiku(new Vector2(-50, -40), 60, 100, 80);
                break;
            case 4: // Trapesium Sama Kaki
                originalShape = bentukDasar.TrapesiumSamaKaki(new Vector2(-50, -40), 60, 100, 80);
                break;
            case 5: // Jajar Genjang
                originalShape = bentukDasar.JajarGenjang(new Vector2(-60, -40), 80, 80, 20);
                break;
            case 6: // Segitiga Sama Kaki
                originalShape = bentukDasar.SegitigaSamaKaki(new Vector2(-50, -50), 100, 80);
                break;
            case 7: // Lingkaran
                originalShape = bentukDasar.Lingkaran(new Vector2(0, 0), 50);
                break;
            case 8: // Elips
                originalShape = bentukDasar.Elips(new Vector2(0, 0), 60, 40);
                break;
            case 9: // Segienam Beraturan
                originalShape = bentukDasar.SegienamBeraturan(new Vector2(0, 0), 60);
                break;
            case 10: // Garis
                originalShape = primitif.LineBresenham(-80, -80, 80, 80);
                break;
        }

        // Offset semua titik ke tengah viewport
        var center = GetShapeCenter(originalShape);
        for (int i = 0; i < originalShape.Count; i++)
            originalShape[i] += (viewportCenter - center);

        // Reset transformations
        ResetTransform();
        ApplyTransformations();
    }

    private void ResetTransform()
    {
        translateX = 0;
        translateY = 0;
        rotateAngle = 0;
        scaleValue = 1.0f;
        shapeCenter = new Vector2(0, 0);

        if (_translateXSlider != null) _translateXSlider.Value = 0;
        if (_translateYSlider != null) _translateYSlider.Value = 0;
        if (_rotateSlider != null) _rotateSlider.Value = 0;
        if (_scaleSlider != null) _scaleSlider.Value = 1.0;

        Transformasi.Matrix3x3Identity(transformMatrix);
    }

    private void ApplyTransformations()
    {
        // Reset matrix
        Transformasi.Matrix3x3Identity(transformMatrix);

        // Pivot di pusat bentuk (agar rotasi & scaling di tempat)
        var pivot = GetShapeCenter(originalShape);

        // Scaling di pivot
        if (scaleValue != 1.0f)
        {
            transformasi.Scaling(transformMatrix, scaleValue, scaleValue, pivot);
        }

        // Rotasi di pivot
        if (rotateAngle != 0)
        {
            transformasi.RotationClockwise(transformMatrix, rotateAngle, pivot);
        }

        // Translasi (geser dari tengah)
        if (translateX != 0 || translateY != 0)
        {
            transformasi.Translation(transformMatrix, translateX, translateY, ref pivot);
        }

        // Apply transformation to shape
        transformedShape = transformasi.GetTransformPoint(transformMatrix, originalShape);

        QueueRedraw();
    }

    private void UpdateDisplay()
    {
        if (_label != null)
        {
            _label.Text = shapeNames[currentShapeIndex];
        }
    }

    // Signal handlers
    private void _on_prev_button_pressed()
    {
        currentShapeIndex--;
        if (currentShapeIndex < 0)
            currentShapeIndex = shapeNames.Length - 1;

        LoadShape(currentShapeIndex);
        UpdateDisplay();
    }

    private void _on_next_button_pressed()
    {
        currentShapeIndex++;
        if (currentShapeIndex >= shapeNames.Length)
            currentShapeIndex = 0;

        LoadShape(currentShapeIndex);
        UpdateDisplay();
    }

    private void _on_margin_button_toggled(bool pressed)
    {
        showMargin = pressed;
        QueueRedraw();
    }

    private void _on_cartesian_button_toggled(bool pressed)
    {
        showCartesian = pressed;
        QueueRedraw();
    }

    private void _on_translate_x_changed(double value)
    {
        translateX = (float)value;
        ApplyTransformations();
    }

    private void _on_translate_y_changed(double value)
    {
        translateY = (float)value;
        ApplyTransformations();
    }

    private void _on_rotate_changed(double value)
    {
        rotateAngle = (float)value;
        ApplyTransformations();
    }

    private void _on_scale_changed(double value)
    {
        scaleValue = (float)value;
        ApplyTransformations();
    }

    public override void _Draw()
    {
        // Draw margin frame
        if (showMargin)
        {
            var margin = bentukDasar.Margin();
            GraphicsUtils.PutPixelAll(this, margin, GraphicsUtils.DrawStyle.DotDot, Colors.Gray);
        }

        // Draw Cartesian grid and axes
        if (showCartesian)
        {
            // Draw grid lines
            var grid = bentukDasar.GridLines(50, 300);
            GraphicsUtils.PutPixelAll(this, grid, GraphicsUtils.DrawStyle.DotDot, new Color(0.3f, 0.3f, 0.3f, 0.5f));

            // Draw axes
            var axisX = bentukDasar.SumbuX(4000);
            var axisY = bentukDasar.SumbuY(3000);
            GraphicsUtils.PutPixelAll(this, axisX, GraphicsUtils.DrawStyle.DotDot, Colors.White);
            GraphicsUtils.PutPixelAll(this, axisY, GraphicsUtils.DrawStyle.DotDot, Colors.White);
        }

        // Draw original shape (semi-transparent white)
        if (originalShape.Count > 0)
        {
            GraphicsUtils.PutPixelAll(this, originalShape, GraphicsUtils.DrawStyle.DotDot,
                new Color(1f, 1f, 1f, 0.3f));
        }

        // Draw transformed shape (cyan)
        if (transformedShape.Count > 0)
        {
            GraphicsUtils.PutPixelAll(this, transformedShape, GraphicsUtils.DrawStyle.DotDot,
                Colors.Cyan);
        }
    }

    public override void _ExitTree()
    {
        bentukDasar?.Dispose();
    }
}