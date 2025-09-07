using System;

namespace Godot;

public partial class Karya3 : Node2D
{
  private Primitif _primitif = new Primitif();
  private BentukDasar _bentukDasar = new BentukDasar();

  public override void _Ready()
  {
    ScreenUtils.Initialize(GetViewport()); // Initialize ScreenUtils
    QueueRedraw();
  }

  public override void _Draw()
  {
    MarginPixel();
    MyFungsi();
  }

  private void MyFungsi()
  {
      // GAMBAR SUMBU KOORDINAT DULU
      GambarKordinat();
      
      // BARU GAMBAR FUNGSI MATEMATIKA
      GambarFungsi();
  }

  private void GambarKordinat()
  {
      // Sumbu X (horizontal) - warna merah
      var sumbuX = _bentukDasar.SumbuX(1000);
      GraphicsUtils.PutPixelAll(this, sumbuX, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(1));
      
      // Sumbu Y (vertikal) - warna hijau  
      var sumbuY = _bentukDasar.SumbuY(1000);
      GraphicsUtils.PutPixelAll(this, sumbuY, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(2));
  }

  private void GambarFungsi()
  {
      // Kuadran 1 - berbagai fungsi matematika
      var fungsiAbsolut = _bentukDasar.FungsiAbsolut(-30, 600, 1f);
      GraphicsUtils.PutPixelAll(this, fungsiAbsolut, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(4));

      var fungsiEksponensial = _bentukDasar.FungsiEksponensial(0, 500, 0.5f);
      GraphicsUtils.PutPixelAll(this, fungsiEksponensial, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(5));

      var fungsiLogaritma = _bentukDasar.FungsiLogaritma(1, 1000, 1f);
      GraphicsUtils.PutPixelAll(this, fungsiLogaritma, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(6));
  }

  private void MarginPixel()
  {
    var margin = _bentukDasar.Margin();
    GraphicsUtils.PutPixelAll(this, margin, color: ColorUtils.ColorStorage(0));
  }

  public override void _ExitTree()
  {
    NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
    base._ExitTree();
  }
}