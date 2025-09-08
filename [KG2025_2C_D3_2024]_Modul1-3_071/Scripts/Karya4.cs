	namespace Godot;

	using Godot;
	using System;
	using System.Collections.Generic;

	public partial class Karya4 : Node2D
	{
		private Primitif _bentukDasar = new Primitif();
		private Transformasi _transformasi = new Transformasi();

		public override void _Ready()
		{
			ScreenUtils.Initialize(GetViewport()); // Initialize ScreenUtils
			QueueRedraw();
		}

		public override void _Draw()
		{
			MarginPixel();
			ScreenUtils.DrawAxes(this, _bentukDasar);
			MyPersegi();
		}
		private void MyPersegi(){
			// === KUADRAN I (Kanan Atas) x > 0, y > 0 ===
			var pos1 = ScreenUtils.ToScreenCoordinate(50, 50);
			var persegi1 = _bentukDasar.Persegi(pos1.X, pos1.Y, 40);
			GraphicsUtils.PutPixelAll(this, persegi1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(1));

			// === KUADRAN II (Kiri Atas) x < 0, y > 0 ===
			var pos2 = ScreenUtils.ToScreenCoordinate(-90, 50);
			var persegi2 = _bentukDasar.Persegi(pos2.X, pos2.Y, 40);
			GraphicsUtils.PutPixelAll(this, persegi2, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(3));

			// === KUADRAN III (Kiri Bawah) x < 0, y < 0 ===
			var pos3 = ScreenUtils.ToScreenCoordinate(-90, -90);
			var persegi3 = _bentukDasar.Persegi(pos3.X, pos3.Y, 40);
			GraphicsUtils.PutPixelAll(this, persegi3, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5));

			// === KUADRAN IV (Kanan Bawah) x > 0, y < 0 ===
			var pos4 = ScreenUtils.ToScreenCoordinate(50, -90);
			var persegi4 = _bentukDasar.Persegi(pos4.X, pos4.Y, 40);
			GraphicsUtils.PutPixelAll(this, persegi4, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(1));
		}
		private void MyPersegiPanjang(){
			//KI
			var pos1 = ScreenUtils.ToScreenCoordinate(100, 150);
			var persegipanjang1 = _bentukDasar.PersegiPanjang(pos1.X, pos1.Y, 40);
			GraphicsUtils.PutPixelAll(this, persegipanjang1, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(1));

			//KII
			var pos2 = ScreenUtils.ToScreenCoordinate(-100, 100);
			var persegipanjang2 = _bentukDasar.PersegiPanjang(pos2.X, pos2.Y, 40);
			GraphicsUtils.PutPixelAll(this, persegipanjang2, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(1));

			//KII
			var pos3 = ScreenUtils.ToScreenCoordinate(-150, -150);
			var persegipanjang3 = _bentukDasar.PersegiPanjang(pos3.X, pos3.Y, 40);
			GraphicsUtils.PutPixelAll(this, persegipanjang3, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(1));
			
			//KIV
			var pos4 = ScreenUtils.ToScreenCoordinate(150, -150);
			var persegipanjang4 = _bentukDasar.PersegiPanjang(pos4.X, pos4.Y, 40);
			GraphicsUtils.PutPixelAll(this, persegipanjang4, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(1));
		}

		// private void MySegitiga(){
		// 	//KII
		// 	var segitiga1 = _bentukDasar.SegitigaSiku(new Vector2(-180, 100), 50, 35);
		// 	GraphicsUtils.PutPixelAll(this, segitiga1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(4));

		// 	//KIII

		// }

		// private void MyTrapesiumSiku(){
		// 	//KIII
		// 	var trapesiumSiku1 = _bentukDasar.TrapesiumSiku(new Vector2(-200, -50), 30, 50, 40);
		// 	GraphicsUtils.PutPixelAll(this, trapesiumSiku1, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(6));
		// }

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
