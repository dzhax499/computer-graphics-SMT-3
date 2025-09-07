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
    //pemanggilan fungsi
    var FungsiEksponen = _bentukDasar.FungsiEksponen(0,70,0.5f);

    // gambar grafiknya
    GraphicsUtils.PutPixelAll(this, expFunc, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(3));
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