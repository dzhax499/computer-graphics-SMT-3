namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

public partial class Karya6 : Node2D
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
        MarginPixel();
        ScreenUtils.DrawAxes(this, _primitif);

        MyGaris();
        MyPersegi();
        MyPersegiPanjang();
        MySegitigaSiku();
        MyTrapesiumSiku();
        MyLingkaran();
        MyElips();
    }

    private void MarginPixel()
    {
        var margin = _bentukDasar.Margin();
        GraphicsUtils.PutPixelAll(this, margin, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(6));
    }

    private void MyGaris()
    {
        // Kuadran I (garis-garis)
        var garis1 = _primitif.LineBresenham(
            ScreenUtils.ToScreenCoordinate(100, 250).X,
            ScreenUtils.ToScreenCoordinate(100, 250).Y,
            ScreenUtils.ToScreenCoordinate(500, 50).X,
            ScreenUtils.ToScreenCoordinate(500, 50).Y
        );
        GraphicsUtils.PutPixelAll(this, garis1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

        // Kuadran II (garis normal)
        var garis2 = _primitif.LineBresenham(
            ScreenUtils.ToScreenCoordinate(-100, 250).X,
            ScreenUtils.ToScreenCoordinate(-100, 250).Y,
            ScreenUtils.ToScreenCoordinate(-500, 50).X,
            ScreenUtils.ToScreenCoordinate(-500, 50).Y
        );
        GraphicsUtils.PutPixelAll(this, garis2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

        // Kuadran III (titik-titik)
        var garis3 = _primitif.LineBresenham(
            ScreenUtils.ToScreenCoordinate(-100, -250).X,
            ScreenUtils.ToScreenCoordinate(-100, -250).Y,
            ScreenUtils.ToScreenCoordinate(-500, -50).X,
            ScreenUtils.ToScreenCoordinate(-500, -50).Y
        );
        GraphicsUtils.PutPixelAll(this, garis3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

        // Kuadran IV (titik-garis-titik)
        var garis4 = _primitif.LineBresenham(
            ScreenUtils.ToScreenCoordinate(100, -250).X,
            ScreenUtils.ToScreenCoordinate(100, -250).Y,
            ScreenUtils.ToScreenCoordinate(500, -50).X,
            ScreenUtils.ToScreenCoordinate(500, -50).Y
        );
        GraphicsUtils.PutPixelAll(this, garis4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
    }

    private void MyPersegi()
    {
        // Kuadran I (garis-garis)
        var titik1 = ScreenUtils.ToScreenCoordinate(200, 200);
        var persegi1 = _primitif.Persegi(titik1.X, titik1.Y, 100);
        GraphicsUtils.PutPixelAll(this, persegi1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

        // Kuadran II (garis normal)
        var titik2 = ScreenUtils.ToScreenCoordinate(-300, 200);
        var persegi2 = _primitif.Persegi(titik2.X, titik2.Y, 100);
        GraphicsUtils.PutPixelAll(this, persegi2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

        // Kuadran III (titik-titik)
        var titik3 = ScreenUtils.ToScreenCoordinate(-300, -100);
        var persegi3 = _primitif.Persegi(titik3.X, titik3.Y, 100);
        GraphicsUtils.PutPixelAll(this, persegi3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

        // Kuadran IV (titik-garis-titik)
        var titik4 = ScreenUtils.ToScreenCoordinate(200, -100);
        var persegi4 = _primitif.Persegi(titik4.X, titik4.Y, 100);
        GraphicsUtils.PutPixelAll(this, persegi4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
    }

    private void MyPersegiPanjang()
    {
        // Kuadran I (garis-garis)
        var titik1 = ScreenUtils.ToScreenCoordinate(200, 200);
        var persegiPanjang1 = _primitif.PersegiPanjang(titik1.X, titik1.Y, 200, 100);
        GraphicsUtils.PutPixelAll(this, persegiPanjang1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

        // Kuadran II (garis normal)
        var titik2 = ScreenUtils.ToScreenCoordinate(-400, 200);
        var persegiPanjang2 = _primitif.PersegiPanjang(titik2.X, titik2.Y, 200, 100);
        GraphicsUtils.PutPixelAll(this, persegiPanjang2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

        // Kuadran III (titik-titik)
        var titik3 = ScreenUtils.ToScreenCoordinate(-400, -100);
        var persegiPanjang3 = _primitif.PersegiPanjang(titik3.X, titik3.Y, 200, 100);
        GraphicsUtils.PutPixelAll(this, persegiPanjang3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

        // Kuadran IV (titik-garis-titik)
        var titik4 = ScreenUtils.ToScreenCoordinate(200, -100);
        var persegiPanjang4 = _primitif.PersegiPanjang(titik4.X, titik4.Y, 200, 100);
        GraphicsUtils.PutPixelAll(this, persegiPanjang4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
    }

    private void MySegitigaSiku()
    {
        // Kuadran I (garis-garis)
        var titik1 = new Vector2(200, 100);
        var segitiga1 = _bentukDasar.SegitigaSiku(titik1, 200, 100);
        GraphicsUtils.PutPixelAll(this, segitiga1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

        // Kuadran II (garis normal)
        var titik2 = new Vector2(-400, 100);
        var segitiga2 = _bentukDasar.SegitigaSiku(titik2, 200, 100);
        GraphicsUtils.PutPixelAll(this, segitiga2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

        // Kuadran III (titik-titik)
        var titik3 = new Vector2(-400, -200);
        var segitiga3 = _bentukDasar.SegitigaSiku(titik3, 200, 100);
        GraphicsUtils.PutPixelAll(this, segitiga3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

        // Kuadran IV (titik-garis-titik)
        var titik4 = new Vector2(200, -200);
        var segitiga4 = _bentukDasar.SegitigaSiku(titik4, 200, 100);
        GraphicsUtils.PutPixelAll(this, segitiga4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
    }

    private void MyTrapesiumSiku()
    {
        // Kuadran I (garis-garis)
        var titik1 = new Vector2(200, 100);
        var trapesium1 = _bentukDasar.TrapesiumSiku(titik1, 150, 200, 100);
        GraphicsUtils.PutPixelAll(this, trapesium1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 1);

        // Kuadran II (garis normal)
        var titik2 = new Vector2(-400, 100);
        var trapesium2 = _bentukDasar.TrapesiumSiku(titik2, 150, 200, 100);
        GraphicsUtils.PutPixelAll(this, trapesium2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

        // Kuadran III (titik-titik)
        var titik3 = new Vector2(-400, -200);
        var trapesium3 = _bentukDasar.TrapesiumSiku(titik3, 200, 150, 100);
        GraphicsUtils.PutPixelAll(this, trapesium3, GraphicsUtils.DrawStyle.StripStrip, Colors.Green, stripLength: 1, gap: 3);

        // Kuadran IV (titik-garis-titik)
        var titik4 = new Vector2(200, -200);
        var trapesium4 = _bentukDasar.TrapesiumSiku(titik4, 200, 150, 100);
        GraphicsUtils.PutPixelAll(this, trapesium4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
    }

    private void MyLingkaran()
    {
        // Kuadran I (garis-garis)
        var titik1 = new Vector2(300, 200);
        var lingkaran1 = _bentukDasar.Lingkaran(titik1, 40);
        GraphicsUtils.PutPixelAll(this, lingkaran1, GraphicsUtils.DrawStyle.CircleDotStrip, Colors.Red, stripLength: 5, gap: 5);

        // Kuadran II (garis normal)
        var titik2 = new Vector2(-320, 220);
        var lingkaran2 = _bentukDasar.Lingkaran(titik2, 40);
        GraphicsUtils.PutPixelAll(this, lingkaran2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

        // Kuadran III (titik-titik)
        var titik3 = new Vector2(-320, -220);
        var lingkaran3 = _bentukDasar.Lingkaran(titik3, 40);
        GraphicsUtils.PutPixelAll(this, lingkaran3, GraphicsUtils.DrawStyle.CircleStrip, Colors.Green, stripLength: 1, gap: 3);

        // Kuadran IV (titik-garis-titik)
        var titik4 = new Vector2(300, -200);
        var lingkaran4 = _bentukDasar.Lingkaran(titik4, 40);
        GraphicsUtils.PutPixelAll(this, lingkaran4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 20, gap: 5);
    }

    private void MyElips()
    {
        // Kuadran I (garis-garis)
        var titik1 = new Vector2(300, 200);
        var elips1 = _bentukDasar.Elips(titik1, 60, 30);
        GraphicsUtils.PutPixelAll(this, elips1, GraphicsUtils.DrawStyle.CircleStrip, Colors.Red, gap: 2);

        // Kuadran II (garis normal)
        var titik2 = new Vector2(-320, 220);
        var elips2 = _bentukDasar.Elips(titik2, 60, 30);
        GraphicsUtils.PutPixelAll(this, elips2, GraphicsUtils.DrawStyle.DotDot, Colors.Blue);

        // Kuadran III (titik-titik)
        var titik3 = new Vector2(-320, -220);
        var elips3 = _bentukDasar.Elips(titik3, 60, 30);
        GraphicsUtils.PutPixelAll(this, elips3, GraphicsUtils.DrawStyle.EllipseStrip, Colors.Green, stripLength: 1, gap: 3);

        // Kuadran IV (titik-garis-titik)
        var titik4 = new Vector2(300, -200);
        var elips4 = _bentukDasar.Elips(titik4, 60, 30);
        GraphicsUtils.PutPixelAll(this, elips4, GraphicsUtils.DrawStyle.DotDash, Colors.Yellow, stripLength: 10, gap: 5);
    }

    public override void _ExitTree()
    {
        NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
        base._ExitTree();
    }
}