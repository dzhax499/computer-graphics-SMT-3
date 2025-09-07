namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

public partial class Primitif: RefCounted
{
	// MODUL 2 - LINE DRAWING ALGORITHMS
	public List<Vector2> LineDDA(float xa, float ya, float xb, float yb)
	{
		float dx = xb - xa;
		float dy = yb - ya;
		float steps;
		float xIncrement;
		float yIncrement;
		float x = xa;
		float y = ya;

		List<Vector2> res = new List<Vector2>();

		if (Mathf.Abs(dx) > Mathf.Abs(dy))
		{
			steps = Mathf.Abs(dx);
		}
		else
		{
			steps = Mathf.Abs(dy);
		}

		xIncrement = dx / steps;
		yIncrement = dy / steps;

		res.Add(new Vector2(Mathf.Round(x), Mathf.Round(y)));

		for (int k = 0; k < steps; k++)
		{
			x += xIncrement;
			y += yIncrement;
			res.Add(new Vector2(Mathf.Round(x), Mathf.Round(y))); 
		}

		return res;
	}

	public List<Vector2> LineBresenham(float xa, float ya, float xb, float yb)
	{
		List<Vector2> res = new List<Vector2>();
		int x1 = (int)xa;
		int y1 = (int)ya;
		int x2 = (int)xb;
		int y2 = (int)yb;

		int dx = Math.Abs(x2 - x1);
		int dy = Math.Abs(y2 - y1);
		int sx = (x1 < x2) ? 1 : -1;
		int sy = (y1 < y2) ? 1 : -1;
		int err = dx - dy;

		while (true)
		{
			res.Add(new Vector2(x1, y1));
			if (x1 == x2 && y1 == y2) break;
			int e2 = 2 * err;
			if (e2 > -dy) { err -= dy; x1 += sx; }
			if (e2 < dx) { err += dx; y1 += sy; }
		}
		return res;
	}

	public List<Vector2> Margin()
	{
		List<Vector2> res = new List<Vector2>();
		res.AddRange(LineBresenham(ScreenUtils.MarginLeft, ScreenUtils.MarginTop, ScreenUtils.MarginRight, ScreenUtils.MarginTop));
		res.AddRange(LineBresenham(ScreenUtils.MarginLeft, ScreenUtils.MarginBottom, ScreenUtils.MarginRight, ScreenUtils.MarginBottom));
		res.AddRange(LineBresenham(ScreenUtils.MarginLeft, ScreenUtils.MarginTop, ScreenUtils.MarginLeft, ScreenUtils.MarginBottom));
		res.AddRange(LineBresenham(ScreenUtils.MarginRight, ScreenUtils.MarginTop, ScreenUtils.MarginRight, ScreenUtils.MarginBottom));
		return res;
	}
	// KOORDINAT CONVERSION FUNCTIONS

    /// Konversi dari koordinat kartesian ke screen coordinate
    public Vector2 ToScreenCoordinate(float x, float y)
    {
        float screenX = ScreenUtils.MarginLeft + x;
        float screenY = ScreenUtils.MarginBottom - y;
        return new Vector2(screenX, screenY);
    }

    /// Konversi dari screen coordinate ke koordinat kartesian
    public Vector2 ToWorldCoordinate(float screenX, float screenY)
    {
        float worldX = screenX - ScreenUtils.MarginLeft;
        float worldY = ScreenUtils.MarginBottom - screenY;
        return new Vector2(worldX, worldY);
    }


	public List<Vector2> Persegi(float x, float y, float ukuran)
	{
		List<Vector2> res = new List<Vector2>();
		res.AddRange(LineBresenham(x, y, x + ukuran, y));
		res.AddRange(LineBresenham(x + ukuran, y + 1, x + ukuran, y + ukuran)); // Start from y + 1
		res.AddRange(LineBresenham(x + ukuran - 1, y + ukuran, x, y + ukuran)); // Start from x + ukuran - 1
		res.AddRange(LineBresenham(x, y + ukuran - 1, x, y + 1)); // Start from y + ukuran - 1 and end at y + 1
		return res;
	}

