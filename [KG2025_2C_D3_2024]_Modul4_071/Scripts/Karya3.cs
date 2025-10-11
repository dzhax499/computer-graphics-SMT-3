namespace Godot;

using System;
using System.Collections.Generic;
using System.Linq;

public partial class Karya3 : Node2D
{
    private BentukDasar _bentukDasar = new BentukDasar();
    private Transformasi _transformasi = new Transformasi();

    private struct GambarBentuk
    {
        public string Name;
        public Action Draw;
        public GambarBentuk(string name, Action draw)
        {
            Name = name;
            Draw = draw;
        }
    }

    private int _index = 0;
    private readonly List<GambarBentuk> _gambarList = new();

    [Export] private Label _label;

    public override void _Ready()
    {
        if (_label == null)
            _label = GetNode<Label>("Label");
        ScreenUtils.Initialize(GetViewport());

        _gambarList.Add(new GambarBentuk("Candy", DrawCandy));
        _gambarList.Add(new GambarBentuk("Penguin", DrawPenguin));
        _gambarList.Add(new GambarBentuk("Plane", DrawPlane));

        QueueRedraw();
    }

    public void _on_prev_btn_pressed()
    {
        _index = (_index - 1 + _gambarList.Count) % _gambarList.Count;
        QueueRedraw();
    }

    public void _on_next_btn_pressed()
    {
        _index = (_index + 1) % _gambarList.Count;
        QueueRedraw();
    }

    public override void _Draw()
    {
        _label.Text = _gambarList[_index].Name;
        _gambarList[_index].Draw();
    }

    // =========================
    // BENTUK: CANDY
    // =========================
    private List<Vector2> ToScreenPolygon(List<Vector2> points)
    {
        var result = new List<Vector2>();
        foreach (var p in points)
            result.Add(ScreenUtils.ToScreenCoordinate(p.X, p.Y));
        return result;
    }

    private void DrawCandy()
    {
        Vector2 pivot = new(0, 0); // pusat rotasi
        float[,] matrix = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrix);

        // Terapkan rotasi 30 derajat searah jarum jam
        _transformasi.RotationClockwise(matrix, 30, pivot);

