using Godot;
using System;

public partial class ExtendedMarker : Marker2D
{
	private Marker2D _weaponOrigin;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_weaponOrigin = GetNode<Marker2D>("../WeaponOrigin");	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position = _weaponOrigin.Position * 2;
	}
}
