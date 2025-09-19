using Godot;
using System;
using System.Collections.Generic;

public partial class Quiz_1 : Node2D
{
    private Primitif _primitif = new Primitif();
    private BentukDasar _bentukDasar = new BentukDasar();
    //rotasi
    private List<Vector2> RotatePoints(List<Vector2> points, Vector2 center, float angleDeg)
    {
        var rotated = new List<Vector2>();
        float angleRad = angleDeg * Mathf.Pi / 180f;

        foreach (var p in points)
        {
            float x = p.X - center.X;
            float y = p.Y - center.Y;
            float xr = x * Mathf.Cos(angleRad) - y * Mathf.Sin(angleRad);
            float yr = x * Mathf.Sin(angleRad) + y * Mathf.Cos(angleRad);
            rotated.Add(new Vector2(xr + center.X, yr + center.Y));
        }
        return rotated;
    }
    public override void _Ready()
    {
        ScreenUtils.Initialize(GetViewport());
        QueueRedraw();
    }

    public override void _Draw()
    {
        MarginPixel();
        segitigasamakaki();
    }

    private void MarginPixel()
    {
        var margin = _bentukDasar.Margin();
        GraphicsUtils.PutPixelAll(this, margin, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(6));
    }

    private void segitigasamakaki()
    {
        // segitiga bawah 1 buah
        var titikAwal1 = ScreenUtils.ToScreenCoordinate(0, 300);
		var segitigasamakaki1 = _bentukDasar.SegitigaSamaKaki(titikAwal1, 70, 70);
        var segitigaRotated = RotatePoints(segitigasamakaki1, titikAwal1, 0);
        GraphicsUtils.PutPixelAll(this, segitigaRotated, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(7));

        // segitiga berpasangan menghadap keluar 
        var titikAwal2 = ScreenUtils.ToScreenCoordinate(35, 230);
		var segitigasamakaki2 = _bentukDasar.SegitigaSamaKaki(titikAwal2, 70, 70);
        var segitigaRotated1 = RotatePoints(segitigasamakaki2, titikAwal2, 90);
        GraphicsUtils.PutPixelAll(this, segitigaRotated1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(12));

        var titikAwal3 = ScreenUtils.ToScreenCoordinate(35, 160);
		var segitigasamakaki3 = _bentukDasar.SegitigaSamaKaki(titikAwal3, 70, 70);
        var segitigaRotated2 = RotatePoints(segitigasamakaki3, titikAwal3, 270);
        GraphicsUtils.PutPixelAll(this, segitigaRotated2, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(12));

        // segitiga berpasangan menghadap kedalam
        var titikAwal4 = ScreenUtils.ToScreenCoordinate(-35, 125);
		var segitigasamakaki4 = _bentukDasar.SegitigaSamaKaki(titikAwal4, 70, 70);
        var segitigaRotated3 = RotatePoints(segitigasamakaki4, titikAwal4, 270);
        GraphicsUtils.PutPixelAll(this, segitigaRotated3, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(7));

        var titikAwal5 = ScreenUtils.ToScreenCoordinate(105, 195);
		var segitigasamakaki5 = _bentukDasar.SegitigaSamaKaki(titikAwal5, 70, 70);
        var segitigaRotated4 = RotatePoints(segitigasamakaki5, titikAwal5, 90);
        GraphicsUtils.PutPixelAll(this, segitigaRotated4, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(7));
        
        // segitiga menghadap keluar ke 2         
        var titikAwal6 = ScreenUtils.ToScreenCoordinate(35,160);
		var segitigasamakaki6 = _bentukDasar.SegitigaSamaKaki(titikAwal6, 70, 70);
        var segitigaRotated5 = RotatePoints(segitigasamakaki6, titikAwal6, 90);
        GraphicsUtils.PutPixelAll(this, segitigaRotated5, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(8));

        var titikAwal7 = ScreenUtils.ToScreenCoordinate(35, 90);
		var segitigasamakaki7 = _bentukDasar.SegitigaSamaKaki(titikAwal7, 70, 70);
        var segitigaRotated6 = RotatePoints(segitigasamakaki7, titikAwal7, 270);
        GraphicsUtils.PutPixelAll(this, segitigaRotated6, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(8));

        // segitiga berpasangan menghadap kedalam ke 2
        var titikAwal8 = ScreenUtils.ToScreenCoordinate(-35, 55);
		var segitigasamakaki8 = _bentukDasar.SegitigaSamaKaki(titikAwal8, 70, 70);
        var segitigaRotated7 = RotatePoints(segitigasamakaki8, titikAwal8, 270);
        GraphicsUtils.PutPixelAll(this, segitigaRotated7, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(8));

        var titikAwal9 = ScreenUtils.ToScreenCoordinate(105, 125);
		var segitigasamakaki9 = _bentukDasar.SegitigaSamaKaki(titikAwal9, 70, 70);
        var segitigaRotated8 = RotatePoints(segitigasamakaki9, titikAwal9, 90);
        GraphicsUtils.PutPixelAll(this, segitigaRotated8, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(8));

        // berpasangan keluar 3
        var titikAwal10 = ScreenUtils.ToScreenCoordinate(35,90);
		var segitigasamakaki10 = _bentukDasar.SegitigaSamaKaki(titikAwal10, 70, 70);
        var segitigaRotated9 = RotatePoints(segitigasamakaki10, titikAwal10, 90);
        GraphicsUtils.PutPixelAll(this, segitigaRotated9, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(8));

        var titikAwal11 = ScreenUtils.ToScreenCoordinate(35, 20);
		var segitigasamakaki11 = _bentukDasar.SegitigaSamaKaki(titikAwal11, 70, 70);
        var segitigaRotated10 = RotatePoints(segitigasamakaki11, titikAwal11, 270);
        GraphicsUtils.PutPixelAll(this, segitigaRotated10, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(8));

        //segitiga 1 buah
        var titikAwal12 = ScreenUtils.ToScreenCoordinate(70, -50);
		var segitigasamakaki12 = _bentukDasar.SegitigaSamaKaki(titikAwal12, 70, 70);
        var segitigaRotated11 = RotatePoints(segitigasamakaki12, titikAwal12, 180);
        GraphicsUtils.PutPixelAll(this, segitigaRotated11, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(7));
    }

    // private void trapesium()
    // {
    //     var titikAwalTrapesiumSiku1 = new Vector2(35, 200);
	// 	var trapesiumSiku1 = _bentukDasar.TrapesiumSiku(titikAwalTrapesiumSiku1, 30, 50, 40);
    //     var trapesrotate1 = RotatePoints(trapesiumSiku1,titikAwalTrapesiumSiku1,90);
	// 	GraphicsUtils.PutPixelAll(this, trapesiumSiku1, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(6));
    // }

}