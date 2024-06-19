using Godot;
using System;
using System.Runtime.Serialization;
using System.Security.AccessControl;

public partial class WeaponHitbox : Area2D
{
	private Marker2D _weaponOrigin;
	private Marker2D _extendedMarker;
	private CollisionShape2D _collisionShape;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_weaponOrigin = GetNode<Marker2D>("WeaponOrigin");
		_extendedMarker = GetNode<Marker2D>("ExtendedMarker");
		_collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdatePositionAndRoation(); 
	}

	// Veraendern der Position und Rotation des Angriffs um sich Richtung Maus zu positionieren
	public void UpdatePositionAndRoation()
	{
		Position = _weaponOrigin.Position;
		_collisionShape.LookAt(_extendedMarker.GlobalPosition);
	}
}

