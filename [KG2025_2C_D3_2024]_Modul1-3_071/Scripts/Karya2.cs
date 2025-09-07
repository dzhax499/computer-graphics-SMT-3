namespace Godot;

using Godot;
using System;
using System.Collections.Generic;
using System.Numerics; // Import System.Numerics for Matrix4x4

public partial class Karya2 : Node2D
{
	private BentukDasar _bentukDasar = new BentukDasar();
	public override void _Ready()
    {
		ScreenUtils.Initialize(GetViewport()); // Initialize ScreenUtils
		QueueRedraw();
    }

	public override void _Draw()
	{
		MyPersegi();
	}

	private void MyPersegi(){
		var persegi1 = _bentukDasar.Persegi(100, 100, 50); // Gambar persegi di posisi (100, 100) dengan ukuran 50
		GraphicsUtils.PutPixelAll(this, persegi1, color: ColorUtils.ColorStorage(3));
	}

	private void MarginPixel(){
		var margin = _bentukDasar.Margin();
		GraphicsUtils.PutPixelAll(this, margin, color: ColorUtils.ColorStorage(0));
	}

	public override void _ExitTree()
    {
		NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
        base._ExitTree();
    }	
}	
