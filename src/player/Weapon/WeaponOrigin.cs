using Godot;
using System;

public partial class WeaponOrigin : Marker2D
{
	[Export]
	public float Radius { get; set; } = 30;
	public float Angle { get; set; }
	private Marker2D _origin;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_origin = GetParent().GetParent().GetNode<Marker2D>("Origin");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var mousePosition = _origin.GetLocalMousePosition().Round();
		Angle = _origin.Position.AngleToPoint(mousePosition);
		float x = (float) Math.Cos(Angle) * Radius;
		float y = (float) Math.Sin(Angle*-1) * Radius;
		//GD.Print("mousePos: " + mousePosition + "  angle: " + Angle);
		Position = new Godot.Vector2(x,y);
	}
}
