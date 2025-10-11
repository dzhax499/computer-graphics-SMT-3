namespace Godot;

using Godot;
using System;
using System.Collections.Generic;
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