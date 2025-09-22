    namespace Godot;

    using Godot;
    using System;
    using System.Collections.Generic;
    using System.Numerics; // Import System.Numerics for Matrix4x4

public partial class Karya3 : Node2D
{
    private BentukDasar _bentukDasar = new BentukDasar();
    private Transformasi _transformasi = new Transformasi();

    private float _rotationAngle = 0f;
    private float _scaleFactor = 1f;
    private float _translationX = 0f;
    private Vector2 _initialPosition = new Vector2(100, 100);
    private float _ukuran = 50;

    // Matriks transformasi yang akan diperbarui
    private float[,] _transformationMatrix = new float[3, 3];
    public override void _Ready()
    {
        ScreenUtils.Initialize(GetViewport());
        Transformasi.Matrix3x3Identity(_transformationMatrix); // Inisialisasi

        // Terapkan transformasi di sini, sekali saja!
        _transformasi.RotationClockwise(_transformationMatrix, 45, new Vector2(100, 100)); // Putar 45 derajat
        _transformasi.Scaling(_transformationMatrix, 2.0f, 2.0f, new Vector2(100, 100)); // Perbesar 2x
        _transformasi.Translation(_transformationMatrix, 50, 50, ref _initialPosition); // Geser 50 piksel

        QueueRedraw();
    }

    public override void _Process(double delta)
    {
        
    }

    public override void _Draw()
    {
        // Gambar Trapesium yang ditransformasi
        var titikAwalTrapesiumSiku1 = new Vector2(250, 200);
        var trapesiumSiku1 = _bentukDasar.TrapesiumSiku(titikAwalTrapesiumSiku1, 30, 50, 40);
        var transformedTrapesium = _transformasi.GetTransformPoint(_transformationMatrix, trapesiumSiku1);
        GraphicsUtils.PutPixelAll(this, transformedTrapesium, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(6));

        // Gambar Segi Enam yang ditransformasi
        var segienamCenter = new Vector2(100, 100);
        var segienam = _bentukDasar.SegienamBeraturan(segienamCenter, 50);
        var transformedSegienam = _transformasi.GetTransformPoint(_transformationMatrix, segienam);
        GraphicsUtils.PutPixelAll(this, transformedSegienam, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(11));
    }
}