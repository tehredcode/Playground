using Godot;
using System;
using System.Runtime.InteropServices;

public class Camera2D : Godot.Camera2D
{
	private Vector2 _camVec = new Vector2(0,0);
	private float _maxZoomLevel = 0.5f;
	private float _minZoomLevel = 8.0f;
	private float _zoomIncrement = 0.05f;
	private float _currentZoomLevel = 1;
	private bool _drag = false;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("cam_drag"))
			_drag = true;
		if (@event.IsActionReleased("cam_drag"))
			_drag = false;
		if (@event.IsAction("zoom_in"))
			UpdateZoom(-_zoomIncrement, GetLocalMousePosition());
		if (@event.IsAction("zoom_out"))
			UpdateZoom(_zoomIncrement, GetLocalMousePosition());
		if (@event is InputEventMouseMotion mevent && _drag)
			Offset = Offset - mevent.Relative * _currentZoomLevel;

	}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	 if (Input.IsActionPressed("ui_up")) 
		 _camVec.y += -10;
	 if (Input.IsActionPressed("ui_down"))
		 _camVec.y += 10;
	 if (Input.IsActionPressed("ui_left"))
		 _camVec.x += -10;
	 if (Input.IsActionPressed("ui_right"))
		 _camVec.x += 10;

	 Position = _camVec;
  }

  private void UpdateZoom(float increment,Vector2 anchor)
  {
	  var oldZoom = _currentZoomLevel;
	  _currentZoomLevel += increment;
	  if (_currentZoomLevel < _maxZoomLevel)
		  _currentZoomLevel = _maxZoomLevel;
	  if (_currentZoomLevel > _minZoomLevel)
		  _currentZoomLevel = _minZoomLevel;
	  if (oldZoom == _currentZoomLevel)
		  return;

	  var zoomCenter = anchor - Offset;
	  var ratio = 1 - _currentZoomLevel / oldZoom;
	  Offset = Offset + zoomCenter * ratio;

	  Zoom = new Vector2(_currentZoomLevel, _currentZoomLevel);
  }
  
}