	public List<Vector2> PersegiPanjang(float x, float y, float panjang, float lebar)
	{
		List<Vector2> res = new List<Vector2>();
		res.AddRange(LineBresenham(x, y, x + panjang, y));
		res.AddRange(LineBresenham(x + panjang, y + 1, x + panjang, y + lebar)); // Start from y + 1
		res.AddRange(LineBresenham(x + panjang - 2, y + lebar, x, y + lebar)); // Start from x + panjang - 1
		res.AddRange(LineBresenham(x, y + lebar - 1, x, y + 1));
		return res;
	}

	public List<Vector2> SegitigaSiku(Vector2 titikAwal, int alas, int tinggi)
	{
		List<Vector2> res = new List<Vector2>();
		res.AddRange(LineBresenham(titikAwal.X, titikAwal.Y, titikAwal.X + alas, titikAwal.Y)); // Alas
		res.AddRange(LineBresenham(titikAwal.X, titikAwal.Y, titikAwal.X, titikAwal.Y - tinggi)); // Tinggi
		res.AddRange(LineBresenham(titikAwal.X, titikAwal.Y - tinggi, titikAwal.X + alas, titikAwal.Y)); // Miring
		return res;
	}

	public List<Vector2> TrapesiumSiku(Vector2 titikAwal, int panjangAtas, int panjangBawah, int tinggi)
	{
		List<Vector2> points = new List<Vector2>();
        
        Vector2 p1 = ToScreenCoordinate(titikAwal.X, titikAwal.Y); // Kiri bawah
        Vector2 p2 = ToScreenCoordinate(titikAwal.X + panjangBawah, titikAwal.Y); // Kanan bawah
        Vector2 p3 = ToScreenCoordinate(titikAwal.X + panjangAtas, titikAwal.Y + tinggi); // Kanan atas
        Vector2 p4 = ToScreenCoordinate(titikAwal.X, titikAwal.Y + tinggi); // Kiri atas
        
        points.AddRange(LineBresenham(p1.X, p1.Y, p2.X, p2.Y)); // Alas
        points.AddRange(LineBresenham(p2.X, p2.Y, p3.X, p3.Y)); // Sisi kanan miring
        points.AddRange(LineBresenham(p3.X, p3.Y, p4.X, p4.Y)); // Atas
        points.AddRange(LineBresenham(p4.X, p4.Y, p1.X, p1.Y)); // Sisi kiri
        
        return points;
	}

	public List<Vector2> TrapesiumSamaKaki(Vector2 titikAwal, int panjangAtas, int panjangBawah, int tinggi)
	{
		List<Vector2> res = new List<Vector2>();
		return res;
	}

	public List<Vector2> JajarGenjang(Vector2 titikAwal, int alas, int tinggi, int jarakBeda)
	{
		List<Vector2> points = new List<Vector2>();
		
		Vector2 p1 = ToScreenCoordinate(titikAwal.X, titikAwal.Y); // Kiri bawah
		Vector2 p2 = ToScreenCoordinate(titikAwal.X + alas, titikAwal.Y); // Kanan bawah
		Vector2 p3 = ToScreenCoordinate(titikAwal.X + alas + jarakBeda, titikAwal.Y + tinggi); // Kanan atas
		Vector2 p4 = ToScreenCoordinate(titikAwal.X + jarakBeda, titikAwal.Y + tinggi); // Kiri atas
		
		points.AddRange(LineBresenham(p1.X, p1.Y, p2.X, p2.Y)); // Alas bawah
		points.AddRange(LineBresenham(p2.X, p2.Y, p3.X, p3.Y)); // Sisi kanan
		points.AddRange(LineBresenham(p3.X, p3.Y, p4.X, p4.Y)); // Alas atas
		points.AddRange(LineBresenham(p4.X, p4.Y, p1.X, p1.Y)); // Sisi kiri
		
		return points;
	}

	public List<Vector2> CircleMidPoint(int xCenter, int yCenter, int radius)
	{
		List<Vector2> points = new List<Vector2>();
		return points;
	}

	private void CirclePlotPoints(int xCenter, int yCenter, int x, int y, List<Vector2> points)
	{

	}

	public List<Vector2> EllipseMidpoint(int xCenter, int yCenter, int rx, int ry)
	{
		List<Vector2> points = new List<Vector2>();
		return points;
	}

	private void EllipsePlotPoints(int xCenter, int yCenter, int x, int y, List<Vector2> points)
	{

	}
	
}
