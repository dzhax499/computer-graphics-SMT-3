namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

public partial class BentukDasar: RefCounted, IDisposable
{
	private Primitif _primitif = new Primitif();
	
	public List<Vector2> Margin(){
		return CheckPrimitifAndCall(() => _primitif.Margin());
	}    
	public List<Vector2> Persegi(float x, float y, float ukuran)
	{
		return CheckPrimitifAndCall(() => _primitif.Persegi(x, y, ukuran));
	}

	public List<Vector2> PersegiPanjang(float x, float y, float panjang, float lebar)
	{
		return CheckPrimitifAndCall(() => _primitif.PersegiPanjang(x, y, panjang, lebar));
	}


	public List<Vector2> SegitigaSiku(Vector2 titikAwal, int alas, int tinggi)
	{
		return CheckPrimitifAndCall(() => _primitif.SegitigaSiku(titikAwal, alas, tinggi));
	}

	public List<Vector2> TrapesiumSiku(Vector2 titikAwal, int panjangAtas, int panjangBawah, int tinggi)
	{
		return CheckPrimitifAndCall(() => _primitif.TrapesiumSiku(titikAwal, panjangAtas, panjangBawah, tinggi));
	}

	public List<Vector2> TrapesiumSamaKaki(Vector2 titikAwal, int panjangAtas, int panjangBawah, int tinggi)
	{
		return CheckPrimitifAndCall(() => _primitif.TrapesiumSamaKaki(titikAwal, panjangAtas, panjangBawah, tinggi));
	}

	public List<Vector2> JajarGenjang(Vector2 titikAwal, int alas, int tinggi, int jarakBeda)
	{
		return CheckPrimitifAndCall(() => _primitif.JajarGenjang(titikAwal, alas, tinggi, jarakBeda));
	}

		// New Circle and Ellipse Methods
	public List<Vector2> Lingkaran(Vector2 titikAwal, int radius)
	{
		return CheckPrimitifAndCall(() => _primitif.CircleMidPoint((int)titikAwal.X, (int)titikAwal.Y, radius));
	}

	public List<Vector2> Elips(Vector2 titikAwal, int radiusX, int radiusY)
	{
		return CheckPrimitifAndCall(() => _primitif.EllipseMidpoint((int)titikAwal.X, (int)titikAwal.Y, radiusX, radiusY));
	}

	private List<Vector2> CheckPrimitifAndCall(Func<List<Vector2>> action)
	{
		List<Vector2> checkResult = NodeUtils.CheckPrimitif(_primitif);
		if (checkResult != null) return checkResult;
		return action();
	}

	public List<Vector2> Polygon(List<Vector2> points)
	{
		List<Vector2> checkResult = NodeUtils.CheckPrimitif(_primitif);
		if (checkResult != null) return checkResult;

		List<Vector2> polygonPoints = new List<Vector2>();
		for (int i = 0; i < points.Count; i++)
		{
			int nextIndex = (i + 1) % points.Count; // Wrap around to the first point for the last line
			polygonPoints.AddRange(_primitif.LineBresenham(points[i].X, points[i].Y, points[nextIndex].X, points[nextIndex].Y));
		}
		return polygonPoints;
	}

	public new void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected new virtual void Dispose(bool disposing) // Fungsi Dispose() yang sebenarnya
	{
		if (disposing)
		{
			NodeUtils.DisposeAndNull(_primitif, "_primitif");
		}
	}
	// TASK 1 | MODUL 2 : MENAMBAHKAN FUNGSI EKSPONEN & LIMIT
	public List<Vector2> FungsiAbsolut(float titikAwalx, float titikAkhirx, float step = 1f)
	{
		List<Vector2> res = new List<Vector2>();
		for (float x = titikAwalx; x <= titikAkhirx; x = x + step)
		{
			float y = Math.Abs(x - 30); // f(x) = |x - 30|
			Vector2 screenPoint = _primitif.ToScreenCoordinate(x, y);
			res.Add(screenPoint);
		}
		return res;
	}

	public List<Vector2> FungsiEksponensial(float titikAwalx, float titikAkhirx, float step = 0.5f)
	{
		List<Vector2> res = new List<Vector2>();
		for (float x = titikAwalx; x <= titikAkhirx; x = x + step)
		{
			float y = (float)Math.Pow(2, x / 10); // f(x) = 2^(x/10)
			Vector2 screenPoint = _primitif.ToScreenCoordinate(x, y);
			res.Add(screenPoint);
		}
		return res;
	}

	public List<Vector2> FungsiLogaritma(float titikAwalx, float titikAkhirx, float step = 1f)
	{
		List<Vector2> res = new List<Vector2>();
		for (float x = titikAwalx; x <= titikAkhirx; x = x + step)
		{
			if (x > 0) // Logaritma hanya untuk x > 0
			{
				float y = (float)Math.Log(x) * 10; // f(x) = ln(x) * 10
				Vector2 screenPoint = _primitif.ToScreenCoordinate(x, y);
				res.Add(screenPoint);
			}
		}
		return res;
	}
	public List<Vector2> SumbuX(float panjang = 400)
	{
		List<Vector2> res = new List<Vector2>();
		// Sumbu X horizontal dari kiri ke kanan
		float startX = -panjang / 2;
		float endX = panjang / 2;
		
		for (float x = startX; x <= endX; x += 1f)
		{
			Vector2 point = _primitif.ToScreenCoordinate(x, 0); // y = 0 untuk sumbu X
			res.Add(point);
		}
		return res;
	}

	public List<Vector2> SumbuY(float tinggi = 300)
	{
		List<Vector2> res = new List<Vector2>();
		// Sumbu Y vertikal dari bawah ke atas
		float startY = -tinggi / 2;
		float endY = tinggi / 2;
		
		for (float y = startY; y <= endY; y += 1f)
		{
			Vector2 point = _primitif.ToScreenCoordinate(0, y); // x = 0 untuk sumbu Y
			res.Add(point);
		}
		return res;
	}

	public List<Vector2> GridLines(float step = 50, float maxRange = 200)
	{
		List<Vector2> res = new List<Vector2>();
		
		// Grid vertikal (garis sejajar sumbu Y)
		for (float x = -maxRange; x <= maxRange; x += step)
		{
			if (x != 0) // Skip sumbu utama
			{
				for (float y = -maxRange; y <= maxRange; y += 10f)
				{
					Vector2 point = _primitif.ToScreenCoordinate(x, y);
					res.Add(point);
				}
			}
		}
		
		// Grid horizontal (garis sejajar sumbu X)
		for (float y = -maxRange; y <= maxRange; y += step)
		{
			if (y != 0) // Skip sumbu utama
			{
				for (float x = -maxRange; x <= maxRange; x += 10f)
				{
					Vector2 point = _primitif.ToScreenCoordinate(x, y);
					res.Add(point);
				}
			}
		}
		
		return res;
	}

}
