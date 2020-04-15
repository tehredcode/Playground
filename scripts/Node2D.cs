using Godot;
using System;

public class Node2D : Node
{
	private TextureButton lastpressed = null;
	


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{

	}



	private void _on_BlackTile_pressed()
	{
		var blackbutton = GetNode<TextureButton>("/root/Node2D/CanvasLayer/Sidebar/TileContainer/BlackTile");

		if (lastpressed == blackbutton)
		{
			blackbutton.Pressed = false;
			lastpressed = null;
		}
		else
		{
			lastpressed = blackbutton;
		}
	}


	private void _on_WhiteTile_pressed()
	{
		var whitebutton = GetNode<TextureButton>("/root/Node2D/CanvasLayer/Sidebar/TileContainer/WhiteTile");

		if (lastpressed == whitebutton)
		{
			whitebutton.Pressed = false;
			lastpressed = null;
		}
		else
		{
			lastpressed = whitebutton;
		}
	}
}
