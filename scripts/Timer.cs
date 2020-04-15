using Godot;
using System;

public class Timer : Godot.Timer
{

	public override void _Ready()
	{

	}


	public override void _Process(float delta)
	{

	}



	private void _on_btnStart_pressed()
	{
		if (IsStopped())
			Start();
	}


	private void _on_btnStop_pressed()
	{
		if (!IsStopped())
			Stop();
	}


	private void _on_btnReset_pressed()
	{
		
	}
}
