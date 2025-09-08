namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

public partial class Quiz_3 : Node2D
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
        jajargenjang();

    }

    private void jajargenjang()
    {
        var titikAwalJajar1 = ScreenUtils.ToScreenCoordinate(-100,100);
        var jajargenj1 = _primitif.JajarGenjang(titikAwalJajar1, 40,70,100);
        var rotatejajargenj1 = RotatePoints(jajargenj1, titikAwalJajar1, 180);
        GraphicsUtils.PutPixelAll(this, rotatejajargenj1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(7));

        var titikAwalTrapesiumSiku1 = new Vector2(250, 200); 
		var trapesiumSiku1 = _bentukDasar.TrapesiumSiku(titikAwalTrapesiumSiku1, 30, 50, 40);
		GraphicsUtils.PutPixelAll(this, trapesiumSiku1, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(6));

        var titikAwalTrapesiumSiku2 = new Vector2(250, 200); 
		var trapesiumSiku2 = _bentukDasar.TrapesiumSiku(titikAwalTrapesiumSiku2, 30, 50, 40);
		GraphicsUtils.PutPixelAll(this, trapesiumSiku2, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(6));
    }

    private void MarginPixel()
    {
        var margin = _bentukDasar.Margin();
        GraphicsUtils.PutPixelAll(this, margin, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(6));
    }

    public override void _ExitTree()
    {
        base._ExitTree();
    }
}