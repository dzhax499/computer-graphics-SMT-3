using System;
using System.Collections.Generic;

namespace Godot;

public partial class Karya1 : Node2D
{
    private Primitif _primitif = new Primitif();
    private BentukDasar _bentukDasar = new BentukDasar();

    public override void _Ready()
    {
        ScreenUtils.Initialize(GetViewport());
        QueueRedraw();
    }

    public override void _Draw()
    {
        // draw axes and margin
        MarginPixel();
        DrawCoordinate();

        // draw shapes and their transformed variants
        DrawAllShapesWithTransforms();
    }

    private void DrawAllShapesWithTransforms()
    {
        // 1) Square (Persegi)
        var square = _bentukDasar.Persegi(-150, 100, 80); // x, y, size (kartesian)
        DrawShapeWithTransforms(square, ColorUtils.ColorStorage(3), new TransformSpec
        {
            Translate = new Vector2(220, -80),
            RotateDeg = 30f,
            Scale = 1.3f
        }, "Persegi (size=80) center approx (-110,140)");

        // 2) Rectangle (Persegi Panjang)
        var rect = _bentukDasar.PersegiPanjang(50, 120, 140, 60); // x,y, panjang, lebar
        DrawShapeWithTransforms(rect, ColorUtils.ColorStorage(4), new TransformSpec
        {
            Translate = new Vector2(-200, -150),
            RotateDeg = -20f,
            Scale = 0.8f
        }, "Persegi Panjang (140x60) center approx (120,150)");

        // 3) Right Triangle (Segitiga Siku)
        var tri = _bentukDasar.SegitigaSiku(new Vector2(-200, -80), 100, 80);
        DrawShapeWithTransforms(tri, ColorUtils.ColorStorage(5), new TransformSpec
        {
            Translate = new Vector2(180, 40),
            RotateDeg = 45f,
            Scale = 1.0f
        }, "Segitiga Siku (alas=100, tinggi=80) start (-200,-80)");

        // 4) Trapezoid Right (Trapesium Siku)
        var trap = _bentukDasar.TrapesiumSiku(new Vector2(80, -60), 60, 120, 50);
        DrawShapeWithTransforms(trap, ColorUtils.ColorStorage(6), new TransformSpec
        {
            Translate = new Vector2(-120, 180),
            RotateDeg = 0f,
            Scale = 1.2f
        }, "Trapesium Siku (atas=60 bawah=120 tinggi=50) start (80,-60)");

        // 5) Circle (Lingkaran)
        var circle = _bentukDasar.Lingkaran(new Vector2(0, -180), 40);
        DrawShapeWithTransforms(circle, ColorUtils.ColorStorage(2), new TransformSpec
        {
            Translate = new Vector2(0, 220),
            RotateDeg = 0f,
            Scale = 1.5f
        }, "Lingkaran (radius=40) center (0,-180)");

        // 6) Ellipse (Elips)
        var ellipse = _bentukDasar.Elips(new Vector2(200, -180), 70, 40);
        DrawShapeWithTransforms(ellipse, ColorUtils.ColorStorage(1), new TransformSpec
        {
            Translate = new Vector2(-260, 200),
            RotateDeg = 15f,
            Scale = 0.9f
        }, "Elips (rx=70, ry=40) center (200,-180)");
    }

    // Draw original shape, draw transformed (translate+rotate+scale) variant, and draw transform lines
    private void DrawShapeWithTransforms(List<Vector2> original, Color color, TransformSpec spec, string debugNote)
    {
        // original: already in screen coordinates from BentukDasar
        GraphicsUtils.PutPixelAll(this, original, GraphicsUtils.DrawStyle.DotDot, color);

        // compute centroids
        var centroidOriginal = ComputeCentroid(original);

        // apply scale around centroid, then rotate around centroid, then translate
        var scaled = Transformasi.Scale(original, spec.Scale, centroidOriginal);
        var rotated = Transformasi.Rotate(scaled, spec.RotateDeg, centroidOriginal);
        var translated = Transformasi.Translate(rotated, spec.Translate);

        // draw transformed shape (dashed/strip look)
        GraphicsUtils.PutPixelAll(this, translated, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(7));

        // draw a line (arrow-like) from original centroid to transformed centroid
        var centroidTransformed = ComputeCentroid(translated);
        var connector = _primitif.LineBresenham((int)centroidOriginal.X, (int)centroidOriginal.Y, (int)centroidTransformed.X, (int)centroidTransformed.Y);
        GraphicsUtils.PutPixelAll(this, connector, GraphicsUtils.DrawStyle.DotDash, ColorUtils.ColorStorage(0));

        // draw small dots at centroids
        GraphicsUtils.PutPixel(this, centroidOriginal.X, centroidOriginal.Y, ColorUtils.ColorStorage(3));
        GraphicsUtils.PutPixel(this, centroidTransformed.X, centroidTransformed.Y, ColorUtils.ColorStorage(4));

        // optional: draw bounding box for original and transformed
        var bboxOrig = BoundingBox(original);
        var bboxTrans = BoundingBox(translated);
        var bboxOrigLines = RectangleToLines(bboxOrig);
        var bboxTransLines = RectangleToLines(bboxTrans);
        GraphicsUtils.PutPixelAll(this, bboxOrigLines, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(3));
        GraphicsUtils.PutPixelAll(this, bboxTransLines, GraphicsUtils.DrawStyle.EllipseDotStrip, ColorUtils.ColorStorage(4));

        // debug label printed to console (for developer)
        GD.Print($"{debugNote} centroidOrig={centroidOriginal} centroidTrans={centroidTransformed} scale={spec.Scale} rot={spec.RotateDeg} translate={spec.Translate}");
    }

