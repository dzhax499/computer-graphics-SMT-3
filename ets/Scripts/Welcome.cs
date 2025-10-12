namespace Godot;

using Godot;
using System;
public partial class Welcome: Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	private void _on_btnMain_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Main.tscn");
	}

	private void _on_btnabout_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/About.tscn");
	}

	private void _on_btnguide_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Guide.tscn");
	}

	private void _on_btnexit_pressed()
	{
		GetTree().Quit();
	}
}
