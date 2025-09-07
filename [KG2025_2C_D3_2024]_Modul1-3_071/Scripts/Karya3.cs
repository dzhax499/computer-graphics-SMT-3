namespace Godot;

using Godot;
using System;
using System.Collections.Generic;
using System.Numerics; // Import System.Numerics for Matrix4x4

public partial class Karya3 : Node2D
{
	private BentukDasar _bentukDasar = new BentukDasar();

    
	public override void _Ready()
    {
		ScreenUtils.Initialize(GetViewport()); // Initialize ScreenUtils
		QueueRedraw();
    }

    public override void _Process(double delta)
    {

        QueueRedraw(); // Redraw the scene
    }

	public override void _Draw()
	{
	}

	public override void _ExitTree()
    {
		NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
        base._ExitTree();
    }	
}	
