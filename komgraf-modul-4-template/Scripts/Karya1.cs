using System;

namespace Godot;

public partial class Karya1 : Node2D
{
    private Primitif _primitif = new Primitif();
    private BentukDasar _bentukDasar = new BentukDasar();
    private Transformasi _transformasi = new Transformasi();

    public override void _Ready()
    {
        ScreenUtils.Initialize(GetViewport());
        QueueRedraw();
    }

    public override void _Draw()
    {
        MarginPixel();
        MyTransformations();
    }

    private void MyTransformations()
    {
        // Gambar koordinat sistem terlebih dahulu
        GambarKordinat();
        
        // Gambar semua bentuk dengan transformasi
        GambarBentukDenganTransformasi();
    }

    private void GambarKordinat()
    {
        // Sumbu X (horizontal) - warna merah
        var sumbuX = _bentukDasar.SumbuX(800);
        GraphicsUtils.PutPixelAll(this, sumbuX, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(1));
        
        // Sumbu Y (vertikal) - warna hijau  
        var sumbuY = _bentukDasar.SumbuY(600);
        GraphicsUtils.PutPixelAll(this, sumbuY, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(2));
        
        // Grid lines untuk referensi - warna abu-abu
        var gridLines = _bentukDasar.GridLines(50, 200);
        GraphicsUtils.PutPixelAll(this, gridLines, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(3));
    }

    private void GambarBentukDenganTransformasi()
    {
        // KUADRAN I - BENTUK ASLI (Original Shapes)
        GambarBentukAsli();
        
        // KUADRAN II - TRANSLASI (Translation)
        GambarBentukTranslasi();
        
        // KUADRAN III - ROTASI DAN SCALING (Rotation & Scaling)
        GambarBentukRotasiScaling();
        
        // KUADRAN IV - REFLEKSI DAN SHEARING (Reflection & Shearing)
        GambarBentukRefleksiShearing();
        
        // TENGAH - TRANSFORMASI KOMPOSIT (Composite Transformations)
        GambarTransformasiKomposit();
    }

    private void GambarBentukAsli()
    {
        // PERSEGI PANJANG di (120, 50) dengan panjang 60, lebar 30
        var persegiPanjang = _bentukDasar.PersegiPanjang(120, 50, 60, 30);
        GraphicsUtils.PutPixelAll(this, persegiPanjang, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5));
    }

    private void GambarBentukTranslasi()
    {
        // PERSEGI PANJANG dengan translasi ke (-150, 50)
        // Original: (0,0) length=60, width=30 -> Translated: (-150, 50)
        var persegiPanjang = _bentukDasar.PersegiPanjang(0, 0, 60, 30);
        float[,] matrixPersegiPanjang = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrixPersegiPanjang);
        Vector2 coordPersegiPanjang = Vector2.Zero;
        _transformasi.Translation(matrixPersegiPanjang, -150, 50,
        ref coordPersegiPanjang);
        var persegiPanjangTransformed = _transformasi.GetTransformPoint(matrixPersegiPanjang, persegiPanjang);
        GraphicsUtils.PutPixelAll(this, persegiPanjangTransformed, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(5));
    }

    private void GambarBentukRotasiScaling()
    {
        // PERSEGI PANJANG dengan rotasi 30° dan scaling 0.8x lalu translasi ke (-150, -120)
        // Original: (0,0) length=50, width=30 -> Rotated 30° -> Scaled 0.8x -> Translated (-150, -120)
        var persegiPanjang = _bentukDasar.PersegiPanjang(0, 0, 50, 30);
        float[,] matrixPersegiPanjang = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrixPersegiPanjang);
        Vector2 pivotPersegiPanjang = Vector2.Zero;
        _transformasi.RotationCounterClockwise(matrixPersegiPanjang, 30, pivotPersegiPanjang);
        _transformasi.Scaling(matrixPersegiPanjang, 0.8f, 0.8f, pivotPersegiPanjang);
        Vector2 coordPersegiPanjang = Vector2.Zero;
        _transformasi.Translation(matrixPersegiPanjang, -150, -120, ref coordPersegiPanjang);
        var persegiPanjangTransformed = _transformasi.GetTransformPoint(matrixPersegiPanjang, persegiPanjang);
        GraphicsUtils.PutPixelAll(this, persegiPanjangTransformed, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(5));
    }

    private void GambarBentukRefleksiShearing()
    {
        //PERSEGI dengan refleksi ke sumbu Y lalu translasi ke (140, 50)
        // Original: (-15,-15) size 30x30 -> Reflect Y -> Translated (140, 50)
        var persegi = _bentukDasar.Persegi(-15, -15, 30);
        float[,] matrixPersegi = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrixPersegi);
        Vector2 coordPersegi = Vector2.Zero;
        _transformasi.ReflectionToY(matrixPersegi, ref coordPersegi);
        _transformasi.Translation(matrixPersegi, 140, 50, ref coordPersegi);
        var persegiTransformed = _transformasi.GetTransformPoint(matrixPersegi, persegi);
        GraphicsUtils.PutPixelAll(this, persegiTransformed, GraphicsUtils.DrawStyle.DotDash, ColorUtils.ColorStorage(7)); 
    }

    private void GambarTransformasiKomposit()
    {
        //PERSEGI PANJANG dengan multiple transformations
        // Original: (0,0) length=60, width=30 -> Shear X=0.2 -> Scale 1.5x0.5 -> Rotate 45° -> Translate (0, -150)
        var persegiPanjang = _bentukDasar.PersegiPanjang(0, 0, 60, 30);
        float[,] matrixPersegiPanjang = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrixPersegiPanjang);
        Vector2 pivotPersegiPanjang = Vector2.Zero;
        _transformasi.Shearing(matrixPersegiPanjang, 0.2f, 0.0f, pivotPersegiPanjang);
        _transformasi.Scaling(matrixPersegiPanjang, 1.5f, 0.5f, pivotPersegiPanjang);
        _transformasi.RotationCounterClockwise(matrixPersegiPanjang, 45, pivotPersegiPanjang);
        Vector2 coordPersegiPanjang = Vector2.Zero;
        _transformasi.Translation(matrixPersegiPanjang, 0, -150, ref coordPersegiPanjang);
        var persegiPanjangTransformed = _transformasi.GetTransformPoint(matrixPersegiPanjang, persegiPanjang);
        GraphicsUtils.PutPixelAll(this, persegiPanjangTransformed, GraphicsUtils.DrawStyle.DotDash, ColorUtils.ColorStorage(6));
    }

    private void MarginPixel()
    {
        var margin = _bentukDasar.Margin();
        GraphicsUtils.PutPixelAll(this, margin, color: ColorUtils.ColorStorage(0));
    }

    public override void _ExitTree()
    {
        NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
        NodeUtils.DisposeAndNull(_transformasi, "_transformasi");
        base._ExitTree();
    }
}