    private Vector2 ComputeCentroid(List<Vector2> pts)
    {
        if (pts == null || pts.Count == 0) return Vector2.Zero;
        float sx = 0, sy = 0;
        foreach (var p in pts)
        {
            sx += p.X; sy += p.Y;
        }
        return new Vector2(sx / pts.Count, sy / pts.Count);
    }

    private (Vector2 min, Vector2 max) BoundingBox(List<Vector2> pts)
    {
        if (pts == null || pts.Count == 0) return (Vector2.Zero, Vector2.Zero);
        float xmin = float.MaxValue, ymin = float.MaxValue, xmax = float.MinValue, ymax = float.MinValue;
        foreach (var p in pts)
        {
            if (p.X < xmin) xmin = p.X;
            if (p.Y < ymin) ymin = p.Y;
            if (p.X > xmax) xmax = p.X;
            if (p.Y > ymax) ymax = p.Y;
        }
        return (new Vector2(xmin, ymin), new Vector2(xmax, ymax));
    }

    private List<Vector2> RectangleToLines((Vector2 min, Vector2 max) bbox)
    {
        var min = bbox.min; var max = bbox.max;
        var corners = new List<Vector2>(){ min, new Vector2(max.X, min.Y), max, new Vector2(min.X, max.Y) };
        var lines = new List<Vector2>();
        for (int i = 0; i < corners.Count; i++)
        {
            int ni = (i + 1) % corners.Count;
            lines.AddRange(_primitif.LineBresenham((int)corners[i].X, (int)corners[i].Y, (int)corners[ni].X, (int)corners[ni].Y));
        }
        return lines;
    }

    // Helper to draw single pixel with ColorUtils
    private void PutPixel(Node2D target, float x, float y, Color color)
    {
        GraphicsUtils.PutPixel(target, x, y, color);
    }

    private void DrawCoordinate()
    {
        var sumbuX = _bentukDasar.SumbuX(1000);
        GraphicsUtils.PutPixelAll(this, sumbuX, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(1));
        var sumbuY = _bentukDasar.SumbuY(1000);
        GraphicsUtils.PutPixelAll(this, sumbuY, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(2));
    }

    private void MarginPixel()
    {
        var margin = _bentukDasar.Margin();
        GraphicsUtils.PutPixelAll(this, margin, color: ColorUtils.ColorStorage(0));
    }

    public override void _ExitTree()
    {
        NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
        base._ExitTree();
    }
}

// Simple Transformasi utilities (operates on screen coordinates)
public static class Transformasi
{
    public static List<Vector2> Translate(List<Vector2> pts, Vector2 delta)
    {
        var res = new List<Vector2>(pts.Count);
        foreach (var p in pts) res.Add(new Vector2(p.X + delta.X, p.Y + delta.Y));
        return res;
    }

    public static List<Vector2> Scale(List<Vector2> pts, float scale, Vector2 center)
    {
        var res = new List<Vector2>(pts.Count);
        foreach (var p in pts)
        {
            var v = p - center;
            v *= scale;
            res.Add(center + v);
        }
        return res;
    }

    public static List<Vector2> Rotate(List<Vector2> pts, float angleDeg, Vector2 center)
    {
        var res = new List<Vector2>(pts.Count);
        float rad = Mathf.Deg2Rad(angleDeg);
        float cos = Mathf.Cos(rad); float sin = Mathf.Sin(rad);
        foreach (var p in pts)
        {
            var v = p - center;
            float rx = v.X * cos - v.Y * sin;
            float ry = v.X * sin + v.Y * cos;
            res.Add(center + new Vector2(rx, ry));
        }
        return res;
    }
}

// Small struct to hold transformation parameters
public class TransformSpec
{
    public Vector2 Translate { get; set; } = Vector2.Zero;
    public float RotateDeg { get; set; } = 0f;
    public float Scale { get; set; } = 1f;
}
