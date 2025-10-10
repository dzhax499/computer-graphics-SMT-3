namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

/*
  BungaCreator.cs (versi Transformasi)
  - Membuat dua tipe bunga:
    * Tipe 1: lingkaran pusat + 4 kelopak (ellips)
    * Tipe 2: lingkaran pusat + 8 kelopak (ellips)
  - Implementasi bertahap (stage):
    1) Gambar lingkaran pusat + 1 kelopak (syarat awal)
    2) Duplikasi pusat bunga: 4 bunga (baris atas, tipe-1), 6 bunga (baris bawah, tipe-2)
    3) Tambahkan kelopak lengkap (rotate via Transformasi)
*/

public partial class Karya2 : Node2D
{
	private BentukDasar _bentuk;
	private Transformasi _transform;

	public override void _Draw()
	{
		ScreenUtils.Initialize(GetViewport());
		_bentuk = new BentukDasar();
		_transform = new Transformasi();

		int centerRadiusType1 = 18;
		int centerRadiusType2 = 14;
		int petalRxType1 = 36;
		int petalRyType1 = 16;
		int petalRxType2 = 28;
		int petalRyType2 = 12;

		float topRowY = 100f;
		float bottomRowY = -100f;


		// Stage 1: Duplikasi pusat
		List<Flower> topRowFlowers = DuplicateFlowersRow(4, -210, topRowY, 150f, centerRadiusType1, 4, petalRxType1, petalRyType1, Colors.Orange, Colors.Red);
		List<Flower> bottomRowFlowers = DuplicateFlowersRow(6, -310, bottomRowY, 120f, centerRadiusType2, 8, petalRxType2, petalRyType2, Colors.Cyan, Colors.Blue);

		foreach (var f in topRowFlowers) f.DrawCenter(_bentuk, this);
		foreach (var f in bottomRowFlowers) f.DrawCenter(_bentuk, this);

		// Stage 2: Tambah kelopak (pakai Transformasi rotate)
		foreach (var f in topRowFlowers) f.DrawAllPetals(_bentuk, this);
		foreach (var f in bottomRowFlowers) f.DrawAllPetals(_bentuk, this);
	}

	private List<Flower> DuplicateFlowersRow(int count, float startX, float y, float spacing, int centerRadius, int petalCount, int petalRx, int petalRy, Color petalColor, Color centerColor)
	{
		List<Flower> res = new List<Flower>();
		float x = startX;
		for (int i = 0; i < count; i++)
		{
			Vector2 center = new Vector2(x, y);
			var f = new Flower(center, centerRadius, petalCount, petalRx, petalRy, petalColor, centerColor, _transform);
			res.Add(f);
			x += spacing;
		}
		return res;
	}
}

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
