using Godot;
using System;
using System.Collections.Generic;

public class Flower
{
    public Vector2 Center { get; private set; }
    public int CenterRadius { get; private set; }
    public int PetalCount { get; private set; }
    public int PetalRx { get; private set; }
    public int PetalRy { get; private set; }
    public Color PetalColor { get; private set; }
    public Color CenterColor { get; private set; }
    private Transformasi TransformUtil;

    public Flower(Vector2 center, int centerRadius, int petalCount, int petalRx, int petalRy, Color petalColor, Color centerColor, Transformasi transformUtil)
    {
        Center = center;
        CenterRadius = centerRadius;
        PetalCount = Mathf.Max(1, petalCount);
        PetalRx = petalRx;
        PetalRy = petalRy;
        PetalColor = petalColor;
        CenterColor = centerColor;
        TransformUtil = transformUtil;
    }

    public void DrawCenter(BentukDasar bd, Node2D target)
    {
        var circle = bd.Lingkaran(Center, CenterRadius);
        GraphicsUtils.PutPixelAll(target, circle, GraphicsUtils.DrawStyle.CircleStrip, CenterColor);
    }

    public void DrawInitialPetal(BentukDasar bd, Node2D target)
    {
        // Letakkan petal awal di bawah pusat (orientasi 0 = mengarah ke bawah)
        Vector2 petalCenter = new Vector2(Center.X, Center.Y - (CenterRadius + PetalRy - 4));
        // Pastikan memanggil bd.Elips (method milik BentukDasar)
        List<Vector2> petalPoints = bd.Elips(petalCenter, PetalRx, PetalRy);
        GraphicsUtils.PutPixelAll(target, petalPoints, GraphicsUtils.DrawStyle.EllipseStrip, PetalColor);
    }
    public void DrawAllPetals(BentukDasar bd, Node2D target)
    {
        // Titik awal kelopak (bawah pusat, world space)
        Vector2 initialPetalCenter = new Vector2(Center.X, Center.Y + (CenterRadius + PetalRy + 10));

        for (int i = 0; i < PetalCount; i++)
        {
            float angle = i * (360f / PetalCount);

            // === 1. Rotasi pusat kelopak mengelilingi bunga ===
            float[,] matrixRotateCenter = new float[3, 3];
            Transformasi.Matrix3x3Identity(matrixRotateCenter);
            TransformUtil.RotationCounterClockwise(matrixRotateCenter, angle, Center);

            var centerList = new List<Vector2>() { initialPetalCenter };
            var rotatedCenterList = TransformUtil.GetTransformPoint(matrixRotateCenter, centerList);
            Vector2 rotatedCenter = rotatedCenterList[0];

            // === 2. Buat elips di koordinat world ===
            List<Vector2> petalWorld = new List<Vector2>();
            for (int deg = 0; deg < 360; deg += 3)
            {
                float rad = Mathf.DegToRad(deg);
                float x = rotatedCenter.X + PetalRx * Mathf.Cos(rad);
                float y = rotatedCenter.Y + PetalRy * Mathf.Sin(rad);
                petalWorld.Add(new Vector2(x, y));
            }

            // === 3. Rotasi bentuk elips-nya terhadap pusat kelopak ===
            float shapeAngle = angle + 90f; // offset supaya orientasi dasar vertikal
            float[,] matrixPetal = new float[3, 3];
            Transformasi.Matrix3x3Identity(matrixPetal);
            // gunakan Clockwise agar arah rotasi bentuk sesuai arah radial visual
            TransformUtil.RotationClockwise(matrixPetal, -shapeAngle, rotatedCenter);

            List<Vector2> rotatedPetalWorld = TransformUtil.GetTransformPoint(matrixPetal, petalWorld);


            // === 4. Ubah ke koordinat layar (screen space) ===
            List<Vector2> petalScreen = new List<Vector2>();
            foreach (var p in rotatedPetalWorld)
                petalScreen.Add(ScreenUtils.ToScreenCoordinate(p.X, p.Y));

            // === 5. Gambar ===
            GraphicsUtils.PutPixelAll(target, petalScreen, GraphicsUtils.DrawStyle.EllipseStrip, PetalColor);
        }
    }
}