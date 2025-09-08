namespace Godot;

using Godot;
using System;
public partial class Welcome: Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	private void _on_btnkarya1_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Karya1.tscn");
	}

	private void _on_btnkarya2_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Karya2.tscn");
	}

	private void _on_btnkarya3_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Karya3.tscn");
	}
	
	private void _on_btnkarya_4_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Karya4.tscn");
	}

	private void _on_btnkarya_5_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Karya5.tscn");
	}

	private void _on_btnkarya_6_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Karya3.tscn");
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
