using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public class TileMap : Godot.TileMap
{
	private int[,] tmapArray = new int[100, 100];


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ResetTileMap();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
	}

	private int[,] GOL(int[,] tmapArray)
	{
		var count = 0;
		int[,] newArray = new int[tmapArray.GetLength(0), tmapArray.GetLength(1)];

		for (int x = 0; x < tmapArray.GetLength(0); x++)
		for (int y = 0; y < tmapArray.GetLength(1); y++)
		{
			if (tmapArray[x, y] == 1)
			{
				foreach (int el in EnumerateAdjacent(tmapArray, x, y))
					if (el == 1)
						count += 1;

				if (count > 3 || count < 2)
					newArray[x, y] = 0;
				else
					newArray[x, y] = 1;

				count = 0;
			}

			if (tmapArray[x, y] == 0)
			{
				foreach (var el in EnumerateAdjacent(tmapArray, x, y))
					if (el == 1)
						count += 1;

				if (count == 3)
					newArray[x, y] = 1;
				else
					newArray[x, y] = 0;

				count = 0;
			}
		}

		return newArray;
	}

	private IEnumerable<int> EnumerateAdjacent(int[,] arr, int x, int y)
	{
		int width = arr.GetLength(0);
		int height = arr.GetLength(1);

		for (int xx = x - 1; xx <= x + 1; xx++)
		for (int yy = y - 1; yy <= y + 1; yy++)
			if (xx >= 0 && yy >= 0 && xx < width && yy < height)
				if (!(xx == x && yy == y))
					yield return arr[xx, yy];
	}

	private void ResetTileMap()
	{
		for (int x = 0; x < 100; x++)
		for (int y = 0; y < 100; y++)
		{
			SetCell(x, y, 0);
		}
	}




	private void _on_Timer_timeout()
	{
		tmapArray = GOL(tmapArray);

		for (int x = 0; x < 100; x++)
		for (int y = 0; y < 100; y++)
		{
			SetCell(x, y, tmapArray[x, y]);
		}

		GD.Print("timeout");
	}

	public override void _UnhandledInput(InputEvent @event)
	{

		if (@event is InputEventMouseButton mbtnEvent)
			if (mbtnEvent.ButtonIndex == (int) ButtonList.Left)
			{
				if (GetNode<TextureButton>("/root/Node2D/CanvasLayer/Sidebar/TileContainer/BlackTile").Pressed)
				{
					int x = (int) WorldToMap(GetLocalMousePosition()).x;
					int y = (int) WorldToMap(GetLocalMousePosition()).y;

					SetCell(x, y, 1);
					tmapArray[x, y] = 1;
				}
				
				if (GetNode<TextureButton>("/root/Node2D/CanvasLayer/Sidebar/TileContainer/WhiteTile").Pressed)
				{
					int x = (int) WorldToMap(GetLocalMousePosition()).x;
					int y = (int) WorldToMap(GetLocalMousePosition()).y;

					SetCell(x, y, 0);
					tmapArray[x, y] = 0;
				}

			}


	}



	private void _on_btnReset_pressed()
	{
		ResetTileMap();
	}
}
