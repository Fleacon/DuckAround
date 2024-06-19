using Godot;
using System;

public partial class WeaponOrigin : Marker2D
{
	[Export]
	public float Radius { get; set; } = 30;

	private float _angle;
	private Marker2D _origin;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_origin = GetParent().GetParent().GetNode<Marker2D>("Origin");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ChangePosition();
	}

	// Funktion zum veraendern der Position in Richtung Maus in einem Radius
	private void ChangePosition()
	{
        var mousePosition = _origin.GetLocalMousePosition().Round();
        _angle = _origin.Position.AngleToPoint(mousePosition);
        float x = (float)Math.Cos(_angle) * Radius;
        float y = (float)Math.Sin(_angle * -1) * Radius;
        Position = new Godot.Vector2(x, y);
    }
}
