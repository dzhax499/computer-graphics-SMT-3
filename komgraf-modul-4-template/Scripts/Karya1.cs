using System;
using System.Collections.Generic;
using Godot;

public partial class Karya1 : Node2D
{
    private Primitif _primitif = new Primitif();
    private BentukDasar _bentukDasar = new BentukDasar();
    private Transformasi _tf = new Transformasi();

    public override void _Ready()
    {
        ScreenUtils.Initialize(GetViewport());
        QueueRedraw();
    }

    public override void _Draw()
    {
        MarginPixel();
        GambarKordinat();

        // --- BENTUK AWAL ---
        var persegi = _bentukDasar.Persegi(250,300,50); // bentuk dasar persegi
        GraphicsUtils.PutPixelAll(this, persegi, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(7));

        // --- DEMO TRANSFORMASI ---
        DemoTranslasi(persegi);
        DemoScaling(persegi);
        DemoRotasi(persegi);
        DemoTranslasiScaling(persegi);
        DemoScalingTranslasi(persegi);
    }

    private void GambarKordinat()
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


    private void DemoTranslasi(List<Vector2> bentuk)
    {
        float[,] matrix = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrix);

        Vector2 pivot = new Vector2(0, 0);
        _tf.Translation(matrix, 100, 50, ref pivot);

        var hasil = _tf.GetTransformPoint(matrix, bentuk);
        GraphicsUtils.PutPixelAll(this, hasil, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(3));
    }

    private void DemoScaling(List<Vector2> bentuk)
    {
        float[,] matrix = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrix);

        Vector2 pivot = new Vector2(50, 50); // titik pusat scaling
        _tf.Scaling(matrix, 1.5f, 1.5f, pivot);

        var hasil = _tf.GetTransformPoint(matrix, bentuk);
        GraphicsUtils.PutPixelAll(this, hasil, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(4));
    }

    private void DemoRotasi(List<Vector2> bentuk)
    {
        float[,] matrix = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrix);

        Vector2 pivot = new Vector2(50, 50);
        _tf.RotationClockwise(matrix, 45, pivot);

        var hasil = _tf.GetTransformPoint(matrix, bentuk);
        GraphicsUtils.PutPixelAll(this, hasil, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5));
    }

    private void DemoTranslasiScaling(List<Vector2> bentuk)
    {
        float[,] matrix = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrix);

        Vector2 pivot = new Vector2(0, 0);
        _tf.Translation(matrix, 80, 30, ref pivot);
        _tf.Scaling(matrix, 2f, 2f, pivot);

        var hasil = _tf.GetTransformPoint(matrix, bentuk);
        GraphicsUtils.PutPixelAll(this, hasil, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(6));
    }

    private void DemoScalingTranslasi(List<Vector2> bentuk)
    {
        float[,] matrix = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrix);

        Vector2 pivot = new Vector2(0, 0);
        _tf.Scaling(matrix, 2f, 2f, pivot);
        _tf.Translation(matrix, 80, 30, ref pivot);

        var hasil = _tf.GetTransformPoint(matrix, bentuk);
        GraphicsUtils.PutPixelAll(this, hasil, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(8));
    }

    public override void _ExitTree()
    {
        NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
        base._ExitTree();
    }
}
