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
			MyPersegiPanjang();
			MySegitigaSiku();
			MySegitigaSamaKaki();
			MyTrapesiumSiku();
		}
		private void MyPersegi(){
			// === KUADRAN I (Kanan Atas) x > 0, y > 0 ===
			var titikAwal_Persegi1 = ScreenUtils.ToScreenCoordinate(50, 50);
			var persegi1 = _bentukDasar.Persegi(titikAwal_Persegi1.X, titikAwal_Persegi1.Y, 40);
			GraphicsUtils.PutPixelAll(this, persegi1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(1));

			// === KUADRAN II (Kiri Atas) x < 0, y > 0 ===
			var titikAwal_Persegi2 = ScreenUtils.ToScreenCoordinate(-90, 50);
			var persegi2 = _bentukDasar.Persegi(titikAwal_Persegi2.X, titikAwal_Persegi2.Y, 40);
			GraphicsUtils.PutPixelAll(this, persegi2, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(3));

			// === KUADRAN III (Kiri Bawah) x < 0, y < 0 ===
			var titikAwal_Persegi3 = ScreenUtils.ToScreenCoordinate(-90, -90);
			var persegi3 = _bentukDasar.Persegi(titikAwal_Persegi3.X, titikAwal_Persegi3.Y, 40);
			GraphicsUtils.PutPixelAll(this, persegi3, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5));

			// === KUADRAN IV (Kanan Bawah) x > 0, y < 0 ===
			var titikAwal_Persegi4 = ScreenUtils.ToScreenCoordinate(50, -90);
			var persegi4 = _bentukDasar.Persegi(titikAwal_Persegi4.X, titikAwal_Persegi4.Y, 40);
			GraphicsUtils.PutPixelAll(this, persegi4, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(2));
		}
		private void MyPersegiPanjang(){
			//KI
			var titikAwal_PersegiPanjang1 = ScreenUtils.ToScreenCoordinate(100, 150);
			var persegipanjang1 = _bentukDasar.PersegiPanjang(titikAwal_PersegiPanjang1.X, titikAwal_PersegiPanjang1.Y, 50,100);
			GraphicsUtils.PutPixelAll(this, persegipanjang1, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(5));

			//KII
			var titikAwal_PersegiPanjang2 = ScreenUtils.ToScreenCoordinate(-180, 100);
			var persegipanjang2 = _bentukDasar.PersegiPanjang(titikAwal_PersegiPanjang2.X, titikAwal_PersegiPanjang2.Y, 50,100);
			GraphicsUtils.PutPixelAll(this, persegipanjang2, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(7));

			//KII
			var titikAwal_PersegiPanjang3 = ScreenUtils.ToScreenCoordinate(-150, -150);
			var persegipanjang3 = _bentukDasar.PersegiPanjang(titikAwal_PersegiPanjang3.X, titikAwal_PersegiPanjang3.Y, 50,100);
			GraphicsUtils.PutPixelAll(this, persegipanjang3, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(6));
			
			//KIV
			var titikAwal_PersegiPanjang4 = ScreenUtils.ToScreenCoordinate(150, -150);
			var persegipanjang4 = _bentukDasar.PersegiPanjang(titikAwal_PersegiPanjang4.X, titikAwal_PersegiPanjang4.Y, 50,100);
			GraphicsUtils.PutPixelAll(this, persegipanjang4, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(9));
		}

		private void MySegitigaSiku(){
			//KI
			var segitigasiku1 = _bentukDasar.SegitigaSiku(new Vector2(150, 170), 50, 35);
       		 GraphicsUtils.PutPixelAll(this, segitigasiku1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(4));
			// //KII
			var segitigasiku2 = _bentukDasar.SegitigaSiku(new Vector2(-180, 170), 100, 35);
       		GraphicsUtils.PutPixelAll(this, segitigasiku2, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(4));

			// //KIII
			var segitigasiku3 = _bentukDasar.SegitigaSiku(new Vector2(-200, -120), 50, 100);
       		GraphicsUtils.PutPixelAll(this, segitigasiku3, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(4));

			// //KIV
			var segitigasiku4 = _bentukDasar.SegitigaSiku(new Vector2(210, -80), 100, 70);
       		GraphicsUtils.PutPixelAll(this, segitigasiku4, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(4));
		}

		public void MySegitigaSamaKaki()
		{
			// KI
			var titikAwal1 = ScreenUtils.ToScreenCoordinate(200, 200);
			var segitigasamakaki1 = _bentukDasar.SegitigaSamaKaki(titikAwal1, 60, 80);
			GraphicsUtils.PutPixelAll(this, segitigasamakaki1, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5));

			// KII
			var titikAwal2 = ScreenUtils.ToScreenCoordinate(-100, 200);
			var segitigasamakaki2 = _bentukDasar.SegitigaSamaKaki(titikAwal2, 60, 80);
			GraphicsUtils.PutPixelAll(this, segitigasamakaki2, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5));

			// KIII
			var titikAwal3 = ScreenUtils.ToScreenCoordinate(-200, -200);
			var segitigasamakaki3 = _bentukDasar.SegitigaSamaKaki(titikAwal3, 60, 80);
			GraphicsUtils.PutPixelAll(this, segitigasamakaki3, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5));

			// KIV
			var titikAwal4 = ScreenUtils.ToScreenCoordinate(200, -200);
			var segitigasamakaki4 = _bentukDasar.SegitigaSamaKaki(titikAwal4, 60, 80);
			GraphicsUtils.PutPixelAll(this, segitigasamakaki4, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5));
		}


		private void MyTrapesiumSiku(){
			//KI
			// var titikAwalTrapesiumSiku1 = ScreenUtils.ToScreenCoordinate(250,300);
			// var trapesiumSiku1 = _bentukDasar.TrapesiumSiku(titikAwalTrapesiumSiku1, 30, 50, 40);
			// GraphicsUtils.PutPixelAll(this, trapesiumSiku1, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(6));
			//KI
			var titikAwalTrapesiumSiku1 = new Vector2(250, 200); // Jangan pakai ToScreenCoordinate
			var trapesiumSiku1 = _bentukDasar.TrapesiumSiku(titikAwalTrapesiumSiku1, 30, 50, 40);
			GraphicsUtils.PutPixelAll(this, trapesiumSiku1, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(6));
			//KII
			var titikAwalTrapesiumSiku2 = new Vector2(-250, 200); // Jangan pakai ToScreenCoordinate
			var trapesiumSiku2 = _bentukDasar.TrapesiumSiku(titikAwalTrapesiumSiku2, 30, 50, 40);
			GraphicsUtils.PutPixelAll(this, trapesiumSiku2, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(6));
			//KIII
			var titikAwalTrapesiumSiku3 = new Vector2(-250, -200); // Jangan pakai ToScreenCoordinate
			var trapesiumSiku3 = _bentukDasar.TrapesiumSiku(titikAwalTrapesiumSiku3, 30, 50, 40);
			GraphicsUtils.PutPixelAll(this, trapesiumSiku3, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(6));
			//KIV
			var titikAwalTrapesiumSiku4 = new Vector2(250, -200); // Jangan pakai ToScreenCoordinate
			var trapesiumSiku4 = _bentukDasar.TrapesiumSiku(titikAwalTrapesiumSiku4, 30, 50, 40);
			GraphicsUtils.PutPixelAll(this, trapesiumSiku4, GraphicsUtils.DrawStyle.DotStripDot, ColorUtils.ColorStorage(6));
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
