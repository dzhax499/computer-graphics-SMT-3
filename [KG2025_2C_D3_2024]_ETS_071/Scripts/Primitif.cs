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

		Vector2 p1 = ScreenUtils.ToScreenCoordinate(titikAwal.X, titikAwal.Y);              // titik awal (kanan bawah)
		Vector2 p2 = ScreenUtils.ToScreenCoordinate(titikAwal.X + alas, titikAwal.Y);       // alas ke kanan
		Vector2 p3 = ScreenUtils.ToScreenCoordinate(titikAwal.X, titikAwal.Y + tinggi);     // tinggi ke atas

		res.AddRange(LineBresenham(p1.X, p1.Y, p2.X, p2.Y)); // alas
		res.AddRange(LineBresenham(p1.X, p1.Y, p3.X, p3.Y)); // tinggi
		res.AddRange(LineBresenham(p2.X, p2.Y, p3.X, p3.Y)); // sisi miring
		return res;
	}

	public List<Vector2> SegitigaSamaKaki(Vector2 titikAwal, int alas, int tinggi)
	{
		List<Vector2> points = new List<Vector2>();
		
		// Titik puncak di tengah atas
		float puncakX = titikAwal.X + alas / 2f;
		float puncakY = titikAwal.Y + tinggi;
		
		// Tiga sisi segitiga
		points.AddRange(LineBresenham(titikAwal.X, titikAwal.Y, titikAwal.X + alas, titikAwal.Y)); // Alas
		points.AddRange(LineBresenham(titikAwal.X, titikAwal.Y, puncakX, puncakY)); // Sisi kiri
		points.AddRange(LineBresenham(titikAwal.X + alas, titikAwal.Y, puncakX, puncakY)); // Sisi kanan
		
		return points;
	}


	public List<Vector2> TrapesiumSiku(Vector2 titikAwal, int panjangAtas, int panjangBawah, int tinggi)
	{
		List<Vector2> points = new List<Vector2>();
        
        Vector2 p1 = ScreenUtils.ToScreenCoordinate(titikAwal.X, titikAwal.Y); // Kiri bawah
        Vector2 p2 = ScreenUtils.ToScreenCoordinate(titikAwal.X + panjangBawah, titikAwal.Y); // Kanan bawah
        Vector2 p3 = ScreenUtils.ToScreenCoordinate(titikAwal.X + panjangAtas, titikAwal.Y + tinggi); // Kanan atas
        Vector2 p4 = ScreenUtils.ToScreenCoordinate(titikAwal.X, titikAwal.Y + tinggi); // Kiri atas
        
        points.AddRange(LineBresenham(p1.X, p1.Y, p2.X, p2.Y)); // Alas
        points.AddRange(LineBresenham(p2.X, p2.Y, p3.X, p3.Y)); // Sisi kanan miring
        points.AddRange(LineBresenham(p3.X, p3.Y, p4.X, p4.Y)); // Atas
        points.AddRange(LineBresenham(p4.X, p4.Y, p1.X, p1.Y)); // Sisi kiri
        
        return points;
	}

	public List<Vector2> TrapesiumSamaKaki(Vector2 titikAwal, int panjangAtas, int panjangBawah, int tinggi)
	{
		List<Vector2> points = new List<Vector2>();
        
        float offset = (panjangBawah - panjangAtas) / 2f;
        
        Vector2 p1 = ScreenUtils.ToScreenCoordinate(titikAwal.X, titikAwal.Y); // Kiri bawah
        Vector2 p2 = ScreenUtils.ToScreenCoordinate(titikAwal.X + panjangBawah, titikAwal.Y); // Kanan bawah
        Vector2 p3 = ScreenUtils.ToScreenCoordinate(titikAwal.X + offset + panjangAtas, titikAwal.Y + tinggi); // Kanan atas
        Vector2 p4 = ScreenUtils.ToScreenCoordinate(titikAwal.X + offset, titikAwal.Y + tinggi); // Kiri atas
        
        points.AddRange(LineBresenham(p1.X, p1.Y, p2.X, p2.Y)); // Alas
        points.AddRange(LineBresenham(p2.X, p2.Y, p3.X, p3.Y)); // Sisi kanan
        points.AddRange(LineBresenham(p3.X, p3.Y, p4.X, p4.Y)); // Atas
        points.AddRange(LineBresenham(p4.X, p4.Y, p1.X, p1.Y)); // Sisi kiri
        
        return points;
	}

	public List<Vector2> JajarGenjang(Vector2 titikAwal, int alas, int tinggi, int jarakBeda)
	{
		List<Vector2> points = new List<Vector2>();
		
		Vector2 p1 = ScreenUtils.ToScreenCoordinate(titikAwal.X, titikAwal.Y); // Kiri bawah
		Vector2 p2 = ScreenUtils.ToScreenCoordinate(titikAwal.X + alas, titikAwal.Y); // Kanan bawah
		Vector2 p3 = ScreenUtils.ToScreenCoordinate(titikAwal.X + alas + jarakBeda, titikAwal.Y + tinggi); // Kanan atas
		Vector2 p4 = ScreenUtils.ToScreenCoordinate(titikAwal.X + jarakBeda, titikAwal.Y + tinggi); // Kiri atas
		
		points.AddRange(LineBresenham(p1.X, p1.Y, p2.X, p2.Y)); // Alas bawah
		points.AddRange(LineBresenham(p2.X, p2.Y, p3.X, p3.Y)); // Sisi kanan
		points.AddRange(LineBresenham(p3.X, p3.Y, p4.X, p4.Y)); // Alas atas
		points.AddRange(LineBresenham(p4.X, p4.Y, p1.X, p1.Y)); // Sisi kiri
		
		return points;
	}

	public List<Vector2> CircleMidPoint(int xCenter, int yCenter, int radius)
	{
		List<Vector2> points = new List<Vector2>();
		int x = 0;
		int y = radius;
		int d = 1 -radius;

		//convert koordinat layar
		Vector2 centerScreen = ScreenUtils.ToScreenCoordinate(xCenter,yCenter);

		void PlotCirclePoints(int xc,int yc,int px, int py)
		{
			points.Add(ScreenUtils.ToScreenCoordinate(xc + px, yc + py));
			points.Add(ScreenUtils.ToScreenCoordinate(xc - px, yc + py));
			points.Add(ScreenUtils.ToScreenCoordinate(xc + px, yc - py));
			points.Add(ScreenUtils.ToScreenCoordinate(xc - px, yc - py));
			points.Add(ScreenUtils.ToScreenCoordinate(xc + py, yc + px));
			points.Add(ScreenUtils.ToScreenCoordinate(xc - py, yc + px));
			points.Add(ScreenUtils.ToScreenCoordinate(xc + py, yc - px));
			points.Add(ScreenUtils.ToScreenCoordinate(xc - py, yc - px));
		}

		PlotCirclePoints(xCenter,yCenter,x,y);

		while(x<y)
		{
			x++;
			if (d < 0)
			{
				d = d + 2 * x+1;
			}
			else 
			{
				y--;
				d = d + 2 * (x-y) + 1;
			}
			PlotCirclePoints(xCenter,yCenter,x,y);
		}

		return points;
	}

	public List<Vector2> EllipseMidpoint(int xCenter, int yCenter, int rx, int ry)
	{
		List<Vector2> points = new List<Vector2>();
		int x = 0;
		int y = ry;

		int rxSq = rx * rx;
		int rySq = ry * ry;
		int twoRxSq = 2 * rxSq;
		int twoRySq = 2 * rySq;

		int px = 0;
		int py = twoRxSq * y;

		// Konversi ke koordinat layar
		Vector2 centerScreen = ScreenUtils.ToScreenCoordinate(xCenter, yCenter);

		void PlotEllipsePoints(int xc, int yc, int px, int py)
		{
			points.Add(ScreenUtils.ToScreenCoordinate(xc + px, yc + py));
			points.Add(ScreenUtils.ToScreenCoordinate(xc - px, yc + py));
			points.Add(ScreenUtils.ToScreenCoordinate(xc + px, yc - py));
			points.Add(ScreenUtils.ToScreenCoordinate(xc - px, yc - py));
		}

		// Region 1
		int p1 = (int)(rySq - (rxSq * ry) + (0.25 * rxSq));
		while (px < py)
		{
			PlotEllipsePoints(xCenter, yCenter, x, y);
			x++;
			px += twoRySq;
			if (p1 < 0)
			{
				p1 += rySq + px;
			}
			else
			{
				y--;
				py -= twoRxSq;
				p1 += rySq + px - py;
			}
		}

		// Region 2
		int p2 = (int)(rySq * (x + 0.5) * (x + 0.5) + rxSq * (y - 1) * (y - 1) - rxSq * rySq);
		while (y >= 0)
		{
			PlotEllipsePoints(xCenter, yCenter, x, y);
			y--;
			py -= twoRxSq;
			if (p2 > 0)
			{
				p2 += rxSq - py;
			}
			else
			{
				x++;
				px += twoRySq;
				p2 += rxSq - py + px;
			}
		}

		return points;
	}

	private void CirclePlotPoints(int xCenter, int yCenter, int x, int y, List<Vector2> points)
	{

	}

	private void EllipsePlotPoints(int xCenter, int yCenter, int x, int y, List<Vector2> points)
	{

	}
	
}
