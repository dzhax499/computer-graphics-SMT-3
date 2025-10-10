namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

// =============================================================
// CLASS Bunga — Representasi OOP & parametrik satu bunga
// =============================================================
public class Bunga
{
	private BentukDasar _bentukDasar = new BentukDasar();
	private Transformasi _transformasi = new Transformasi();

	public Vector2 Center { get; set; }
	public int RadiusPusat { get; set; }
	public int RadiusXKelopak { get; set; }
	public int RadiusYKelopak { get; set; }
	public int JumlahKelopak { get; set; }
	public Color WarnaKelopak { get; set; }
	public Color WarnaPusat { get; set; }

	public Bunga(Vector2 center, int radiusPusat, int radiusX, int radiusY, int jumlahKelopak, Color warnaKelopak, Color warnaPusat)
	{
		Center = center;
		RadiusPusat = radiusPusat;
		RadiusXKelopak = radiusX;
		RadiusYKelopak = radiusY;
		JumlahKelopak = jumlahKelopak;
		WarnaKelopak = warnaKelopak;
		WarnaPusat = warnaPusat;
	}

	public void Gambar(Node2D canvas)
	{
		// Gambar lingkaran pusat
		var lingkaran = _bentukDasar.Lingkaran(Center, RadiusPusat);
		GraphicsUtils.PutPixelAll(canvas, lingkaran, GraphicsUtils.DrawStyle.CircleDot, WarnaPusat);

		// Satu kelopak (elips di atas pusat)
		var kelopak = _bentukDasar.Elips(Center + new Vector2(0, -RadiusPusat - RadiusYKelopak / 2),
			RadiusXKelopak, RadiusYKelopak);

		// Rotasi kelopak mengelilingi pusat
		float sudutKelopak = 360f / JumlahKelopak;
		for (int i = 0; i < JumlahKelopak; i++)
		{
			var kelopakRotasi = TransformKelopak(kelopak, Center, sudutKelopak * i);
			GraphicsUtils.PutPixelAll(canvas, kelopakRotasi, GraphicsUtils.DrawStyle.EllipseDot, WarnaKelopak);
		}
	}

	private List<Vector2> TransformKelopak(List<Vector2> kelopak, Vector2 pivot, float angleDeg)
	{
		var hasil = new List<Vector2>();
		float angleRad = Mathf.DegToRad(angleDeg);

		foreach (var p in kelopak)
		{
			var v = p - pivot;
			var vRot = new Vector2(
				v.X * Mathf.Cos(angleRad) - v.Y * Mathf.Sin(angleRad),
				v.X * Mathf.Sin(angleRad) + v.Y * Mathf.Cos(angleRad)
			);
			hasil.Add(vRot + pivot);
		}
		return hasil;
	}
}

// =============================================================
// CLASS Karya2 — Main Scene
// =============================================================
public partial class Karya2 : Node2D
{
	public override void _Ready()
	{
		ScreenUtils.Initialize(GetViewport());
		QueueRedraw();
	}

	public override void _Draw()
	{
		// ======================
		// Bunga 4 Kelopak
		// ======================
		var bunga4Kelopak = new List<Vector2>
		{
			new(-300, 150), new(-100, 150), new(100, 150), new(300, 150)
		};

		foreach (var pos in bunga4Kelopak)
		{
			var bunga = new Bunga(pos, 25, 30, 40, 4, Colors.Yellow, Colors.Purple);
			bunga.Gambar(this);
		}

		// ======================
		// Bunga 8 Kelopak
		// ======================
		var bunga8Kelopak = new List<Vector2>
		{
			new(-500, -150), new(-300, -150), new(-100, -150),
			new(100, -150), new(300, -150), new(500, -150)
		};

		foreach (var pos in bunga8Kelopak)
		{
			var bunga = new Bunga(pos, 25, 25, 15, 8, Colors.Pink, Colors.Cyan);
			bunga.Gambar(this);
		}
	}
}
