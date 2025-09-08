using Godot;
using System;

// Karya6.cs - 4 Scene berbeda dengan style garis berbeda
public partial class Karya6 : Node2D
{
    private BentukDasar _bentukDasar = new BentukDasar();
    private int currentScene = 0; // 0=Normal, 1=Titik-titik, 2=Titik-garis-titik, 3=Garis-garis

    public override void _Ready()
    {
        ScreenUtils.Initialize(GetViewport());
        QueueRedraw();
    }

    // public override void _Input(InputEvent @event)
    // {
    //     // Tekan Space untuk ganti scene
    //     if (@event is InputEventKey keyEvent && keyEvent.Pressed && keyEvent.Keycode == Key.Space)
    //     {
    //         currentScene = (currentScene + 1) % 4;
    //         QueueRedraw();
    //     }
    // }

    public override void _Draw()
    {
        MarginPixel();
        // Kuadran I - Normal
        var persegi1 = _bentukDasar.Persegi(100, 100, 80);
        GraphicsUtils.PutPixelAll(this, persegi1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(1));

        // Kuadran II - Titik-titik
        var persegi2 = _bentukDasar.Persegi(-200, 100, 80);
        GraphicsUtils.PutPixelAll(this, persegi2, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(2), 3, 4);

        // Kuadran III - Titik-garis-titik
        var persegi3 = _bentukDasar.Persegi(-200, -100, 80);
        GraphicsUtils.PutPixelAll(this, persegi3, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(3));

        // Kuadran IV - Garis-garis
        var persegi4 = _bentukDasar.Persegi(100, -100, 80);
        GraphicsUtils.PutPixelAll(this, persegi4, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(4));
    }

    // private void DrawCurrentScene()
    // {
    //     GraphicsUtils.DrawStyle style;
    //     int gap = 2;
        
    //     switch (currentScene)
    //     {
    //         case 0: style = GraphicsUtils.DrawStyle.DotDot; break;
    //         case 1: style = GraphicsUtils.DrawStyle.DotDot; gap = 3; break; // Titik-titik dengan gap
    //         case 2: style = GraphicsUtils.DrawStyle.DotStripDot; gap = 2; break;
    //         case 3: style = GraphicsUtils.DrawStyle.StripStrip; gap = 1; break;
    //         default: style = GraphicsUtils.DrawStyle.DotDot; break;
    //     }

    //     // Gambar semua bentuk dengan style yang sama
    //     DrawAllShapesWithStyle(style, gap);
    // }

    // private void DrawAllShapesWithStyle(GraphicsUtils.DrawStyle style, int gap)
    // {
    //     // Persegi
    //     var persegi = _bentukDasar.Persegi(50, 50, 80);
    //     GraphicsUtils.PutPixelAll(this, persegi, style, ColorUtils.ColorStorage(1), 3, gap);

    //     // Persegi Panjang
    //     var persegiPanjang = _bentukDasar.PersegiPanjang(-200, 50, 100, 60);
    //     GraphicsUtils.PutPixelAll(this, persegiPanjang, style, ColorUtils.ColorStorage(2), 3, gap);

    //     // Segitiga Siku-siku
    //     var segitiga = _bentukDasar.SegitigaSiku(new Vector2(50, -100), 80, 60);
    //     GraphicsUtils.PutPixelAll(this, segitiga, style, ColorUtils.ColorStorage(3), 3, gap);

    //     // Trapesium Siku
    //     var trapesium = _bentukDasar.TrapesiumSiku(new Vector2(-200, -100), 40, 80, 60);
    //     GraphicsUtils.PutPixelAll(this, trapesium, style, ColorUtils.ColorStorage(4), 3, gap);

    //     // Lingkaran - dengan style khusus circle
    //     var lingkaran = _bentukDasar.Lingkaran(new Vector2(250, 100), 40);
    //     var circleStyle = GetCircleStyle(style);
    //     GraphicsUtils.PutPixelAll(this, lingkaran, circleStyle, ColorUtils.ColorStorage(5), 3, gap);

    //     // Ellips - dengan style khusus ellipse
    //     var ellips = _bentukDasar.Elips(new Vector2(-50, 200), 60, 35);
    //     var ellipseStyle = GetEllipseStyle(style);
    //     GraphicsUtils.PutPixelAll(this, ellips, ellipseStyle, ColorUtils.ColorStorage(6), 3, gap);
    // }

    // private GraphicsUtils.DrawStyle GetCircleStyle(GraphicsUtils.DrawStyle baseStyle)
    // {
    //     switch (baseStyle)
    //     {
    //         case GraphicsUtils.DrawStyle.DotDot: return GraphicsUtils.DrawStyle.CircleDot;
    //         case GraphicsUtils.DrawStyle.DotStripDot: return GraphicsUtils.DrawStyle.CircleDotStrip;
    //         case GraphicsUtils.DrawStyle.StripStrip: return GraphicsUtils.DrawStyle.CircleStrip;
    //         default: return GraphicsUtils.DrawStyle.CircleDot;
    //     }
    // }

    // private GraphicsUtils.DrawStyle GetEllipseStyle(GraphicsUtils.DrawStyle baseStyle)
    // {
    //     switch (baseStyle)
    //     {
    //         case GraphicsUtils.DrawStyle.DotDot: return GraphicsUtils.DrawStyle.EllipseDot;
    //         case GraphicsUtils.DrawStyle.DotStripDot: return GraphicsUtils.DrawStyle.EllipseDotStrip;
    //         case GraphicsUtils.DrawStyle.StripStrip: return GraphicsUtils.DrawStyle.EllipseStrip;
    //         default: return GraphicsUtils.DrawStyle.EllipseDot;
    //     }
    // }

    // private void DrawInstructions()
    // {
    //     string[] sceneNames = { "Normal (DotDot)", "Titik-titik (Gap)", "Titik-Garis-Titik", "Garis-garis" };
    //     // Gunakan DrawString jika tersedia, atau buat text node
    //     // DrawString($"Scene {currentScene + 1}: {sceneNames[currentScene]} - Tekan SPACE untuk ganti", 
    //     //     new Vector2(10, 30), ColorUtils.ColorStorage(0));
    // }

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