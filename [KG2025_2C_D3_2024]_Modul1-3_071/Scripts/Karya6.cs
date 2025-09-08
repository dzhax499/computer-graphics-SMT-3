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

// namespace Godot;

// using Godot;
// using System.Collections.Generic;

// public partial class Karya7 : Node2D
// {
//     private BentukDasar _bentukDasar;

//     public override void _Ready()
//     {
//         _bentukDasar = new BentukDasar();
//         QueueRedraw();
//     }

//     public override void _Draw()
//     {
//         // Gambar sumbu koordinat untuk referensi
//         DrawAxes();
        
//         // Gambar permen lollipop
//         DrawLollipop();
        
//         // Gambar permen batang (candy cane)
//         DrawCandyCane();
        
//         // Gambar permen bulat kecil
//         DrawSmallCandies();
//     }

//     private void DrawAxes()
//     {
//         // Gambar sumbu X dan Y
//         var sumbuX = _bentukDasar.SumbuX(400);
//         GraphicsUtils.PutPixelAll(this, sumbuX, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(14)); // Vanilla/light color
        
//         var sumbuY = _bentukDasar.SumbuY(300);
//         GraphicsUtils.PutPixelAll(this, sumbuY, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(14));
//     }

//     private void DrawLollipop()
//     {
//         // Permen lollipop besar di tengah atas
//         Vector2 centerLollipop = new Vector2(-20, 50);
        
//         // Kepala permen - lingkaran besar
//         var lollipopHead = _bentukDasar.Lingkaran(centerLollipop, 40);
//         GraphicsUtils.PutPixelAll(this, lollipopHead, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(1)); // Red
        
//         // Lingkaran dalam untuk efek berlapis
//         var innerCircle1 = _bentukDasar.Lingkaran(centerLollipop, 30);
//         GraphicsUtils.PutPixelAll(this, innerCircle1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(4)); // Yellow
        
//         var innerCircle2 = _bentukDasar.Lingkaran(centerLollipop, 20);
//         GraphicsUtils.PutPixelAll(this, innerCircle2, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(8)); // Rose/Pink
        
//         var innerCircle3 = _bentukDasar.Lingkaran(centerLollipop, 10);
//         GraphicsUtils.PutPixelAll(this, innerCircle3, GraphicsUtils.DrawStyle.DotDot, Colors.White);
        
//         // Stick permen - persegi panjang vertikal
//         var stick = _bentukDasar.PersegiPanjang(centerLollipop.X - 3, centerLollipop.Y - 40, 6, -60);
//         GraphicsUtils.PutPixelAll(this, stick, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(13)); // Brown
//     }

//     private void DrawCandyCane()
//     {
//         // Permen tongkat di sebelah kiri
//         Vector2 caneStart = new Vector2(-120, -20);
        
//         // Bagian vertikal tongkat
//         var verticalPart = _bentukDasar.PersegiPanjang(caneStart.X, caneStart.Y, 8, 80);
//         GraphicsUtils.PutPixelAll(this, verticalPart, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(1), 3, 2); // Red with strips
        
//         // Bagian horizontal (bengkok)
//         var horizontalPart = _bentukDasar.PersegiPanjang(caneStart.X, caneStart.Y + 80, 30, 8);
//         GraphicsUtils.PutPixelAll(this, horizontalPart, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(1), 3, 2);
        
//         // Ujung bengkokan
//         var bendPart = _bentukDasar.PersegiPanjang(caneStart.X + 30, caneStart.Y + 60, 8, 20);
//         GraphicsUtils.PutPixelAll(this, bendPart, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(1), 3, 2);
        
//         // Garis putih untuk efek bergaris
//         var whiteStripe1 = _bentukDasar.PersegiPanjang(caneStart.X + 2, caneStart.Y, 4, 80);
//         GraphicsUtils.PutPixelAll(this, whiteStripe1, GraphicsUtils.DrawStyle.DotStripDot, Colors.White, 2, 4);
        
