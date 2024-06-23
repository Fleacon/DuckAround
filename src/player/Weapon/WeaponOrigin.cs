using Godot;
using System;

public partial class WeaponOrigin : Marker2D
{
	[Export]
	public float Radius { get; set; } = 30;

	private float _angle;
	private Marker2D _playerOrigin;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playerOrigin = GetParent().GetParent().GetNode<Marker2D>("Origin");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ChangePosition();
	}

	// Funktion zum veraendern der Position in Richtung Maus in einem Radius
	private void ChangePosition()
	{
		// Maus Position bekommen
        var mousePosition = _playerOrigin.GetLocalMousePosition().Round();

		// Winkel vom Spieler Urpsprung zur Mausposition
        _angle = _playerOrigin.Position.AngleToPoint(mousePosition);

		// Berechnen der Waffenposition mit relation zum Radius
        float x = (float)Math.Cos(_angle) * Radius;
        float y = (float)Math.Sin(_angle * -1) * Radius;

		// Setzen der neuen Position
        Position = new Godot.Vector2(x, y);
    }
}