        // ===== Segitiga kiri =====
        var segitigaKiri = _bentukDasar.Polygon(new List<Vector2> {
        new(-350, 67),
        new(-350, -67),
        new(-250, 0)
    });
        var segitigaKiriT = _transformasi.GetTransformPoint(matrix, segitigaKiri);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(segitigaKiriT), GraphicsUtils.DrawStyle.DotDot, Colors.Orange);

        // ===== Belah ketupat hijau =====
        var diamond = _bentukDasar.Polygon(new List<Vector2> {
        new(-250, 0),
        new(-200, 100),
        new(-150, 0),
        new(-200, -100)
    });
        var diamondT = _transformasi.GetTransformPoint(matrix, diamond);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(diamondT), GraphicsUtils.DrawStyle.DotDot, Colors.Green);

        // ===== Segitiga atas =====
        var segitigaAtas = _bentukDasar.Polygon(new List<Vector2> {
        new(-200, 100),
        new(-150, 0),
        new(-100, 100)
    });
        var segitigaAtasT = _transformasi.GetTransformPoint(matrix, segitigaAtas);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(segitigaAtasT), GraphicsUtils.DrawStyle.DotDot, Colors.Orange);

        // ===== Segitiga bawah =====
        var segitigaBawah = _bentukDasar.Polygon(new List<Vector2> {
        new(-200, -100),
        new(-150, 0),
        new(-100, -100)
    });
        var segitigaBawahT = _transformasi.GetTransformPoint(matrix, segitigaBawah);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(segitigaBawahT), GraphicsUtils.DrawStyle.DotDot, Colors.Orange);

        // ===== Hexagon merah (tengah) =====
        var hexagon = _bentukDasar.Polygon(new List<Vector2> {
        new(-100, 100),
        new(-150, 0),
        new(-100, -100),
        new(20, -100),
        new(70, 0),
        new(20, 100)
    });
        var hexagonT = _transformasi.GetTransformPoint(matrix, hexagon);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(hexagonT), GraphicsUtils.DrawStyle.DotDot, Colors.Red);

        // ===== Segitiga kanan =====
        var segitigaKanan = _bentukDasar.Polygon(new List<Vector2> {
        new(170, 67),
        new(170, -67),
        new(70, 0)
    });
        var segitigaKananT = _transformasi.GetTransformPoint(matrix, segitigaKanan);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(segitigaKananT), GraphicsUtils.DrawStyle.DotDot, Colors.Orange);
    }

    // =========================
    // BENTUK: PENGUIN
    // =========================
    private void DrawPenguin()
    {
        Vector2 titikAwal = new(-50, 0);
        float[,] matrix = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrix);

        // Transformasi global
        _transformasi.ReflectionToX(matrix, ref titikAwal);
        _transformasi.Translation(matrix, 0, 150, ref titikAwal);

        // ===== Jajar genjang atas =====
        var jajarGenjang1 = _bentukDasar.JajarGenjang(titikAwal, 100, 70, 40);

        var jajarGenjang1T = _transformasi.GetTransformPoint(matrix, jajarGenjang1);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(jajarGenjang1T), GraphicsUtils.DrawStyle.DotDot, Colors.Green);

        // ===== Hexagon 1 =====
        var hexagon1 = _bentukDasar.Polygon(new List<Vector2> {
        new (titikAwal.X, titikAwal.Y),
        new (titikAwal.X + 100, titikAwal.Y),
        new (titikAwal.X + 140, titikAwal.Y + 70),
        new (titikAwal.X + 100, titikAwal.Y + 140),
        new (titikAwal.X, titikAwal.Y + 140),
        new (titikAwal.X - 40, titikAwal.Y + 70),
    });
        var hexagon1T = _transformasi.GetTransformPoint(matrix, hexagon1);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(hexagon1T), GraphicsUtils.DrawStyle.DotDot, Colors.Red);

        // ===== Hexagon 2 =====
        var hexagon2 = _bentukDasar.Polygon(new List<Vector2> {
        new (titikAwal.X, titikAwal.Y + 140),
        new (titikAwal.X + 100, titikAwal.Y + 140),
        new (titikAwal.X + 140, titikAwal.Y + 70 + 140),
        new (titikAwal.X + 100, titikAwal.Y + 140 + 140),
        new (titikAwal.X, titikAwal.Y + 140 + 140),
        new (titikAwal.X - 40, titikAwal.Y + 70 + 140),
    });
        var hexagon2T = _transformasi.GetTransformPoint(matrix, hexagon2);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(hexagon2T), GraphicsUtils.DrawStyle.DotDot, Colors.Red);

        // ===== Diamond kiri atas =====
        var diamond1 = _bentukDasar.Polygon(new List<Vector2> {
        new (titikAwal.X, titikAwal.Y + 140),
        new (titikAwal.X - 40, titikAwal.Y + 70),
        new (titikAwal.X - 80, titikAwal.Y + 140),
        new (titikAwal.X - 40, titikAwal.Y + 70 + 140),
    });
        var diamond1T = _transformasi.GetTransformPoint(matrix, diamond1);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(diamond1T), GraphicsUtils.DrawStyle.DotDot, Colors.Green);

        // ===== Diamond kanan atas =====
        var diamond2 = _bentukDasar.Polygon(new List<Vector2> {
        new (titikAwal.X + 140, titikAwal.Y + 70),
        new (titikAwal.X + 100, titikAwal.Y + 140),
        new (titikAwal.X + 140, titikAwal.Y + 70 + 140),
        new (titikAwal.X + 180, titikAwal.Y + 140),
    });
        var diamond2T = _transformasi.GetTransformPoint(matrix, diamond2);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(diamond2T), GraphicsUtils.DrawStyle.DotDot, Colors.Green);

        // ===== Diamond kiri bawah =====
        var diamond3 = _bentukDasar.Polygon(new List<Vector2> {
        new (titikAwal.X, titikAwal.Y + 140 + 140),
        new (titikAwal.X - 40, titikAwal.Y + 70 + 140),
        new (titikAwal.X - 80, titikAwal.Y + 140 + 140),
        new (titikAwal.X - 40, titikAwal.Y + 70 + 140 + 140),
    });
        var diamond3T = _transformasi.GetTransformPoint(matrix, diamond3);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(diamond3T), GraphicsUtils.DrawStyle.DotDot, Colors.Green);

        // ===== Diamond kanan bawah =====
        var diamond4 = _bentukDasar.Polygon(new List<Vector2> {
        new (titikAwal.X + 140, titikAwal.Y + 70 + 140),
        new (titikAwal.X + 100, titikAwal.Y + 140 + 140),
        new (titikAwal.X + 140, titikAwal.Y + 70 + 140 + 140),
        new (titikAwal.X + 180, titikAwal.Y + 140 + 140),
    });
        var diamond4T = _transformasi.GetTransformPoint(matrix, diamond4);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(diamond4T), GraphicsUtils.DrawStyle.DotDot, Colors.Green);

        // ===== Lengan kiri =====
        var leftHand = _bentukDasar.Polygon(new List<Vector2> {
        new (titikAwal.X - 40, titikAwal.Y + 70),
        new (titikAwal.X - 80, titikAwal.Y + 140),
        new (titikAwal.X - 150, titikAwal.Y + 140 + 50),
        new (titikAwal.X - 110, titikAwal.Y + 110),
    });
        var leftHandT = _transformasi.GetTransformPoint(matrix, leftHand);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(leftHandT), GraphicsUtils.DrawStyle.DotDot, Colors.DarkCyan);

        // ===== Lengan kanan =====
        var rightHand = _bentukDasar.Polygon(new List<Vector2> {
        new (titikAwal.X + 100 + 40, titikAwal.Y + 70),
        new (titikAwal.X + 100 + 80, titikAwal.Y + 140),
        new (titikAwal.X + 100 + 150, titikAwal.Y + 140 + 50),
        new (titikAwal.X + 100 + 110, titikAwal.Y + 110),
    });
        var rightHandT = _transformasi.GetTransformPoint(matrix, rightHand);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(rightHandT), GraphicsUtils.DrawStyle.DotDot, Colors.DarkCyan);
    }


    // =========================
    // BENTUK: PLANE
    // =========================
    // ===== Helpers (versi 3x3) =====
    private void DrawJJ3x3(float[,] m, Vector2 start, int alas, int tinggi, int jarakBeda, Color warna)
    {
        var jj = _bentukDasar.JajarGenjang(start, alas, tinggi, jarakBeda);
        var jjT = _transformasi.GetTransformPoint(m, jj);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(jjT), GraphicsUtils.DrawStyle.DotDot, warna);
    }

    private void DrawTrap3x3(float[,] m, Vector2 start, int panjangAtas, int panjangBawah, int tinggi, Color warna)
    {
        var trap = _bentukDasar.TrapesiumSamaKaki(start, panjangAtas, panjangBawah, tinggi);
        var trapT = _transformasi.GetTransformPoint(m, trap);
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(trapT), GraphicsUtils.DrawStyle.DotDot, warna);
    }
    private void DrawPlane()
    {
        Vector2 titikAwal = new(-500, -50);
        Vector2 pivot = new(0, 0);

        float[,] matrix = new float[3, 3];
        Transformasi.Matrix3x3Identity(matrix);

        // === TRANSFORMASI GLOBAL (derajat, bukan radian) ===
        _transformasi.ReflectionToX(matrix, ref pivot);
        _transformasi.RotationClockwise(matrix, 30, pivot);
        _transformasi.Scaling(matrix, 0.8f, 0.8f, pivot);
        _transformasi.Translation(matrix, 0, -100, ref pivot);

        // === KEPALA (trapesium) ===
        var trapesium1 = _transformasi.GetTransformPoint(
            matrix,
            _bentukDasar.TrapesiumSamaKaki(titikAwal, 120, 200, 100)
        );
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(trapesium1), GraphicsUtils.DrawStyle.DotDot, Colors.Yellow);

        // === BADAN (jajargenjang tengah) ===
        var jajarGenjang1 = _transformasi.GetTransformPoint(
            matrix, _bentukDasar.JajarGenjang(new Vector2(titikAwal.X + 200, titikAwal.Y), 120, 100, -40)
        );
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(jajarGenjang1), GraphicsUtils.DrawStyle.DotDot, Colors.Green);

        var jajarGenjang2 = _transformasi.GetTransformPoint(
            matrix, _bentukDasar.JajarGenjang(new Vector2(titikAwal.X + 200 + 120, titikAwal.Y), 120, 100, -40)
        );
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(jajarGenjang2), GraphicsUtils.DrawStyle.DotDot, Colors.Green);

        var jajarGenjang3 = _transformasi.GetTransformPoint(
            matrix, _bentukDasar.JajarGenjang(new Vector2(titikAwal.X + 200 + 240, titikAwal.Y), 120, 100, -40)
        );
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(jajarGenjang3), GraphicsUtils.DrawStyle.DotDot, Colors.Green);

        // === EKOR (polygon) ===
        var ekor = _transformasi.GetTransformPoint(
            matrix,
            _bentukDasar.Polygon(new List<Vector2> {
            new(titikAwal.X + 200 + 360,       titikAwal.Y),
            new(titikAwal.X + 200 + 360 - 40,  titikAwal.Y - 100),
            new(titikAwal.X + 200 + 360 + 60,  titikAwal.Y - 240),
            new(titikAwal.X + 200 + 360 + 170, titikAwal.Y - 240),
            })
        );
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(ekor), GraphicsUtils.DrawStyle.DotDot, Colors.Yellow);

        // === SAYAP ATAS (trapesium pangkal) ===
        var trapesium2 = _transformasi.GetTransformPoint(
            matrix, _bentukDasar.TrapesiumSamaKaki(new Vector2(titikAwal.X + 200 - 40, titikAwal.Y - 100), 120, 200, 100)
        );
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(trapesium2), GraphicsUtils.DrawStyle.DotDot, Colors.Yellow);

        // === SAYAP BAWAH (trapesium pangkal) ===
        var trapesium3 = _transformasi.GetTransformPoint(
            matrix, _bentukDasar.TrapesiumSamaKaki(new Vector2(titikAwal.X + 200 - 40 + 40, titikAwal.Y + 100), 200, 120, 100)
        );
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(trapesium3), GraphicsUtils.DrawStyle.DotDot, Colors.Yellow);

        // === SAMBUNGAN SAYAP ATAS (2 jajargenjang) ===
        var jajarGenjang4 = _transformasi.GetTransformPoint(
            matrix, _bentukDasar.JajarGenjang(new Vector2(titikAwal.X + 200 - 40 + 40, titikAwal.Y - 100 - 100), 120, 100, 40)
        );
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(jajarGenjang4), GraphicsUtils.DrawStyle.DotDot, Colors.Green);

        var jajarGenjang5 = _transformasi.GetTransformPoint(
            matrix, _bentukDasar.JajarGenjang(new Vector2(titikAwal.X + 200 - 40 + 40 + 40, titikAwal.Y - 100 - 100 - 100), 120, 100, 40)
        );
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(jajarGenjang5), GraphicsUtils.DrawStyle.DotDot, Colors.Green);

        // === SAMBUNGAN SAYAP BAWAH (2 jajargenjang) ===
        var jajarGenjang6 = _transformasi.GetTransformPoint(
            matrix, _bentukDasar.JajarGenjang(new Vector2(titikAwal.X + 200 - 40 + 40 + 40, titikAwal.Y + 100 + 100), 120, 100, -40)
        );
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(jajarGenjang6), GraphicsUtils.DrawStyle.DotDot, Colors.Green);

        var jajarGenjang7 = _transformasi.GetTransformPoint(
            matrix, _bentukDasar.JajarGenjang(new Vector2(titikAwal.X + 200 - 40 + 40 + 40 + 40, titikAwal.Y + 100 + 100 + 100), 120, 100, -40)
        );
        GraphicsUtils.PutPixelAll(this, ToScreenPolygon(jajarGenjang7), GraphicsUtils.DrawStyle.DotDot, Colors.Green);
    }





    public override void _ExitTree()
    {
        NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
        NodeUtils.DisposeAndNull(_transformasi, "_transformasi");
        base._ExitTree();
    }
}