//         var whiteStripe2 = _bentukDasar.PersegiPanjang(caneStart.X, caneStart.Y + 82, 30, 4);
//         GraphicsUtils.PutPixelAll(this, whiteStripe2, GraphicsUtils.DrawStyle.DotStripDot, Colors.White, 2, 4);
//     }

//     private void DrawSmallCandies()
//     {
//         // Permen bulat kecil-kecil di sekitar
//         List<Vector2> candyPositions = new List<Vector2>
//         {
//             new Vector2(80, 60),    // Kanan atas
//             new Vector2(90, -10),   // Kanan tengah
//             new Vector2(70, -60),   // Kanan bawah
//             new Vector2(-80, -70),  // Kiri bawah
//             new Vector2(-100, 20),  // Kiri tengah
//             new Vector2(120, 30),   // Jauh kanan
//             new Vector2(-140, -30)  // Jauh kiri
//         };
        
//         Color[] candyColors = 
//         {
//             ColorUtils.ColorStorage(2),  // Green
//             ColorUtils.ColorStorage(3),  // Blue
//             ColorUtils.ColorStorage(5),  // Magenta
//             ColorUtils.ColorStorage(6),  // Cyan
//             ColorUtils.ColorStorage(7),  // Orange
//             ColorUtils.ColorStorage(9),  // Violet
//             ColorUtils.ColorStorage(11)  // Spring Green
//         };
        
//         for (int i = 0; i < candyPositions.Count; i++)
//         {
//             // Permen bulat dengan berbagai ukuran
//             int radius = 12 + (i % 3) * 4; // Radius bervariasi antara 12, 16, 20
//             var candy = _bentukDasar.Lingkaran(candyPositions[i], radius);
//             GraphicsUtils.PutPixelAll(this, candy, GraphicsUtils.DrawStyle.CircleDot, candyColors[i], 2, 1);
            
//             // Highlight putih kecil di tengah
//             var highlight = _bentukDasar.Lingkaran(candyPositions[i], radius / 3);
//             GraphicsUtils.PutPixelAll(this, highlight, GraphicsUtils.DrawStyle.DotDot, Colors.White);
//         }
        
//         // Tambahkan beberapa permen berbentuk persegi (tablet)
//         DrawSquareCandies();
//     }
    
//     private void DrawSquareCandies()
//     {
//         List<Vector2> tabletPositions = new List<Vector2>
//         {
//             new Vector2(40, -80),
//             new Vector2(-60, 80),
//             new Vector2(110, -40),
//             new Vector2(-110, 60)
//         };
        
//         Color[] tabletColors = 
//         {
//             ColorUtils.ColorStorage(4),  // Yellow
//             ColorUtils.ColorStorage(8),  // Rose
//             ColorUtils.ColorStorage(10), // Azure
//             ColorUtils.ColorStorage(12)  // Chartreuse
//         };
        
//         for (int i = 0; i < tabletPositions.Count; i++)
//         {
//             // Tablet persegi dengan sudut membulat (simulasi dengan persegi + elips kecil di sudut)
//             var tablet = _bentukDasar.Persegi(tabletPositions[i].X, tabletPositions[i].Y, 20);
//             GraphicsUtils.PutPixelAll(this, tablet, GraphicsUtils.DrawStyle.DotDot, tabletColors[i]);
            
//             // Border dalam
//             var innerBorder = _bentukDasar.Persegi(tabletPositions[i].X + 3, tabletPositions[i].Y + 3, 14);
//             GraphicsUtils.PutPixelAll(this, innerBorder, GraphicsUtils.DrawStyle.DotDot, Colors.White);
            
//             // Inti tablet
//             var core = _bentukDasar.Persegi(tabletPositions[i].X + 6, tabletPositions[i].Y + 6, 8);
//             GraphicsUtils.PutPixelAll(this, core, GraphicsUtils.DrawStyle.DotDot, 
//                 ColorUtils.BrightenColor(tabletColors[i], 0.3f));
//         }
//     }

//     public override void _ExitTree()
//     {
//         _bentukDasar?.Dispose();
//     }
// }