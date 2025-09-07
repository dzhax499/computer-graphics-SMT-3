namespace Godot;

using Godot;
using System;
using System.Collections.Generic;

public partial class Karya4 : Node2D
{
	private BentukDasar _bentukDasar = new BentukDasar();
	private Transformasi _transformasi = new Transformasi();

	public override void _Ready()
    {
		ScreenUtils.Initialize(GetViewport()); // Initialize ScreenUtils
		QueueRedraw();
    }

	public override void _Draw()
	{
		MarginPixel();
		MyPersegi();
	}

	private void MyPersegi(){
		var persegi1 = _bentukDasar.Persegi(100, 100, 50); // Gambar persegi di posisi (100, 100) dengan ukuran 50
		GraphicsUtils.PutPixelAll(this, persegi1, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(4), 3, 2);

    	var persegipanjang1 = _bentukDasar.PersegiPanjang(200, 150, 80, 40); // Gambar persegi panjang
		GraphicsUtils.PutPixelAll(this, persegipanjang1, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(3), 3, 2);

		var segitiga1 = _bentukDasar.SegitigaSiku(new Vector2(300, 300), 60, 40); // Gambar segitiga siku-siku
		GraphicsUtils.PutPixelAll(this, segitiga1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5), 3, 2);
	}

	private void MarginPixel(){
		var margin = _bentukDasar.Margin();
		GraphicsUtils.PutPixelAll(this, margin, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(6));
	}

	public override void _ExitTree()
    {
		NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
        NodeUtils.DisposeAndNull(_transformasi, "_transformasi");
        base._ExitTree();
    }
	
}	
