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
        CartesianLine();
        
        // Gambar semua bentuk dengan transformasi
        GambarBentukDenganTransformasi();
    }

private void CartesianLine()
	{
		var xAxis = _primitif.LineBresenham(
			ScreenUtils.MarginLeft,
			ScreenUtils.ScreenHeight / 2,
			ScreenUtils.MarginRight,
			ScreenUtils.ScreenHeight / 2
		);
		var yAxis = _primitif.LineBresenham(
			ScreenUtils.ScreenWidth / 2,
			ScreenUtils.MarginTop,
			ScreenUtils.ScreenWidth / 2,
			ScreenUtils.MarginBottom
		);
		GraphicsUtils.PutPixelAll(this, xAxis, color: ColorUtils.ColorStorage(0));
		GraphicsUtils.PutPixelAll(this, yAxis, color: ColorUtils.ColorStorage(0));
	}

    private void GambarBentukDenganTransformasi()
    {
        // // KUADRAN I - BENTUK ASLI (Original Shapes)
        // GambarBentukAsli();
        
        // // KUADRAN II - TRANSLASI (Translation)
        // GambarBentukTranslasi();
        
        // // KUADRAN III - ROTASI DAN SCALING (Rotation & Scaling)
        // GambarBentukRotasiScaling();
        
        // // KUADRAN IV - REFLEKSI DAN SHEARING (Reflection & Shearing)
        // GambarBentukRefleksiShearing();
        
        // // TENGAH - TRANSFORMASI KOMPOSIT (Composite Transformations)
        // GambarTransformasiKomposit();

        GambarBerbagaiTransformasiSatuBentuk();
    }

    private void GambarBerbagaiTransformasiSatuBentuk()
    {
        // Buat satu bentuk dasar (persegi panjang) yang akan kita transformasikan
        var bentukDasar = _bentukDasar.PersegiPanjang(0, 0, 50, 30);
        
        // Tampilkan bentuk dasar di Kuadran I
        GraphicsUtils.PutPixelAll(this, bentukDasar, GraphicsUtils.DrawStyle.DotDash, ColorUtils.ColorStorage(1));
        
        // --- TRANSFORMASI 1: TRANSLASI ---
        // Inisialisasi matriks identitas
        float[,] matrix1 = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrix1);
        
        // Terapkan translasi ke Kuadran II
        Vector2 coord1 = Vector2.Zero;
        _transformasi.Translation(matrix1, -150, 50, ref coord1);
        
        // Dapatkan titik-titik yang sudah ditransformasi
        var transformed1 = _transformasi.GetTransformPoint(matrix1, bentukDasar);
        
        // Gambarlah bentuk yang sudah ditransformasi
        GraphicsUtils.PutPixelAll(this, transformed1, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(2));
        
        // --- TRANSFORMASI 2: ROTASI + SCALING ---
        // Inisialisasi matriks baru
        float[,] matrix2 = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrix2);
        
        // Terapkan rotasi 45 derajat berlawanan arah jarum jam
        _transformasi.RotationCounterClockwise(matrix2, 45, Vector2.Zero);
        
        // Terapkan scaling 1.5x
        _transformasi.Scaling(matrix2, 1.5f, 1.5f, Vector2.Zero);
        
        // Terapkan translasi ke Kuadran III
        Vector2 coord2 = Vector2.Zero;
        _transformasi.Translation(matrix2, -150, -120, ref coord2);
        
        // Dapatkan titik-titik yang sudah ditransformasi
        var transformed2 = _transformasi.GetTransformPoint(matrix2, bentukDasar);
        
        // Gambarlah bentuk yang sudah ditransformasi
        GraphicsUtils.PutPixelAll(this, transformed2, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(3));

        // --- TRANSFORMASI 3: REFLEKSI + SHEARING ---
        // Inisialisasi matriks baru
        float[,] matrix3 = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrix3);

        // Terapkan refleksi terhadap sumbu X
        Vector2 coord3 = Vector2.Zero;
        _transformasi.ReflectionToX(matrix3, ref coord3);
        
        // Terapkan shearing pada sumbu Y
        _transformasi.Shearing(matrix3, 0.0f, 0.5f, Vector2.Zero);
        
        // Terapkan translasi ke Kuadran IV
        _transformasi.Translation(matrix3, 140, -100, ref coord3);
        
        // Dapatkan titik-titik yang sudah ditransformasi
        var transformed3 = _transformasi.GetTransformPoint(matrix3, bentukDasar);
        
        // Gambarlah bentuk yang sudah ditransformasi
        GraphicsUtils.PutPixelAll(this, transformed3, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(4));
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